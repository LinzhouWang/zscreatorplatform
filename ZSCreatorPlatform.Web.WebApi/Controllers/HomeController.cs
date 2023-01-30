using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            using (var httpclient=new HttpClient())
            {
                for (int i = 0; i < 1000; i++)
                {
                    var strRes = await httpclient.GetStringAsync("http://eeeff.com");//,cancellationToken);

                    Debug.WriteLine($"-----------第{i}次--------{strRes.Substring(0, 100)}");
                    //第一种方法
                    //if (cancellationToken.IsCancellationRequested)
                    //{
                    //    break;
                    //}

                    //第二种方法
                    cancellationToken.ThrowIfCancellationRequested();
                }   
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetContentAsync()
        {
            var res = await GetHtmlAsync();
            return Content(res);
        }

        private Task<string> GetHtmlAsync()
        {
            using (var httpclient = new HttpClient())
            {
                return httpclient.GetStringAsync("http://eeeff.com");
            }
        }


        #region async thread
        /// <summary>
        /// 使用异步方法并不一定能切换到新的线程，除非你手动将他们放入新线程，换句话说异步不一定是多线程
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetStringAsync()
        {
            Debug.WriteLine($"开始前线程id:{Thread.CurrentThread.ManagedThreadId}");
            var num=await GetNumberAsync();
            Debug.WriteLine($"num----------{num}");
            Debug.WriteLine($"结束后线程id:{Thread.CurrentThread.ManagedThreadId}");
            return Content("----ok-----");
        }


        private async Task<long> GetNumberAsync()
        {
            //var num = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    num += i;
            //}
            //await Task.CompletedTask;
            //return num;

            return await Task.Run(async ()=> 
            {
                var num = 0;
                for (int i = 0; i < 10000; i++)
                {
                    num += i;
                }
               return await Task.FromResult<long>(num);
            });

        }

        private Task<long> GetNumber2Async()
        {
            //var num = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    num += i;
            //}
            //await Task.CompletedTask;
            //return num;

            return Task.Run(() =>
            {
                var num = 0;
                for (int i = 0; i < 10000; i++)
                {
                    num += i;
                }
                return Task.FromResult<long>(num);
            });

        }


        #endregion


        #region task 委托 lambda linq whenall whenany 异步中等待不要用Thread.Sleep会造成主线程阻塞，要用Task.Delay

        public async Task<IActionResult> GetTaskAsync()
        {
            var task1 = Task.Run(async ()=> 
            {
                await Task.Delay(30000);
                Debug.WriteLine("这个是任务一");
                return 1;
            });

            var task2 = Task.Run(async () =>
            {
                await Task.Delay(4000);
                Debug.WriteLine("这个是任务二");
                return 2;
            });

            var task3 = Task.Run(async () =>
            {
                await Task.Delay(5000);
                Debug.WriteLine("这个是任务三");
                return 3;
            });

            var task4 = Task.Run(async () =>
            {
                await Task.Delay(6000);
                Debug.WriteLine("这个是任务四");
                return 4;
            });


            //await Task.WaitAll();//阻塞

            //await Task.WaitAny();

            //await Task.WhenAll();

            await Task.WhenAll(new List<Task> {task1,task2,task3,task4 }).ContinueWith(t=> 
            {
                Debug.WriteLine("所有任务执行完毕！");
            });
            return Content("ok");
        }
        #endregion

        #region 关于yield return

        public IActionResult GetNumYield()
        {
            var numList = GetNumY();
            var numIntList = new List<int>();
            var numIntArray = new int[] { };
            foreach (var item in GetNumY())
            {
                Debug.WriteLine(item);
            }
            return Content(nameof(GetNumYield));
        }


        private IEnumerable<int> GetNumY()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        private async IAsyncEnumerable<int> GetNumYAsync()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }


        #endregion
    }
}
