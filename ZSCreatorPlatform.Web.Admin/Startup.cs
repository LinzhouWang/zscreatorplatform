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
            //接口文档swagger
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

            //认证\授权
            #region 自定义cookie认证
            //认证
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

            //授权
            services.AddAuthorization(options=> 
            {
                options.AddPolicy("default",builder=> 
                {
                    builder.Requirements.Add(new DefaultAuthorizationRequirement());
                });
            });
            services.AddScoped<IAuthorizationHandler, DefaultAuthorizationHandler>(); 
            #endregion
            
            //缓存
            #region 内存缓存

            //内存缓存
            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromHours(3);//默认每三个小时检测缓存是否过期
            });

            //同步timeout 异常正常
            // services.AddStackExchangeRedisCache(options =>
            // {
            //     options.Configuration = _configuration.GetConnectionString("");
            //     options.InstanceName = "";
            // });

            //自定义扩展，关于连接释放那块儿需要再测试
            // services.AddCSRedisCache(options =>
            // {
            //     
            // });
            
            //分布式缓存
            //单例
            var csredis = new CSRedisClient(_configuration.GetSection("RedisConnectionStrings")["Defalut"]);
            //services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(csredis));
            
            RedisHelper.Initialization(csredis);//也可以基于RedisHelper再封装成distributedcache
            //集群
            // var csredisClustor = new CSRedisClient(null,
            //     "127.0.0.1:6371,password=123,defaultDatabase=11,poolsize=10,ssl=false,writeBuffer=10240,prefix=key前辍", 
            //     "127.0.0.1:6372,password=123,defaultDatabase=12,poolsize=11,ssl=false,writeBuffer=10240,prefix=key前辍",
            //     "127.0.0.1:6373,password=123,defaultDatabase=13,poolsize=12,ssl=false,writeBuffer=10240,prefix=key前辍",
            //     "127.0.0.1:6374,password=123,defaultDatabase=14,poolsize=13,ssl=false,writeBuffer=10240,prefix=key前辍"
            // );
            // services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(csredisClustor));

            #endregion

            //跨域

            //异常

            //定时任务

            //数据库、多库

            //cap

            //消息队列

            //进程内，进程间事务一致性

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
