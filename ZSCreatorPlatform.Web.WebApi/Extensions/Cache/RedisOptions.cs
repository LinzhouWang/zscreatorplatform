using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Extensions.Cache
{
    public class RedisOptions
    {
        public string RedisConnectionString { get; set; }

        public Func<string, string> NodeRole { get; set; }

        public string[] ConnectionStrings { get; set; }

    }
}
