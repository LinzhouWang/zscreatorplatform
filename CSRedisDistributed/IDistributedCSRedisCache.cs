using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// 分布式redis缓存接口
    /// </summary>
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

        /// <summary>
        /// HashSetNxAsync 只有字段dataKey值不存在时设置hash表字段的值异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<bool> HashSetNxAsync<T>(string key,string dataKey,T val);

        #endregion


        #region list

        /// <summary>
        /// 移除指定list中的某一项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long ListRemove<T>(string key,T val);

        /// <summary>
        /// 移除指定list中的某一项异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> ListRemoveAsync<T>(string key,T val);

        /// <summary>
        /// 获取指定key指定范围内的list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<T> ListRange<T>(string key,int count=1000);

        /// <summary>
        /// 获取指定key指定范围内的list异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<T>> ListRangeAsync<T>(string key, int count = 1000);

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long ListRightPush<T>(string key,T val);

        /// <summary>
        /// 入队异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key,T val);

        /// <summary>
        /// 批量入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        long ListRightPush<T>(string key,List<T> vals);

        /// <summary>
        /// 批量入队异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key,List<T> vals);

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ListRightPop<T>(string key);

        /// <summary>
        /// 出对异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string key);

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string key,T val);

        /// <summary>
        /// 入栈异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key,T val);

        /// <summary>
        /// 批量入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string key,List<T> vals);

        /// <summary>
        /// 批量入栈异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key,List<T> vals);

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ListLeftPop<T>(string key);

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string key);

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long ListLength(string key);

        /// <summary>
        /// 获取集合中的数量异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string key);

        /// <summary>
        /// 对一个列表进行裁剪，只保留指定区间内元素，不包含在区间内的元素进行删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <param name="stop">结束位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <returns></returns>
        bool ListTrim(string key,long start,long stop);

        /// <summary>
        /// 对一个列表进行裁剪，只保留指定区间内元素，不包含在区间内的元素进行删除异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <param name="stop">结束位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <returns></returns>
        Task<bool> ListTrimAsync(string key,long start,long stop);

        /// <summary>
        /// 获取集合内随机值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetListRandMember(string key);

        /// <summary>
        /// 获取集合内随机值异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetListRandMemberAsync(string key);

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

    /// <summary>
    /// 分布式redis缓存服务
    /// </summary>
    public class DistributedRedisCache : IDistributedRedisCache
    {

        #region string

        /// <summary>
        /// 保存单个string值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expire = default)
        {
            return RedisHelper.Set(key, value, expire.HasValue ? Convert.ToInt32(expire.Value.TotalSeconds) : -1);
        }

        /// <summary>
        /// 保存单个string值异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expire = default)
        {
            var res = await RedisHelper.SetAsync(key, value, expire.HasValue ? Convert.ToInt32(expire.Value.TotalSeconds) : -1);
            return res;
        }

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T obj, TimeSpan? expire = default)
        {
            return RedisHelper.Set(key, obj, expire.HasValue ? Convert.ToInt32(expire.Value.TotalSeconds) : -1);
        }

        /// <summary>
        /// 保存单个对象异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expire = default)
        {
            var res = await RedisHelper.SetAsync(key, obj, expire.HasValue ? Convert.ToInt32(expire.Value.TotalSeconds) : -1);
            return res;
        }

        /// <summary>
        /// 获取单个string值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            return RedisHelper.Get(key);
        }

        /// <summary>
        /// 获取单个string值异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            var res =await RedisHelper.GetAsync(key);
            return res;
        }

        /// <summary>
        /// 获取多个string值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<string> StringGetList(List<string> keys)
        {
            return RedisHelper.MGet(keys.ToArray()).ToList();
        }

        /// <summary>
        /// 获取多个string值异步
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<List<string>> StringGetListAsync(List<string> keys)
        {
            var res = await RedisHelper.MGetAsync(keys.ToArray());
            return res.ToList();
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            var res = RedisHelper.Get<T>(key);
            return res;
        }

        /// <summary>
        /// 获取一个key的对象异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key)
        {
            var res = await RedisHelper.GetAsync<T>(key);
            return res;
        }

        /// <summary>
        /// 为数值增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public long StringIncrement(string key, long val = 1)
        {
            return RedisHelper.IncrBy(key,val);
        }

        /// <summary>
        /// 为数值增长val异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<long> StringIncrementAsync(string key, long val = 1)
        {
            var res = await RedisHelper.IncrByAsync(key,val);
            return res;
        }

        /// <summary>
        /// 为数值减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public long StringDecrement(string key, long val = 1)
        {
            return RedisHelper.IncrBy(key,0-val);
        }

        /// <summary>
        /// 为数值减少val异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<long> StringDecrementAsync(string key, long val = 1)
        {
            var res = await RedisHelper.IncrByAsync(key,0-val);
            return res;
        }

        /// <summary>
        /// 设置Nx,只有key不存在时设置val值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetNx<T>(string key, T val)
        {
            return RedisHelper.SetNx(key,val);
        }

        /// <summary>
        /// 设置Nx，只有key不存在时设置val值异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> SetNxAsync<T>(string key, T val)
        {
            var res =await RedisHelper.SetNxAsync(key,val);
            return res;
        }

        #endregion


        #region hash

        /// <summary>
        /// 判断某个数据是否已经存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashExists(string key, string dataKey)
        {
            return RedisHelper.HExists(key,dataKey);
        }

        /// <summary>
        /// 判断某个数据是否已经存在异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HaskExistsAsync(string key, string dataKey)
        {
            var res = await RedisHelper.HExistsAsync(key,dataKey);
            return res;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string dataKey, T t)
        {
            var res = RedisHelper.HSet(key,dataKey,t);
            return res;
        }

        /// <summary>
        /// 存储数据到hash表异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string dataKey, T t)
        {
            var res = await RedisHelper.HSetAsync(key,dataKey,t);
            return res;
        }

        /// <summary>
        /// 批量设置Hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dics"></param>
        /// <returns></returns>
        public bool HashSetList(string key, Dictionary<string, object> dics)
        {
            var keyValues = new List<object>();
            foreach (var item in dics)
            {
                keyValues.Add(new { item.Key, item.Value });//.AddRange(new List<object> { new { item.Key, item.Value } });
            }
            var res = RedisHelper.HMSet(key,keyValues.ToArray());
            return res;
        }

        /// <summary>
        /// 批量设置Hash异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dics"></param>
        /// <returns></returns>
        public async Task<bool> HashSetListAsync(string key, Dictionary<string, object> dics)
        {
            var keyValues = new List<object>();
            foreach (var item in dics)
            {
                keyValues.Add(new { item.Key,item.Value});
            }
            var res =await RedisHelper.HMSetAsync(key,keyValues.ToArray());
            return res;
        }

        /// <summary>
        /// 移除hash中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string dataKey)
        {
            var res = RedisHelper.HDel(key,dataKey)>=0;
            return res;
        }

        /// <summary>
        /// 移除hash中的某个值异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string dataKey)
        {
            var res = await RedisHelper.HDelAsync(key,dataKey);
            return res >= 0;
        }

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long HashDeleteList(string key, List<string> dataKeys)
        {
            var res = RedisHelper.HDel(key,dataKeys.ToArray());
            return res;
        }

        /// <summary>
        /// 移除hash中的多个值异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteListAsync(string key, List<string> dataKeys)
        {
            var res = await RedisHelper.HDelAsync(key,dataKeys.ToArray());
            return res;
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string dataKey)
        {
            var res = RedisHelper.HGet<T>(key,dataKey);
            return res;
        }

        /// <summary>
        /// 从hash表获取数据异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string dataKey)
        {
            var res = await RedisHelper.HGetAsync<T>(key,dataKey);
            return res;
        }

        /// <summary>
        /// 从hash表获取数据字典集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public Dictionary<string, T> HashGetDics<T>(string key, List<string> dataKeys)
        {
            if (dataKeys==null||dataKeys.Count<=0)
            {
                return RedisHelper.HGetAll<T>(key);
            }

            dataKeys = dataKeys.Distinct().ToList();
            var values = RedisHelper.HMGet<T>(key,dataKeys.ToArray());
            var dics = new Dictionary<string, T>();
            for (int i = 0; i < dataKeys.Count; i++)
            {
                if (!dics.ContainsKey(dataKeys[i])&&values[i]!=null)
                {
                    dics.Add(dataKeys[i],values[i]);
                }
            }
            return dics;
        }

        /// <summary>
        /// 从hash表获取数据字典集合异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetDicsAsync<T>(string key, List<string> dataKeys)
        {
            if (dataKeys == null || dataKeys.Count <= 0)
            {
                return RedisHelper.HGetAll<T>(key);
            }

            dataKeys = dataKeys.Distinct().ToList();
            var values =await RedisHelper.HMGetAsync<T>(key, dataKeys.ToArray());
            var dics = new Dictionary<string, T>();
            for (int i = 0; i < dataKeys.Count; i++)
            {
                if (!dics.ContainsKey(dataKeys[i]) && values[i] != null)
                {
                    dics.Add(dataKeys[i], values[i]);
                }
            }
            return dics;
        }

        /// <summary>
        /// 为hash数值增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public long HashIncrement(string key, string dataKey, long val = 1)
        {
            return RedisHelper.HIncrBy(key,dataKey,val);
        }

        /// <summary>
        /// 为hash数值增长val异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<long> HashIncrementAsync(string key, string dataKey, long val = 1)
        {
            var res = await RedisHelper.HIncrByAsync(key,dataKey,val);
            return res;
        }

        /// <summary>
        /// 为hash数值减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public long HashDecrement(string key, string dataKey, long val = 1)
        {
            var res = RedisHelper.HIncrBy(key,dataKey,0-val);
            return res;
        }

        /// <summary>
        /// 为hash数值减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<long> HashDecrementAsync(string key, string dataKey, long val = 1)
        {
            var res = await RedisHelper.HIncrByAsync(key,dataKey,0-val);
            return res;
        }

        /// <summary>
        /// 获取hashkey所有redis key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashKeys(string key)
        {
            var res = RedisHelper.Keys(key).ToList();
            return res;
        }

        /// <summary>
        /// 获取hashkey所有redis key异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> HashKeysAsync(string key)
        {
            var res = await RedisHelper.KeysAsync(key);
            return res.ToList();
        }

        /// <summary>
        /// HashSetNx 只有字段dataKey值不存在时设置hash表字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool HashSetNx<T>(string key, string dataKey, T val)
        {
            var res = RedisHelper.HSetNx(key,dataKey,val);
            return res;
        }

        /// <summary>
        /// HashSetNxAsync 只有字段dataKey值不存在时设置hash表字段的值异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> HashSetNxAsync<T>(string key, string dataKey, T val)
        {
            var res = await RedisHelper.HSetNxAsync(key,dataKey,val);
            return res;
        }

        #endregion


        #region list

        /// <summary>
        /// 移除指定list中的某一项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long ListRemove<T>(string key, T val);

        /// <summary>
        /// 移除指定list中的某一项异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> ListRemoveAsync<T>(string key, T val);

        /// <summary>
        /// 获取指定key指定范围内的list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<T> ListRange<T>(string key, int count = 1000);

        /// <summary>
        /// 获取指定key指定范围内的list异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<T>> ListRangeAsync<T>(string key, int count = 1000);

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long ListRightPush<T>(string key, T val);

        /// <summary>
        /// 入队异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key, T val);

        /// <summary>
        /// 批量入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        long ListRightPush<T>(string key, List<T> vals);

        /// <summary>
        /// 批量入队异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key, List<T> vals);

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ListRightPop<T>(string key);

        /// <summary>
        /// 出对异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string key);

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string key, T val);

        /// <summary>
        /// 入栈异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key, T val);

        /// <summary>
        /// 批量入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        long ListLeftPush<T>(string key, List<T> vals);

        /// <summary>
        /// 批量入栈异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key, List<T> vals);

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ListLeftPop<T>(string key);

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string key);

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long ListLength(string key);

        /// <summary>
        /// 获取集合中的数量异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string key);

        /// <summary>
        /// 对一个列表进行裁剪，只保留指定区间内元素，不包含在区间内的元素进行删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <param name="stop">结束位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <returns></returns>
        bool ListTrim(string key, long start, long stop);

        /// <summary>
        /// 对一个列表进行裁剪，只保留指定区间内元素，不包含在区间内的元素进行删除异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <param name="stop">结束位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <returns></returns>
        Task<bool> ListTrimAsync(string key, long start, long stop);

        /// <summary>
        /// 获取集合内随机值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetListRandMember(string key);

        /// <summary>
        /// 获取集合内随机值异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetListRandMemberAsync(string key);

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
        long ZAdd<T>(string key, T val, double score);

        /// <summary>
        /// 向有序集合添加一个成员，或者更新以存在成员的分数异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<long> ZAddAsync<T>(string key, T val, double score);

        /// <summary>
        /// 向有序集合添加成员集合，或者更新已存在成员的分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scoreMembers"></param>
        /// <returns></returns>
        long ZAddList<T>(string key, List<(double, T)> scoreMembers);

        /// <summary>
        /// 向有序集合添加成员集合，或者更新已经存在成员的分数异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="socreMembers"></param>
        /// <returns></returns>
        Task<long> ZAddListAsync<T>(string key, List<(double, T)> socreMembers);

        /// <summary>
        /// 获取指定成员排名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        long? ZRank<T>(string key, T member);

        /// <summary>
        /// 获取指定成员排名异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<long?> ZRankAsync<T>(string key, T member);

        /// <summary>
        /// 增加指定成员分数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        decimal ZIncrBy(string key, string member, decimal increment);

        /// <summary>
        /// 增加指定成员分数异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        Task<decimal> ZIncrByAsync(string key, string member, decimal increment);

        /// <summary>
        /// 删除指定单个或多个成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        long ZRem<T>(string key, List<T> member);

        /// <summary>
        /// 删除指定单个或多个成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<long> ZRemAsync<T>(string key, List<T> member);

        /// <summary>
        /// 获取全部成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        List<T> ZRangeAll<T>(string key, int endIndex = 1000);

        /// <summary>
        /// 获取全部成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        Task<List<T>> ZRangeAllAsync<T>(string key, int endIndex = 1000);

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
        Task<List<T>> ZRangeAsync<T>(string key, int startIndex, int endIndex);

        /// <summary>
        /// 获取分数降序指定索引范围内成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        List<T> ZRevRange<T>(string key, int startIndex, int endIndex);

        /// <summary>
        /// 获取分数降序指定索引范围内成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        Task<List<T>> ZRevRangeAsync<T>(string key, int startIndex, int endIndex);

        /// <summary>
        /// 判断是否存在项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        bool ZScore<T>(string key, T val);

        /// <summary>
        /// 判断是否存在项异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        Task<bool> ZScoreAsync<T>(string key, T val);

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
        bool KeyRename(string key, string newKey);

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="newKey">rename redis key</param>
        /// <returns>is successful</returns>
        Task<bool> KeyRenameAsync(string key, string newKey);

        /// <summary>
        /// 设置key的过期时长
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool KeyExpire(string key, TimeSpan? expire = default);

        /// <summary>
        /// 设置key的过期时长异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire timespan</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExpireAsync(string key, TimeSpan? expire = default);

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        bool KeyExpireAt(string key, DateTime expire);

        /// <summary>
        /// 设置key过期时间异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire time</param>
        /// <returns>is successful</returns>
        Task<bool> KeyExpireAtAsync(string key, DateTime expire);

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
        void SubScribe(string channel, Action<string> handler = null);

        /// <summary>
        /// 用于将信息发送到指定分区节点的频道，最终消息发布格式：1|message
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<long> PublishAsync(string channel, string msg);

        #endregion

    }

}
