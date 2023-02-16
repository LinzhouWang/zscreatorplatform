using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.Admin.Extensions
{
    public class DefaultAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public DefaultAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            :base(options,logger,encoder,clock)
        { 
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未认证访问
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            //await base.HandleChallengeAsync(properties);
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(new { code=401,msg="没有访问权限，请先进行身份验证"}));

        }

        /// <summary>
        /// 请求被禁止
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            //await base.HandleForbiddenAsync(properties);
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(new { code=403,msg="请求被禁止"}));
        }

    }
}
