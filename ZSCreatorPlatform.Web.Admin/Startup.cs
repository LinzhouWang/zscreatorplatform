using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ZSCreatorPlatform.Web.Admin.Extensions;

namespace ZSCreatorPlatform.Web.Admin
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
            services.AddControllersWithViews();

            //��֤\��Ȩ
            #region �Զ���cookie��֤
            //��֤
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(DefaultAuthenticationHandler);
                options.DefaultForbidScheme = nameof(DefaultAuthenticationHandler);
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
             {
                 options.LoginPath = "/Account/Login";
                 options.LogoutPath = "/Account/Logout";
                 options.AccessDeniedPath = "/Account/AccessDenied";
                 options.SlidingExpiration = true;
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                 options.Cookie.Name = "ZSCreatorPlatform";
                 options.Cookie.HttpOnly = true;
             })
            .AddScheme<AuthenticationSchemeOptions, DefaultAuthenticationHandler>(nameof(DefaultAuthenticationHandler),null);

            //��Ȩ
            services.AddAuthorization(options=> 
            {
                options.AddPolicy("default",builder=> 
                {
                    builder.Requirements.Add(new DefaultAuthorizationRequirement());
                });
            });
            services.AddScoped<IAuthorizationHandler, DefaultAuthorizationHandler>(); 
            #endregion
            
            //����
            //�ڴ滺��
            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromHours(3);//Ĭ��ÿ����Сʱ��⻺���Ƿ����
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _configuration.GetConnectionString("");
                options.InstanceName = "";
            });

            services.AddCSRedisCache();

            //�ӿ��ĵ�swagger

            //����

            //�쳣

            //��ʱ����

            //���ݿ⡢���

            //cap

            //��Ϣ����

            //�����ڣ����̼�����һ����

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();//index.htm index.html default.htm default.html
            defaultFilesOptions.DefaultFileNames.Add("login.html");
            app.UseDefaultFiles(defaultFilesOptions);
     
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints=> 
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{Controller=Account}/{Action=Login}/{id?}"
                    );
            });

        }
    }
}
