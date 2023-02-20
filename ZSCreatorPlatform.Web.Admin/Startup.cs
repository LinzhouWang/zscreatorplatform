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
using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
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
            //�ӿ��ĵ�swagger
            #region swageger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("zscreatorplatform",new OpenApiInfo()
                {
                    Title = _configuration["Swagger:Title"],
                    Description = _configuration["Swagger:Description"],
                    Version = "v"+_configuration["Swagger:Version"],
                    Contact = new OpenApiContact()
                    {
                        Name = _configuration["Swagger:Name"],
                        Email = _configuration["Swagger:Email"],
                        Url = new Uri(_configuration["Swagger:Url"])
                    },
                    License = new OpenApiLicense()
                    {
                        Name = _configuration["Swagger:Name"],
                        Url = new Uri(_configuration["Swagger:Url"])
                    }
                });                
            });

            #endregion
            
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
            #region �ڴ滺��

            //�ڴ滺��
            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromHours(3);//Ĭ��ÿ����Сʱ��⻺���Ƿ����
            });

            //ͬ��timeout �쳣����
            // services.AddStackExchangeRedisCache(options =>
            // {
            //     options.Configuration = _configuration.GetConnectionString("");
            //     options.InstanceName = "";
            // });

            //�Զ�����չ�����������ͷ��ǿ����Ҫ�ٲ���
            // services.AddCSRedisCache(options =>
            // {
            //     
            // });
            
            //�ֲ�ʽ����
            //����
            var csredis = new CSRedisClient(_configuration.GetSection("RedisConnectionStrings")["Defalut"]);
            //services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(csredis));
            
            RedisHelper.Initialization(csredis);//Ҳ���Ի���RedisHelper�ٷ�װ��distributedcache
            //��Ⱥ
            // var csredisClustor = new CSRedisClient(null,
            //     "127.0.0.1:6371,password=123,defaultDatabase=11,poolsize=10,ssl=false,writeBuffer=10240,prefix=keyǰ�", 
            //     "127.0.0.1:6372,password=123,defaultDatabase=12,poolsize=11,ssl=false,writeBuffer=10240,prefix=keyǰ�",
            //     "127.0.0.1:6373,password=123,defaultDatabase=13,poolsize=12,ssl=false,writeBuffer=10240,prefix=keyǰ�",
            //     "127.0.0.1:6374,password=123,defaultDatabase=14,poolsize=13,ssl=false,writeBuffer=10240,prefix=keyǰ�"
            // );
            // services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(csredisClustor));

            #endregion

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

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("","");
                });
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
