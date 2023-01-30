using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSRedisDistributed
{
    /// <summary>
    /// About the extension interface of idistributedcache, you can extend more methods as needed
    /// </summary>
    public interface IDistributedCSRedisCache : IDistributedCache, IDisposable
    {

        T Get<T>(string key);

        Task<T> GetAsync<T>(string key, CancellationToken token = default);

        void Set<T>(string key, T value, DistributedCacheEntryOptions options);

        Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default);


    }


    public interface IDistributedRedisCache:IDisposable
    {

        #region string

        #endregion


        #region hash

        #endregion


        #region list

        #endregion


        #region set

        #endregion


        #region zset(sorted set)

        #endregion


        #region key actions

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>is successful</returns>
        Task<bool> KeyDeleteAsync(string key);

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">redis key</param>
        /// <returns>successful number</returns>
        Task<long> KeyDeleteAsync(List<string> keys);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExistsAsync(string key);

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="newKey">rename redis key</param>
        /// <returns>is successful</returns>
        Task<bool> KeyRenameAsync(string key,string newKey);

        /// <summary>
        /// 设置key的过期时长
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire timespan</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExpireAsync(string key,TimeSpan? expire=default);

        /// <summary>
        /// 设置key过期时间
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire time</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExpireAtAsync(string key,DateTime expire);

        /// <summary>
        /// 搜索key
        /// </summary>
        /// <param name="pattern">表达式</param>
        /// <returns></returns>
        Task<List<string>> SearchKeys(string pattern);

        #endregion


        #region subscribe/publish

        /// <summary>
        /// 订阅，根据分区规则返回SubscribeObject，Subscribe(("chan1", msg => Console.WriteLine(msg.Body)),("chan2", msg => Console.WriteLine(msg.Body)))
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handler"></param>
        void SubScribe(string channel,Action<string> handler=null);

        /// <summary>
        /// 用于将信息发送到指定分区节点的频道，最终消息发布格式：1|message
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<long> PublishAsync(string channel,string msg);

        #endregion

    }

}
