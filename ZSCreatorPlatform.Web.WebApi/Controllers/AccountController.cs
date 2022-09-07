using CSRedisDistributed;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZSCreatorPlatform.Web.WebApi.Extensions.ActionFilters;
//using ZSCreatorPlatform.Web.WebApi.Extensions.Cache;
using ZSCreatorPlatform.Web.WebApi.Extensions.Dtos;
using ZSCreatorPlatform.Web.WebApi.Models;
using ZSCreatorPlatform.Web.WebApi.Models.Account;
using NLog;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace ZSCreatorPlatform.Web.WebApi.Controllers
{   
    [EnableCors("any")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtConfigDto _jwtConfigDto;

        private readonly IDistributedCSRedisCache _distributedCache;

        private readonly ILogger<AccountController> _logger;

        public AccountController(IOptionsMonitor<JwtConfigDto> jwtConfigDto, IDistributedCSRedisCache distributedCache,ILogger<AccountController> logger)
        {
            _jwtConfigDto = jwtConfigDto.CurrentValue;
            _distributedCache = distributedCache;
            _logger = logger;
        }


        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultContent Login([FromBody]LoginViewModel model)
        {
            var claimList = new List<Claim> {new Claim("name",model.Name),new Claim("roleid","5") };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigDto.SigningKey));
            var jwtSecurityToken = new JwtSecurityToken(
                issuer:_jwtConfigDto.Issuser,
                audience:_jwtConfigDto.Audience,
                claims: claimList,
                notBefore:DateTime.Now,
                expires:DateTime.Now.AddMinutes(_jwtConfigDto.ExpirationTime),
                signingCredentials:new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            _distributedCache.Set<string>("test",DateTime.Now.ToString(),null);
            return ResultContent.Result(200,"成功",token);
        }


        [Authorize("default")]
        [HttpGet]
        [CActionFilter]
        public async Task<ResultContent> GetUserInfoAsync()//ResultContent<string>
        {
            await Task.CompletedTask;
            _logger.LogInformation($"---------------------{nameof(GetUserInfoAsync)}");
            //throw new ArgumentNullException("参数不能为空");
            var obj = new {info="用户信息",time=DateTime.Now };
            var result= ResultContent.Result(200,"成功",obj);
            var redisV=_distributedCache.Get<string>("test");
            Console.WriteLine($"拿到redis中的值{redisV}");
            return result;
        }


    }
}
