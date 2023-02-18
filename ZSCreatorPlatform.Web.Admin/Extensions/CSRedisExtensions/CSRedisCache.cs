using System;
using System.Threading;
using System.Threading.Tasks;
using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ZSCreatorPlatform.Web.Admin.Extensions.CSRedisExtensions
{
    /// <summary>
    /// CSDistributedCache
    /// </summary>
    public class CSRedisCache:IDistributedCache
    {


        #region Contors

        private readonly CSRedisClient _csRedisClient;
        
        private readonly ILogger<CSRedisCache> _logger;

        
        // public CSRedisCache(IOptions<CSRedisCacheOptions> optionsAccessor) 
        //     : this(optionsAccessor, Abstractions.NullLoggerFactory.Instance.CreateLogger<CSRedisCache>())//默认创建日志
        // {
        //     
        // }

        public CSRedisCache(IOptions<CSRedisCacheOptions> optionsAccessor,ILogger<CSRedisCache> logger)
        {
            if (optionsAccessor==null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            if (logger==null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            
            CSRedisCacheOptions csRedisCacheOptions = optionsAccessor.Value;
            if (csRedisCacheOptions.NodeRule != null && csRedisCacheOptions.ConnectionStrings != null)
            {
                _csRedisClient = new CSRedisClient(csRedisCacheOptions.NodeRule, csRedisCacheOptions.ConnectionStrings);
            }
            else
            {
                if (csRedisCacheOptions.ConnectionString==null)
                {
                    throw new ArgumentNullException(nameof(csRedisCacheOptions.ConnectionString));
                }
                _csRedisClient = new CSRedisClient(csRedisCacheOptions.ConnectionString);
            }

            RedisHelper<RedisHelper>.Initialization(_csRedisClient);
            
            _logger = logger;
        }

        #endregion



        public byte[] Get(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            throw new System.NotImplementedException();
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
            CancellationToken token = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public void Refresh(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }
    }
}