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

namespace ZSCreatorPlatform.Web.WebApi.Extensions
{
    public class ApiResponseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        public ApiResponseAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未授权访问
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            //await base.HandleChallengeAsync(properties);
            Response.ContentType = "application/json";
            await Response.WriteAsync("401,未授权访问");
        }

        /// <summary>
        /// 禁止访问
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            //await base.HandleForbiddenAsync(properties);
            Response.ContentType = "application/json";
            await Response.WriteAsync("403,禁止访问");
        }

    }
}
