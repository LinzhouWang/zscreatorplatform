using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ZSCreatorPlatform.Web.WebApi.Models;

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
            await Response.WriteAsync(JsonConvert.SerializeObject(ResultContent.Result(401,"访问未授权","")
                ,new JsonSerializerSettings() 
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateFormatString = "yyyy/MM/dd HH:mm:ss",//兼容ios"yyyy-MM-dd HH:mm:ss"不支持
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
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
            await Response.WriteAsync(JsonConvert.SerializeObject(ResultContent.Result(403,"请求被禁止","")
                , new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateFormatString = "yyyy/MM/dd HH:mm:ss",//兼容ios"yyyy-MM-dd HH:mm:ss"不支持
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }

    }
}
