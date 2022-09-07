using CSRedisDistributed;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSCreatorPlatform.Model.Models;
using ZSCreatorPlatform.Web.WebApi.Extensions;
using ZSCreatorPlatform.Web.WebApi.Extensions.ActionFilters;
//using ZSCreatorPlatform.Web.WebApi.Extensions.Cache;
using ZSCreatorPlatform.Web.WebApi.Extensions.Dtos;
using ZSCreatorPlatform.Web.WebApi.Extensions.Exception;

namespace ZSCreatorPlatform.Web.WebApi
{
    public class Startup
    {

        public IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options=> 
            {
                options.AddPolicy("any",builder=> 
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddDbContextPool<ZSAuthContext>(builder=> 
            {
                builder.UseMySql(_configuration.GetConnectionString("DefaultDbConnectionString"));
            });

            services.AddOptions();
            services.Configure<JwtConfigDto>(_configuration.GetSection("JwtConfiguration"));

            #region Cache

            services.AddMemoryCache();

            services.AddDistributedCSRedisCache(optoins=> 
            {
                optoins.RedisConnectionString = _configuration.GetConnectionString("RedisConnectionString");
            });

            #endregion

            #region Authentication Authorization
            var jwtConfigDto = _configuration.GetSection("JwtConfiguration").Get<JwtConfigDto>();
            //决定用哪种认证方式，以及认证信息配置，请求未授权返回，请求禁止返回，token失效原因
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(ApiResponseAuthenticationHandler);
                options.DefaultForbidScheme = nameof(ApiResponseAuthenticationHandler);
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
             {
                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {//3+2
                     ValidIssuer = jwtConfigDto.Issuser,
                     ValidateIssuer = true,
                     ValidAudience = jwtConfigDto.Audience,
                     ValidateAudience = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigDto.SigningKey)),
                     ValidateIssuerSigningKey = true,
                     ClockSkew = TimeSpan.FromSeconds(0),
                     RequireExpirationTime = true,
                     ValidateLifetime = true
                 };
                 //options.Events.OnAuthenticationFailed = context => 
                 //{
                 //    context.Exception;
                 //};
                 options.Events = new JwtBearerEvents
                 {
                     OnChallenge = context =>
                     {
                         context.Response.Headers.Add("token-error", context.ErrorDescription);
                         return Task.CompletedTask;
                     },
                     OnAuthenticationFailed = context =>
                     {
                         try
                         {
                             var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                             var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                             if (jwtToken.Issuer != jwtConfigDto.Issuser)
                             {
                                 context.Response.Headers.Add("token-error-issuser", "issuser is wrong");
                             }

                             if (jwtToken.Audiences.FirstOrDefault() != jwtConfigDto.Audience)
                             {
                                 context.Response.Headers.Add("token-error-audience", "audience is wrong");
                             }

                             if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                             {
                                 context.Response.Headers.Add("token-expired", "token expired");
                             }
                         }
                         catch (Exception ex)//处理jwttoken传输错误读取失败
                         {
                             context.Response.Headers.Add("token-error", ex.Message);
                         }


                         return Task.CompletedTask;
                     }
                 };
             })
            .AddScheme<AuthenticationSchemeOptions, ApiResponseAuthenticationHandler>(nameof(ApiResponseAuthenticationHandler), options => { });

            //动态验证权限，使用自定义策略，但是这里只能判断出token是否正确，无法判断出到底是token过期还是token不合法
            services.AddAuthorization(options =>
            {
                options.AddPolicy("default", builder =>
                 {
                     builder.Requirements.Add(new CAuthorizationRequirement());
                 });
            });
            services.AddScoped<IAuthorizationHandler, CAuthorizationHandler>(); 
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<ApiBehaviorOptions>(options=> //关闭模型验证，默认返回
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //swaggergen
            //services.AddSwaggerGen(options=> 
            //{
            //    options.SwaggerDoc("v1.0.0",new Microsoft.OpenApi.Models.OpenApiInfo 
            //    {
            //        Title="ZSCreatorPlatformTitle",
            //        Version="v1"
            //    });
            //});
            services.AddSwaggerGen();


            services.AddControllers(options=> 
            {
                //实体验证
                options.Filters.Add<CActionFilterAttribute>();
                //异常处理
                options.Filters.Add<CExceptionFilter>();
                //Swagger剔除不需要加入api展示的列表
                //options.Conventions.Add(new ApiExplorerIgnores());
            }).AddNewtonsoftJson(options=> 
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";//兼容ios"yyyy-MM-dd HH:mm:ss"不支持
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            //Directory
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //异常、全局日志、Hangfire、swagger
            //mysql 多租户 聚合根 聚合内事务events 聚合间事务(cap)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("login.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("any");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapSwagger();
            });
        }
    }
}
