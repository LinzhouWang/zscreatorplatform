using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ZSCreatorPlatform.Web.WebApi.Models;

namespace ZSCreatorPlatform.Web.WebApi.Extensions.Exception
{

    public class CExceptionFilter : IExceptionFilter
    {

        //private readonly ILogger _logger;

        //public CExceptionFilter(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public CExceptionFilter() { }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                //日记记录具体错误信息
                Console.WriteLine($"---{nameof(CExceptionFilter)}---");
                Console.WriteLine($"{context.Exception.Message}");

                context.ExceptionHandled = true;
                context.Result = new ContentResult
                {
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject
                    (ResultContent.Result((int)HttpStatusCode.InternalServerError, "系统错误，请稍后重试", "")),
                    StatusCode=(int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
