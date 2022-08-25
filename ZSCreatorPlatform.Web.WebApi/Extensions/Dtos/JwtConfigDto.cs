using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Extensions.Dtos
{
    public class JwtConfigDto
    {
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuser { get; set; }

        /// <summary>
        /// 接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 必须16位
        /// </summary>
        public string SigningKey { get; set; }

        /// <summary>
        /// 过期时间单位分钟
        /// </summary>
        public int ExpirationTime { get; set; }

    }
}
