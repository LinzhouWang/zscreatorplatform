using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CSRedisCache:ICSRedisCache,IDisposable
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

        #region extension methods

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
        public long ListRemove<T>(string key, T val)
        {
            return RedisHelper.LRem(key,int.MaxValue,val);
        }

        /// <summary>
        /// 移除指定list中的某一项异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<long> ListRemoveAsync<T>(string key, T val)
        {
            var res = await RedisHelper.LRemAsync(key,int.MaxValue,val);
            return res;
        }

        /// <summary>
        /// 获取指定key指定范围内的list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key, int count = 1000)
        {
            return RedisHelper.LRange<T>(key,0,count).ToList();
        }

        /// <summary>
        /// 获取指定key指定范围内的list异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRangeAsync<T>(string key, int count = 1000)
        {
            var res = await RedisHelper.LRangeAsync<T>(key,0,count);
            return res.ToList();
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, T val)
        {
            return RedisHelper.RPush<T>(key,val);
        }

        /// <summary>
        /// 入队异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, T val)
        {
            var res = await RedisHelper.RPushAsync<T>(key,val);
            return res;
        }

        /// <summary>
        /// 批量入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string key, List<T> vals)
        {
            return RedisHelper.RPush(key,vals);
        }

        /// <summary>
        /// 批量入队异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, List<T> vals)
        {
            var res = await RedisHelper.RPushAsync(key,vals);
            return res;
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            return RedisHelper.RPop<T>(key);
        }

        /// <summary>
        /// 出对异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key)
        {
            var res = await RedisHelper.RPopAsync<T>(key);
            return res;
        }

        /// <summary>
        /// 入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, T val)
        {
            return RedisHelper.LPush<T>(key,val);
        }

        /// <summary>
        /// 入栈异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string key, T val)
        {
            var res = await RedisHelper.LPushAsync<T>(key,val);
            return res;
        }

        /// <summary>
        /// 批量入栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string key, List<T> vals)
        {
            return RedisHelper.LPush(key,vals);
        }

        /// <summary>
        /// 批量入栈异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string key, List<T> vals)
        {
            var res = await RedisHelper.LPushAsync(key,vals);
            return res;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            return RedisHelper.LPop<T>(key);
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            var res = await RedisHelper.LPopAsync<T>(key);
            return res;
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return RedisHelper.LLen(key);
        }

        /// <summary>
        /// 获取集合中的数量异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key)
        {
            var res = await RedisHelper.LLenAsync(key);
            return res;
        }

        /// <summary>
        /// 对一个列表进行裁剪，只保留指定区间内元素，不包含在区间内的元素进行删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <param name="stop">结束位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <returns></returns>
        public bool ListTrim(string key, long start, long stop)
        {
            return RedisHelper.LTrim(key,start,stop);
        }

        /// <summary>
        /// 对一个列表进行裁剪，只保留指定区间内元素，不包含在区间内的元素进行删除异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <param name="stop">结束位置，0代表第一个元素，-1代表最后一个元素</param>
        /// <returns></returns>
        public async Task<bool> ListTrimAsync(string key, long start, long stop)
        {
            var res = await RedisHelper.LTrimAsync(key,start,stop);
            return res;
        }

        /// <summary>
        /// 获取集合内随机值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetListRandMember(string key)
        {
            return RedisHelper.SRandMember(key);
        }

        /// <summary>
        /// 获取集合内随机值异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetListRandMemberAsync(string key)
        {
            var res = await RedisHelper.SRandMemberAsync(key);
            return res;
        }

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
        public long ZAdd<T>(string key, T val, double score)
        {
            return RedisHelper.ZAdd(key,(Convert.ToDecimal(score),val));
        }

        /// <summary>
        /// 向有序集合添加一个成员，或者更新以存在成员的分数异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<long> ZAddAsync<T>(string key, T val, double score)
        {
            return await RedisHelper.ZAddAsync(key,(Convert.ToDecimal(score),val));
        }

        /// <summary>
        /// 向有序集合添加成员集合，或者更新已存在成员的分数
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="key"></param>
        /// <param name="scoreMembers"></param>
        /// <returns></returns>
        public long ZAddList(string key, List<(decimal, object)> scoreMembers)
        {
            return RedisHelper.ZAdd(key,scoreMembers.ToArray());
        }

        /// <summary>
        /// 向有序集合添加成员集合，或者更新已经存在成员的分数异步
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="key"></param>
        /// <param name="socreMembers"></param>
        /// <returns></returns>
        public async Task<long> ZAddListAsync(string key, List<(decimal, object)> socreMembers)
        {
            return await RedisHelper.ZAddAsync(key, socreMembers.ToArray());
        }

        /// <summary>
        /// 获取指定成员排名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long? ZRank<T>(string key, T member)
        {
            return RedisHelper.ZRank(key,member);
        }

        /// <summary>
        /// 获取指定成员排名异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<long?> ZRankAsync<T>(string key, T member)
        {
            return await RedisHelper.ZRankAsync(key,member);
        }

        /// <summary>
        /// 增加指定成员分数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public decimal ZIncrBy(string key, string member, decimal increment)
        {
            return RedisHelper.ZIncrBy(key,member,increment);
        }

        /// <summary>
        /// 增加指定成员分数异步
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public async Task<decimal> ZIncrByAsync(string key, string member, decimal increment)
        {
            return await RedisHelper.ZIncrByAsync(key,member,increment);
        }

        /// <summary>
        /// 删除指定单个或多个成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long ZRem<T>(string key, List<T> member)
        {
            return RedisHelper.ZRem<T>(key,member.ToArray());
        }

        /// <summary>
        /// 删除指定单个或多个成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<long> ZRemAsync<T>(string key, List<T> member)
        {
            return await RedisHelper.ZRemAsync<T>(key,member.ToArray());
        }

        /// <summary>
        /// 获取全部成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<T> ZRangeAll<T>(string key, int endIndex = 1000)
        {
            return RedisHelper.ZRange<T>(key,0,endIndex).ToList();
        }

        /// <summary>
        /// 获取全部成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public async Task<List<T>> ZRangeAllAsync<T>(string key, int endIndex = 1000)
        {
            var res = await RedisHelper.ZRangeAsync<T>(key, 0, endIndex);
            return res.ToList();
        }

        /// <summary>
        /// 获取指定索引范围内成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<T> ZRange<T>(string key, int startIndex, int endIndex)
        {
            return RedisHelper.ZRange<T>(key,startIndex,endIndex).ToList();
        }

        /// <summary>
        /// 获取指定索引范围内成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public async Task<List<T>> ZRangeAsync<T>(string key, int startIndex, int endIndex)
        {
            var res = await RedisHelper.ZRangeAsync<T>(key,startIndex,endIndex);
            return res.ToList();
        }

        /// <summary>
        /// 获取分数降序指定索引范围内成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<T> ZRevRange<T>(string key, int startIndex, int endIndex)
        {
            return RedisHelper.ZRevRange<T>(key,startIndex,endIndex).ToList();
        }

        /// <summary>
        /// 获取分数降序指定索引范围内成员异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public async Task<List<T>> ZRevRangeAsync<T>(string key, int startIndex, int endIndex)
        {
            var res =await RedisHelper.ZRevRangeAsync<T>(key,startIndex,endIndex);
            return res.ToList();
        }

        /// <summary>
        /// 判断是否存在项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool ZScore<T>(string key, T val)
        {
            return RedisHelper.ZScore(key,val).HasValue;
        }

        /// <summary>
        /// 判断是否存在项异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<bool> ZScoreAsync<T>(string key, T val)
        {
            var res = await RedisHelper.ZScoreAsync(key,val);
            return res.HasValue;
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ZCard(string key)
        {
            return RedisHelper.ZCard(key);
        }

        /// <summary>
        /// 获取集合中的数量异步
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ZCardAsync(string key)
        {
            var res = await RedisHelper.ZCardAsync(key);
            return res;
        }

        #endregion


        #region key actions

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            return RedisHelper.Del(key) > 0;
        }

        /// <summary>
        /// 删除单个key异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>is successful</returns>
        public async Task<bool> KeyDeleteAsync(string key)
        {
            var res = await RedisHelper.DelAsync(key) > 0;
            return res;
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long KeyDelete(List<string> keys)
        {
            return RedisHelper.Del(keys.ToArray());
        }

        /// <summary>
        /// 删除多个key异步
        /// </summary>
        /// <param name="keys">redis key</param>
        /// <returns>successful number</returns>
        public async Task<long> KeyDeleteAsync(List<string> keys)
        {
            var res = await RedisHelper.DelAsync(keys.ToArray());
            return res;
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return RedisHelper.Exists(key);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>is successful</returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            var res = await RedisHelper.ExistsAsync(key);
            return res;
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newKey"></param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey)
        {
            return RedisHelper.Rename(key,newKey);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="newKey">rename redis key</param>
        /// <returns>is successful</returns>
        public async Task<bool> KeyRenameAsync(string key, string newKey)
        {
            var res = await RedisHelper.RenameAsync(key,newKey);
            return res;
        }

        /// <summary>
        /// 设置key的过期时长
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expire = default)
        {
            return RedisHelper.Expire(key,expire.HasValue?Convert.ToInt32(expire.Value.TotalSeconds):-1);
        }

        /// <summary>
        /// 设置key的过期时长异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire timespan</param>
        /// <returns>is successful</returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expire = default)
        {
            var res = await RedisHelper.ExpireAsync(key,expire.HasValue?Convert.ToInt32(expire.Value.TotalSeconds):-1);
            return res;
        }

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public bool KeyExpireAt(string key, DateTime expire)
        {
            return RedisHelper.ExpireAt(key,expire);
        }

        /// <summary>
        /// 设置key过期时间异步
        /// </summary>
        /// <param name="key">redis key</param>
        /// <param name="expire">expire time</param>
        /// <returns>is successful</returns>
        public async Task<bool> KeyExpireAtAsync(string key, DateTime expire)
        {
            return await RedisHelper.ExpireAtAsync(key,expire);
        }

        /// <summary>
        /// 搜索key
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public List<string> SearchKeys(string pattern)
        {
            return RedisHelper.Keys(pattern).ToList();
        }

        /// <summary>
        /// 搜索key异步
        /// </summary>
        /// <param name="pattern">表达式</param>
        /// <returns></returns>
        public async Task<List<string>> SearchKeysAsync(string pattern)
        {
            var res= await RedisHelper.KeysAsync(pattern);
            return res.ToList();
        }

        #endregion


        #region subscribe/publish

        /// <summary>
        /// 订阅，根据分区规则返回SubscribeObject，Subscribe(("chan1", msg => Console.WriteLine(msg.Body)),("chan2", msg => Console.WriteLine(msg.Body)))
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handler"></param>
        public void SubScribe(string channel, Action<string> handler = null)
        {
            RedisHelper.Subscribe((channel,msg=>handler.Invoke(msg.Body)));
        }

        /// <summary>
        /// 用于将信息发送到指定分区节点的频道，最终消息发布格式：1|message
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task<long> PublishAsync(string channel, string msg)
        {
            return await RedisHelper.PublishAsync(channel,msg);
        }

        #endregion


        #endregion

        public void Dispose()
        {
            _csRedisClient.Dispose();
        }
    }
}