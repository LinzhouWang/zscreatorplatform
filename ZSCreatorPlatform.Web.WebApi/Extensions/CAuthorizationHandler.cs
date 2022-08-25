using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Extensions
{
    public class CAuthorizationHandler : AuthorizationHandler<CAuthorizationRequirement>
    {

        private readonly IHttpContextAccessor _accessor;

        public CAuthorizationHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// 验证是否登录（无法具体判断是token过期还是token无效），判断操作权限
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CAuthorizationRequirement requirement)
        {
            //验证是否登录
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            //验证登录状态下，该用户权限集合


            //判断此次请求，是否在该用户权限集合内



        }

    }
}
