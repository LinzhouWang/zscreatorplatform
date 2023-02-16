using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZSCreatorPlatform.Web.Admin.Models.Account;

namespace ZSCreatorPlatform.Web.Admin.Controllers
{
    public class AccountController : Controller
    {

        #region Contors

        public AccountController()
        {

        }
        #endregion


        #region Methods

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model)
        {
            if (model==null)//model.IsValid()
            {
                return new JsonResult(new { code=401,msg="账号密码不能为空！"});
            }
            //判断
            var claims = new List<Claim> 
            {
                new Claim("name","zhisen"),
                new Claim("role","admin"),
                new Claim("roleid","001")
            };
            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            return new JsonResult(new { code=200,msg="登录成功"});
        }

        /// <summary>
        /// 登出页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            return View();
        }

        /// <summary>
        /// 登出方法
        /// </summary>
        /// <returns></returns>
        [Authorize("default")]
        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult(new { code=200,msg="登出成功"});
        }

        /// <summary>
        /// 访问禁止页面
        /// </summary>
        /// <returns></returns>
        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion



    }
}
