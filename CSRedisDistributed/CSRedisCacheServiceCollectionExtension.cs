using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSRedisDistributed
{
    /// <summary>
    ///  Extension methods of csredis for adding services to an Microsoft.Extensions.DependencyInjection.IServiceCollection.
    /// </summary>
    public static class CSRedisCacheServiceCollectionExtension
    {

        /// <summary>
        /// Add a singleton service of the type specified in idistributedcsrediscache with an implementation
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the service to.</param>
        /// <param name="setupAction">redisoptions action</param>
        /// <returns></returns>
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

            services.AddOptions<RedisOptions>().Configure(setupAction);
            services.AddSingleton<IDistributedCSRedisCache, DistributedCSRedisCache>();
            return services;
        }
    }
}
