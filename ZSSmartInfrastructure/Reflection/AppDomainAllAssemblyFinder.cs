using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ZSSmartInfrastructure.Finders;

namespace ZSSmartInfrastructure.Reflection
{
    /// <summary>
    /// 应用目录程序集查找器
    /// </summary>
    public class AppDomainAllAssemblyFinder : BaseFinder<Assembly>, IAllAssemblyFinder
    {
        /// <summary>
        /// 过滤网络程序集
        /// </summary>
        private readonly bool _filterNetAssembly;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="filterNetAssembly"></param>
        public AppDomainAllAssemblyFinder(bool filterNetAssembly)
        {
            _filterNetAssembly = filterNetAssembly;
        }

        /// <summary>
        /// 程序集查找
        /// </summary>
        /// <returns></returns>
        protected override Assembly[] FindAllItems()
        {
            string[] filters = new string[] 
            {
                //"mscrolib",
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window"
            };
            var context = DependencyContext.Default;
            if (context!=null)
            {
                var names = new List<string>();
                var assemblies = context.CompileLibraries
                    .SelectMany(m => m.Assemblies).Distinct().ToList();
                var dllNames = context.CompileLibraries
                    .SelectMany(m => m.Assemblies)
                    .Distinct()
                    .Select(m => m.Replace(".dll", ""))
                    .OrderBy(m => m).ToArray();
                if (dllNames.Length>0)
                {

                }
                else
                {

                }

            }

            //遍历文件夹的方式，用于传统的.netfx
            var currentPath = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentPath, "*.dll", SearchOption.TopDirectoryOnly)
            .Concat(Directory.GetFiles(currentPath,"*.exe",SearchOption.TopDirectoryOnly))
            .ToArray();

            return files.Where(file => filters.All(token => Path.GetFileName(file)?.StartsWith(token) != true))
                .Select(Assembly.LoadFrom).ToArray();
        }


        #region PrivateMethods

        /// <summary>
        /// 从文件路径加载程序集
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private static Assembly[] LoadFiles(IEnumerable<string> files)
        {
            var assemblies = new List<Assembly>();
            foreach (var file in files)
            {
                var name = new AssemblyName(file);
                try
                {
                    assemblies.Add(Assembly.Load(name));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return assemblies.ToArray();
        }
        #endregion

    }
}
