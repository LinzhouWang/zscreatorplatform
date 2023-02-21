using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using ZSCreatorPlatform.Web.Admin.Models.Account;

namespace ZSCreatorPlatform.Web.Admin.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    public class AccountController : Controller
    {

        #region Contors

        private readonly IMemoryCache _memoryCache;

        private readonly IDistributedCache _distributedCache;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memoryCache"></param>
        public AccountController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        // public AccountController(IMemoryCache memoryCache,IDistributedCache distributedCache)
        // {
        //     _memoryCache = memoryCache;
        //     _distributedCache = distributedCache;
        // }
        
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
            var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
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

        /// <summary>
        /// 测试页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            #region memorycache

            string zscreator_val;
            _memoryCache.TryGetValue("zscreator_key",out zscreator_val);
            if (string.IsNullOrWhiteSpace(zscreator_val))
            {
                _memoryCache.GetOrCreate("zscreator_key", cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(5);
                    return DateTime.Now;
                });    
            }

            _memoryCache.Set("zscreator_key",DateTime.Now.ToString(),TimeSpan.FromMinutes(5));
            

            #endregion

            #region distributedcache
            //_distributedCache.SetString();
            RedisHelper.HSet("zscreatorplatform","key_center","哈哈哈哈");
            
            #endregion
            
            
            return Content("zscreator_memorycache_test");
        }


        #endregion



    }
}
