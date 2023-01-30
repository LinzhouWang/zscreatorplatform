using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ZSCreatorPlatform
{
    class Program
    {

        public class Emp
        {
            public int StoreId { get; set; }

            public int EmployeeId { get; set; }

            public int Number { get; set; }

        }

        static async Task Main(string[] args)
        {

            var testEmps = new List<Emp> { new Emp {StoreId=1,EmployeeId=11,Number=1 },new Emp {StoreId=1,EmployeeId=12,Number=2 },new Emp {StoreId=2,EmployeeId=22,Number=4 } };
            var num=testEmps.Sum(x => x.Number);

            var dics = new Dictionary<string, object>();

            var dics22 = new Dictionary<string, object>();
            //var emps=testEmps.GroupBy(x=>x.StoreId).ToList()
            //    .ForEach(g=> { dics.Add(g.Key.ToString(),g.SelectMany(x=>g)); });

            var emps = testEmps.GroupBy(x => x.StoreId).ToList();
            foreach (var item in emps)
            {
                var emp11 = item.Select(x => new Emp { StoreId = x.StoreId, EmployeeId = x.EmployeeId }).ToList();
                var emp22 = item.ToList();
                var testNum = item.Sum(x=>x.Number);
                dics.Add(item.Key.ToString(),emp11);
                dics22.Add(item.Key.ToString(),emp22);
            }


            Console.ReadKey();
            return;

            #region Configuration
            var servicesNow = new ServiceCollection();
            using (var spNow = servicesNow.BuildServiceProvider())
            {
                



            }
            
            


            #endregion

            #region Task

            Console.WriteLine($"---当前线程:{Thread.CurrentThread.ManagedThreadId}---");

            await Task.Run(()=> 
            {
                Console.WriteLine($"我是任务1，线程:{Thread.CurrentThread.ManagedThreadId}--");
            });

            await Task.Run(()=> 
            {
                Console.WriteLine($"我是任务2，线程:{Thread.CurrentThread.ManagedThreadId}--");
            });

            await Task.Run(()=> 
            {
                Console.WriteLine($"我是任务3，线程:{Thread.CurrentThread.ManagedThreadId}--");
            });

            Console.WriteLine($"---结束线程:{Thread.CurrentThread.ManagedThreadId}---");

            #endregion

            #region 依赖注入  控制反转
            //ServiceCollection服务容器，服务定位器ServiceLocator

            //框架负责注入,Transient,Scoped,Singleton
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<Controller>();

            //带参数注入
            //services.AddScoped(new ParamService("哈哈哈"));

            //不想写的太复杂，只想要要啥给啥，自定义扩展方法
            //services.AddAuthentication();
            //using (var sp = services.BuildServiceProvider())
            //{
            //    var controller = sp.GetService<Controller>();
            //    controller.SendEmailTo();
            //}


            //从代码层面理解transient（瞬时）  scoped（范围内）  singleton（单例）
            //object.ReferenceEquals();判断引用类型是否相等

            //services.AddTransient<IAnimalService, EmployeeService>();

            //services.AddScoped<IAnimalService, EmployeeService>();

            services.AddSingleton<IAnimalService, EmployeeService>();

            using (var sp1=services.BuildServiceProvider())
            {

                #region Transient 说明瞬时每次获取不一样
                //IAnimalService ani = sp1.GetService<IAnimalService>();
                //ani.Name = "哈哈";
                //ani.SayHello();

                //IAnimalService ani2 = sp1.GetService<IAnimalService>();
                //ani2.Name = "拉拉";
                //ani2.SayHello();

                //ani.SayHello();

                //var res = object.ReferenceEquals(ani, ani2);

                //Console.WriteLine($"Transient两次服务获取是否相等:{res}");//false 
                #endregion

                #region scoped请求范围内一致，不同请求范围不一致

                //IAnimalService ani;
                //IAnimalService ani22;
                //using (var sp11 = sp1.CreateScope())
                //{
                //    ani = sp11.ServiceProvider.GetService<IAnimalService>();
                //    ani.Name = "哈哈哈";
                //    ani.SayHello();

                //    //IAnimalService ani2 = sp11.ServiceProvider.GetService<IAnimalService>();
                //    //ani2.Name = "啦啦啦";
                //    //ani2.SayHello();

                //    //ani.SayHello();

                //    //var res = ""; //object.ReferenceEquals(ani, ani2);
                //    //Console.WriteLine($"Scoped两次服务获取是否相等:{res}");

                //}

                //using (var sp22=sp1.CreateScope())
                //{
                //    ani22 = sp22.ServiceProvider.GetService<IAnimalService>();
                //    ani22.Name = "啦啦啦";
                //    ani22.SayHello();
                //}

                //var res=object.ReferenceEquals(ani,ani22);

                //Console.WriteLine($"两次范围内服务是否相等:{res}");


                #endregion





            }

            //读取配置类型文件ini，配置文件根据环境重载

            //第三方注入容器Autofac，与自带容器的对比 属性注入、基于名称注入、子容器、自定生存期管理、迟缓初始化，

            //带参数注入



            #endregion

            Console.WriteLine("---------------------------执行完毕------------------------------");
            Console.ReadLine();
        }
    }

    #region 带参数注入

    public interface IParamService
    {
        void PrintParam();
    }

    public class ParamService : IParamService
    {
        public string Name { get; set; }

        public ParamService(string name)
        {
            this.Name = name;
        }

        public void PrintParam()
        {
            Console.WriteLine($"打印出参数名称:{this.Name}");
        }
    }


    #endregion

    #region 理解注入服务声明周期

    public interface IAnimalService
    {
        public string Name { get; set; }

        public void SayHello();

    }

    public class EmployeeService : IAnimalService
    {
        public string Name { get; set; }

        public void SayHello()
        {
            Console.WriteLine($"你好！老板------------{this.Name}");    
        }

    }


    public class BossService : IAnimalService
    {
        public string Name { get; set; }

        public void SayHello()
        {
            Console.WriteLine($"小员工！你好------------{this.Name}");
        }
    }





    #endregion

    #region 理解DI

    public class Controller
    {
        private readonly IEmailService _emailService;
        public Controller(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void SendEmailTo()
        {
            _emailService.SendEmail();
        }

    }

    //配置

    public interface IConfigurationService
    {
        void GetConfiguration();
    }

    public class ConfigurationService : IConfigurationService
    {
        public void GetConfiguration()
        {
            Console.WriteLine("成功读取到配置文件");
        }
    }


    //日志
    public interface ILogService
    {
        void LogInfo(string msg);

        void LogError(string msg);
    }

    public class LogService : ILogService
    {
        public void LogError(string msg)
        {
            Console.WriteLine($"输出错误信息:{msg}");
        }

        public void LogInfo(string msg)
        {
            Console.WriteLine($"输出普通信息:{msg}");
        }
    }


    //邮件
    public interface IEmailService
    {
        void SendEmail();
    }

    public class EmailService : IEmailService
    {

        private readonly IConfigurationService _configurationService;

        private readonly ILogService _logService;

        public EmailService(IConfigurationService configurationService, ILogService logService)
        {
            _configurationService = configurationService;
            _logService = logService;
        }

        public void SendEmail()
        {
            try
            {
                //读取配置文件
                _configurationService.GetConfiguration();

                //开始发送邮件
                _logService.LogInfo("开始发送邮件！");
                Console.WriteLine($"发送邮件成功！");

                //邮件发送成功
                _logService.LogInfo("邮件发送成功！");
            }
            catch (Exception ex)
            {
                _logService.LogError($"邮件发送过程中失败:{ex.Message}");
            }

        }
    }
    #endregion


    #region Microsoft.Extensions.Configuration  Microsoft.Extensions.Configuration.Binder Microsoft.Extensions.Configuration.Json Microsoft.Extensions.Options
    //AddOptions AddMonitorOptions AddSnap
    
    


    #endregion


}
