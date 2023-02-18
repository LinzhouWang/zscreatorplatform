using System;

namespace ZSCreatorPlatform.Web.Admin.Extensions.CSRedisExtensions
{
    /// <summary>
    /// CSRedisCahceOptions
    /// </summary>
    public class CSRedisCacheOptions
    {
        
        public Func<string, string> NodeRule { get; set; }
        
        public string[] ConnectionStrings { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}