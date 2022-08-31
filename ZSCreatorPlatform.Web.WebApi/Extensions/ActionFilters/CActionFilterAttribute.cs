using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSCreatorPlatform.Web.WebApi.Models;

namespace ZSCreatorPlatform.Web.WebApi.Extensions.ActionFilters
{
    public class CActionFilterAttribute:ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //base.OnActionExecuting(context);
            if (!context.ModelState.IsValid)
            {
                var sb = new StringBuilder();
                var mState = context.ModelState;
                var index = 0;
                foreach (var key in mState.Keys)
                {
                    var state=mState[key];
                    if (state.Errors.Any())
                    {
                        var errMsg = state.Errors.FirstOrDefault().ErrorMessage;
                        if (index==0)
                        {
                            sb.Append($"{key}:{errMsg}");
                        }
                        else
                        {
                            sb.Append($",{key}:{errMsg}");
                        }
                        
                        index++;
                    }
                }
                context.Result = new ContentResult
                {
                    ContentType = "application/jsong",
                    Content = JsonConvert.SerializeObject(ResultContent.Result(400,"请求参数有误！",sb.ToString()))
                };
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

    }
}
