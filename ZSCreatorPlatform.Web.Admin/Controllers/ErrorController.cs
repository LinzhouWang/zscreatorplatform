using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ZSCreatorPlatform.Web.Admin.Controllers
{
    /// <summary>
    /// 错误页面控制器
    /// </summary>
    public class ErrorController : Controller
    {

        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/Error")]
        public IActionResult Index()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionFeature!=null)
            {
                
                _logger.LogError(exceptionFeature.Error,exceptionFeature.Error.Message);
                return View("Error", exceptionFeature);
            }
            
            return View();
        }

        [HttpGet("Error/NotFound/{statusCode}")]
        public IActionResult NotFound(int statusCode)
        {
            var exceptionCodeFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            return View("NotFound",exceptionCodeFeature.OriginalPath);
        }

    }
}