using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSCreatorPlatform.Web.WebApi.Extensions;
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
                 //options.Events.OnChallenge = context => 
                 //{
                 //    context.
                 //};
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
             })
            .AddScheme<AuthenticationSchemeOptions,ApiResponseAuthenticationHandler>(nameof(ApiResponseAuthenticationHandler),options=> { });

            //��̬��֤Ȩ�ޣ�ʹ���Զ�����ԣ���������ֻ���жϳ�token�Ƿ���ȷ���޷��жϳ�������token���ڻ���token���Ϸ�
            services.AddAuthorization(options=> 
            {
                options.AddPolicy("default",builder=> 
                {
                    builder.Requirements.Add(new CAuthorizationRequirement());
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthorizationHandler,CAuthorizationHandler>();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
