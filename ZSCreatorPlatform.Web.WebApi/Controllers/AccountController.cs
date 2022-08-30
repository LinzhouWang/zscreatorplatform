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
using ZSCreatorPlatform.Web.WebApi.Extensions.Dtos;
using ZSCreatorPlatform.Web.WebApi.Models;
using ZSCreatorPlatform.Web.WebApi.Models.Account;

namespace ZSCreatorPlatform.Web.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtConfigDto _jwtConfigDto;

        private readonly IDistributedCache _distributedCache;

        public AccountController(IOptionsMonitor<JwtConfigDto> jwtConfigDto,IDistributedCache distributedCache)
        {
            _jwtConfigDto = jwtConfigDto.CurrentValue;
            _distributedCache = distributedCache;
        }


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
            return ResultContent.Result(200,"成功",token);
        }


        [Authorize("default")]
        public async Task<ResultContent> GetUserInfoAsync()//ResultContent<string>
        {
            await Task.CompletedTask;

            var test1 = new Test1 
            {
                Name="111",
                Age=22,
                Address="zz"
            };

            var testJson1 = JsonConvert.SerializeObject(test1);

            var test2 = JsonConvert.DeserializeObject<Test2>(testJson1);

            Console.WriteLine($"姓名:{test2.Name},年龄:{test2.Age}");

            var testJson2 = JsonConvert.SerializeObject(test2);

            var test11 = JsonConvert.DeserializeObject<Test1>(testJson2);
            Console.WriteLine($"姓名：{test11.Name},年龄：{test11.Age},地址：{test11.Address}");

            var result= ResultContent.Result(200,"成功","用户信息");
            
            return result;
        }

        public class Test1
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public string Address { get; set; }
        }

        public class Test2
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }


    }
}
