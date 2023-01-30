using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSSmartInfrastructure.Finders;

namespace ZSSmartInfrastructure.Reflection
{
    /// <summary>
    /// 指定基类的实现类型查找器
    /// </summary>
    public class BaseTypeFinder<BaseType> : BaseFinder<Type>,ITypeFinder
    {
        private readonly IAllAssemblyFinder _allAssemblyFinder;

        protected BaseTypeFinder(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            var assemblies = _allAssemblyFinder.FindAll(true);
            var types=assemblies.SelectMany(assembly=>assembly.GetTypes())
                .Where(type=>type.IsDeriveClassFrom<BaseType>()).Distinct().ToArray();
            return types;
        }
    }
}
