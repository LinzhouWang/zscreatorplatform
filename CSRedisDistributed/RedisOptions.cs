using System;
using System.Collections.Generic;
using System.Text;

namespace CSRedisDistributed
{
    public class RedisOptions
    {
        public string RedisConnectionString { get; set; }

        public Func<string, string> NodeRole { get; set; }

        public string[] ConnectionStrings { get; set; }
    }

    /// <summary>
    /// redis选项新
    /// </summary>
    public class RedisNewOptions
    {
        /// <summary>
        /// 获取或设置 Redis服务器配置
        /// </summary>
        public string Server { get; set; } = "127.0.0.1";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 获取或设置 数据Key前缀
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 连接池大小
        /// </summary>
        public int PoolSize { get; set; }

        /// <summary>
        /// 获取或设置 Redis仓库
        /// </summary>
        public int DataBase { get; set; }

        /// <summary>
        /// 启用订阅
        /// </summary>
        public bool EnableSubscriber { get; set; }

        /// <summary>
        /// 默认七天
        /// </summary>
        public int TimeOut { get; set; } = 60 * 24 * 7;
    }

}
