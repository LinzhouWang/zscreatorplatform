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

        /// <summary>
        /// 保存单个string值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool StringSet(string key,string value,TimeSpan? expire=default);

        /// <summary>
        /// 保存单个string值异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync(string key,string value,TimeSpan? expire=default);

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool StringSet<T>(string key,T obj,TimeSpan? expire=default);

        /// <summary>
        /// 保存单个对象异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        Task<bool> StringSetAsync<T>(string key,T obj,TimeSpan? expire=default);

        /// <summary>
        /// 获取单个string值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string StringGet(string key);

        /// <summary>
        /// 获取单个string值异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> StringGetAsync(string key);

        /// <summary>
        /// 获取多个string值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        List<string> StringGetList(List<string> keys);
        
        /// <summary>
        /// 获取多个string值异步
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<List<string>> StringGetListAsync(List<string> keys);

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T StringGet<T>(string key);

        /// <summary>
        /// 获取一个key的对象异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string key);

        /// <summary>
        /// 为数值增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long StringIncrement(string key,long val=1);

        /// <summary>
        /// 为数值增长val异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> StringIncrementAsync(string key,long val=1);

        /// <summary>
        /// 为数值减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long StringDecrement(string key,long val=1);

        /// <summary>
        /// 为数值减少val异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> StringDecrementAsync(string key,long val=1);

        /// <summary>
        /// 设置Nx,只有key不存在时设置val值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        bool SetNx<T>(string key,T val);

        /// <summary>
        /// 设置Nx，只有key不存在时设置val值异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<bool> SetNxAsync<T>(string key,T val);

        #endregion


        #region hash

        /// <summary>
        /// 判断某个数据是否已经存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        bool HashExists(string key,string dataKey);

        /// <summary>
        /// 判断某个数据是否已经存在异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        Task<bool> HaskExistsAsync(string key,string dataKey);

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        bool HashSet<T>(string key,string dataKey,T t);

        /// <summary>
        /// 存储数据到hash表异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> HashSetAsync<T>(string key,string dataKey,T t);

        /// <summary>
        /// 批量设置Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dics"></param>
        /// <returns></returns>
        bool HashSetList(string key,Dictionary<string,object> dics);

        /// <summary>
        /// 批量设置Hash异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dics"></param>
        /// <returns></returns>
        Task<bool> HashSetListAsync(string key,Dictionary<string,object> dics);

        /// <summary>
        /// 移除hash中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        bool HashDelete(string key,string dataKey);

        /// <summary>
        /// 移除hash中的某个值异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string key,string dataKey);

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        long HashDeleteList(string key,List<string> dataKeys);

        /// <summary>
        /// 移除hash中的多个值异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        Task<long> HashDeleteListAsync(string key,List<string> dataKeys);

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        T HashGet<T>(string key,string dataKey);

        /// <summary>
        /// 从hash表获取数据异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        Task<T> HashGetAsync<T>(string key,string dataKey);

        /// <summary>
        /// 从hash表获取数据字典集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        Dictionary<string, T> HashGetDics<T>(string key,List<string> dataKeys);

        /// <summary>
        /// 从hash表获取数据字典集合异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        Task<Dictionary<string, T>> HashGetDicsAsync<T>(string key,List<string> dataKeys);

        /// <summary>
        /// 为hash数值增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long HashIncrement(string key,string dataKey,long val=1);

        /// <summary>
        /// 为hash数值增长val异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> HashIncrementAsync(string key,string dataKey,long val=1);

        /// <summary>
        /// 为hash数值减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long HashDecrement(string key,string dataKey,long val=1);

        /// <summary>
        /// 为hash数值减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> HashDecrementAsync(string key,string dataKey,long val=1);

        /// <summary>
        /// 获取hashkey所有redis key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<string> HashKeys(string key);

        /// <summary>
        /// 获取hashkey所有redis key异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<string>> HashKeysAsync(string key);

        /// <summary>
        /// HashSetNx 只有字段dataKey值不存在时设置hash表字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        bool HashSetNx<T>(string key,string dataKey,T val);


        #endregion


        #region list

        #endregion


        #region set

        #endregion


        #region zset(sorted set)

        /// <summary>
        /// 向有序集合添加一个成员，或者更新已存在成员的分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        long ZAdd<T>(string key,T val,double score);

        /// <summary>
        /// 向有序集合添加一个成员，或者更新以存在成员的分数异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<long> ZAddAsync<T>(string key,T val,double score);

        /// <summary>
        /// 向有序集合添加成员集合，或者更新已存在成员的分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scoreMembers"></param>
        /// <returns></returns>
        long ZAddList<T>(string key,List<(double,T)> scoreMembers);

        /// <summary>
        /// 向有序集合添加成员集合，或者更新已经存在成员的分数异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="socreMembers"></param>
        /// <returns></returns>
        Task<long> ZAddListAsync<T>(string key,List<(double,T)> socreMembers);

        /// <summary>
        /// 获取指定成员排名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        long? ZRank<T>(string key,T member);

        /// <summary>
        /// 获取指定成员排名异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<long?> ZRankAsync<T>(string key,T member);

        /// <summary>
        /// 增加指定成员分数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        decimal ZIncrBy(string key,string member,decimal increment);

        /// <summary>
        /// 增加指定成员分数异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        Task<decimal> ZIncrByAsync(string key,string member,decimal increment);

        /// <summary>
        /// 删除指定单个或多个成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        long ZRem<T>(string key,List<T> member);

        /// <summary>
        /// 删除指定单个或多个成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<long> ZRemAsync<T>(string key,List<T> member);

        /// <summary>
        /// 获取全部成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        List<T> ZRangeAll<T>(string key,int endIndex=1000);

        /// <summary>
        /// 获取全部成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        Task<List<T>> ZRangeAllAsync<T>(string key,int endIndex=1000);

        /// <summary>
        /// 获取指定索引范围内成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        List<T> ZRange<T>(string key, int startIndex, int endIndex);

        /// <summary>
        /// 获取指定索引范围内成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        Task<List<T>> ZRangeAsync<T>(string key,int startIndex,int endIndex);

        /// <summary>
        /// 获取分数降序指定索引范围内成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        List<T> ZRevRange<T>(string key,int startIndex,int endIndex);

        /// <summary>
        /// 获取分数降序指定索引范围内成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        Task<List<T>> ZRevRangeAsync<T>(string key,int startIndex,int endIndex);

        /// <summary>
        /// 判断是否存在项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        bool ZScore<T>(string key,T val);

        /// <summary>
        /// 判断是否存在项异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<bool> ZScoreAsync<T>(string key,T val);

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long ZCard(string key);

        /// <summary>
        /// 获取集合中的数量异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> ZCardAsync(string key);

        #endregion


        #region key actions

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyDelete(string key);

        /// <summary>
        /// 删除单个key异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>is successful</returns>
        Task<bool> KeyDeleteAsync(string key);

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        long KeyDelete(List<string> keys);

        /// <summary>
        /// 删除多个key异步
        /// </summary>
        /// <param name="keys">redis key</param>
        /// <returns>successful number</returns>
        Task<long> KeyDeleteAsync(List<string> keys);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyExists(string key);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExistsAsync(string key);

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newKey"></param>
        /// <returns></returns>
        bool KeyRename(string key,string newKey);

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
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool KeyExpire(string key,TimeSpan? expire=default);

        /// <summary>
        /// 设置key的过期时长异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire timespan</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExpireAsync(string key,TimeSpan? expire=default);

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool KeyExpireAt(string key,DateTime expire);

        /// <summary>
        /// 设置key过期时间异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire time</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExpireAtAsync(string key,DateTime expire);

        /// <summary>
        /// 搜索key
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        List<string> SearchKeys(string pattern);

        /// <summary>
        /// 搜索key异步
        /// </summary>
        /// <param name="pattern">表达式</param>
        /// <returns></returns>
        Task<List<string>> SearchKeysAsync(string pattern);

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
