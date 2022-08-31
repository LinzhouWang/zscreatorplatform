using CSRedisDistributed;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using ZSCreatorPlatform.Web.WebApi.Extensions;
using ZSCreatorPlatform.Web.WebApi.Extensions.ActionFilters;
//using ZSCreatorPlatform.Web.WebApi.Extensions.Cache;
using ZSCreatorPlatform.Web.WebApi.Extensions.Dtos;

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
            //������������֤��ʽ���Լ���֤��Ϣ���ã�����δ��Ȩ���أ������ֹ���أ�tokenʧЧԭ��
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
                         catch (Exception ex)//����jwttoken��������ȡʧ��
                         {
                             context.Response.Headers.Add("token-error", "token error");
                         }


                         return Task.CompletedTask;
                     }
                 };
             })
            .AddScheme<AuthenticationSchemeOptions, ApiResponseAuthenticationHandler>(nameof(ApiResponseAuthenticationHandler), options => { });

            //��̬��֤Ȩ�ޣ�ʹ���Զ�����ԣ���������ֻ���жϳ�token�Ƿ���ȷ���޷��жϳ�������token���ڻ���token���Ϸ�
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


            services.Configure<ApiBehaviorOptions>(options=> //�ر�ģ����֤��Ĭ�Ϸ���
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options=> 
            {
                //ʵ����֤
                options.Filters.Add<CActionFilterAttribute>();
                //�쳣����
                //options.Filters.Add<GlobalExceptionsFilterForClient>();
                //Swagger�޳�����Ҫ����apiչʾ���б�
                //options.Conventions.Add(new ApiExplorerIgnores());
            }).AddNewtonsoftJson(options=> 
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";//����ios"yyyy-MM-dd HH:mm:ss"��֧��
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�쳣��ȫ����־��Hangfire
            //mysql ���⻧ �ۺϸ� �ۺ�������events �ۺϼ�����(cap)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("login.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();

            app.UseRouting();

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
            });
        }
    }
}
