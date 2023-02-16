using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.Admin.Extensions
{
    /// <summary>
    /// 默认授权验证处理
    /// </summary>
    public class DefaultAuthorizationHandler : AuthorizationHandler<DefaultAuthorizationRequirement>
    {

        /// <summary>
        /// 认证授权验证
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultAuthorizationRequirement requirement)
        {
            //认证验证
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            //授权验证


            context.Succeed(requirement);
            return;
        }
    }
}
