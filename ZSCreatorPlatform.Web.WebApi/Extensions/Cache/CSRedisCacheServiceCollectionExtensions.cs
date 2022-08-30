using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Extensions.Cache
{
    /// <summary>
    /// csredis extension class
    /// </summary>
    public static class CSRedisCacheServiceCollectionExtensions
    {

        public static IServiceCollection AddDistributedCSRedisCache(this IServiceCollection services
            ,Action<RedisOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();//已配置过
            services.Configure(setupAction);
            services.AddSingleton<IDistributedCSRedisCache, DistributedCSRedisCache>();
            return services;
        }

    }
}
