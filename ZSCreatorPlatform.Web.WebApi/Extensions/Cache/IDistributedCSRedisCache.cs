using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Extensions.Cache
{
    /// <summary>
    /// About the extension interface of idistributedcache, you can extend more methods as needed
    /// </summary>
    public interface IDistributedCSRedisCache: IDistributedCache, IDisposable
    {

        T Get<T>(string key);

        Task<T> GetAsync<T>(string key, CancellationToken token = default);

        void Set<T>(string key, T value, DistributedCacheEntryOptions options);

        Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default);


    }
}
