using System;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZSCreatorPlatform.Web.Admin.Domain;

namespace ZSCreatorPlatform.Web.Admin.Controllers
{
    public class PublishController : Controller
    {

        #region Constructor

        private readonly ICapPublisher _capPublisher;

        private readonly ILogger<PublishController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="capPublisher"></param>
        /// <param name="logger"></param>
        public PublishController(ICapPublisher capPublisher,ILogger<PublishController> logger)
        {
            this._capPublisher = capPublisher;
            this._logger = logger;
        }

        #endregion


        #region Methods
        
        /// <summary>
        /// PublishMethod
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public IActionResult EFWithTransaction([FromServices]ZSCreatorDbContext dbContext)
        {
            using (var transaction=dbContext.Database.BeginTransaction(_capPublisher,true))
            {
                _capPublisher.Publish("xxx.zscreator.com",DateTime.Now);
            }
            return Ok();
        }

        /// <summary>
        /// SubScribeMessage
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [CapSubscribe("xxx.zscreator.com")]
        public IActionResult CheckReceiveMessage(DateTime dateTime)
        {
            Console.WriteLine(dateTime);
        }

        #endregion
    }
}