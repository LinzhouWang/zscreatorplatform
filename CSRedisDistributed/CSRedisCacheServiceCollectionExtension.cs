using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSRedisDistributed
{
    public static class CSRedisCacheServiceCollectionExtension
    {
        public static IServiceCollection AddDistributedCSRedisCache(this IServiceCollection services
           , Action<RedisOptions> setupAction)
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
