using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.Admin.Extensions
{
    /// <summary>
    /// 自定义授权声明
    /// </summary>
    public class DefaultAuthorizationRequirement:IAuthorizationRequirement
    {
    }
}
