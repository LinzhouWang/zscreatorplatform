using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Extensions.Cache
{
    public class DistributedCSRedisCache : IDistributedCache,IDisposable
    {

        private readonly CSRedisClient _redisClient;

        public DistributedCSRedisCache(IOptions<RedisOptions> redisAccessor)
        {
            if (redisAccessor==null)
            {
                throw new ArgumentNullException(nameof(redisAccessor));
            }
            var options = redisAccessor.Value;
            if (options==null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (options.NodeRole!=null&&options.ConnectionStrings!=null)
            {
                _redisClient = new CSRedisClient(options.NodeRole,options.ConnectionStrings);
            }
            else if (options.RedisConnectionString!=null)
            {
                _redisClient = new CSRedisClient(redisAccessor.Value.RedisConnectionString);
            }
            else
            {
                throw new ArgumentNullException(nameof(redisAccessor.Value.RedisConnectionString));
            }
           
            RedisHelper.Initialization(_redisClient);
        }

        public void Dispose()
        {
            if (_redisClient!=null)
            {
                _redisClient.Dispose();
            }
        }

        public byte[] Get(string key)
        {
            if (key==null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return RedisHelper.Get<byte[]>(key);
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return await RedisHelper.GetAsync<byte[]>(key);
        }

        public void Refresh(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            //暂不实现
        }

        public async Task RefreshAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            token.ThrowIfCancellationRequested();
            await Task.CompletedTask;
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            RedisHelper.Del(key);
        }

        public async Task RemoveAsync(string key, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            token.ThrowIfCancellationRequested();
            await RedisHelper.DelAsync(key);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            RedisHelper.Set(key,value);
        }

        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            token.ThrowIfCancellationRequested();
            await RedisHelper.SetAsync(key,value);
        }
    }
}
