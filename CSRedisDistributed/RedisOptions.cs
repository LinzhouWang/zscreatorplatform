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
}
