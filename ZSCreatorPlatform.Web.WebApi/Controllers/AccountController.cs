using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private JwtConfigDto _jwtConfigDto;

        public AccountController(IOptionsMonitor<JwtConfigDto> jwtConfigDto)
        {
            _jwtConfigDto = jwtConfigDto.CurrentValue;
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
            var result= ResultContent.Result(200,"成功","用户信息");
            return result;
        }


    }
}
