using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSSmartInfrastructure.Finders;

namespace ZSSmartInfrastructure.Reflection
{
    public class BaseAttributeTypeFinder<BaseAttributeType>:BaseFinder<Type>,ITypeFinder
        where BaseAttributeType:Attribute
    {

        private readonly IAllAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="allAssemblyFinder"></param>
        public BaseAttributeTypeFinder(IAllAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        protected override Type[] FindAllItems()
        {
            var assemblies = _allAssemblyFinder.FindAll(true);
            var types = assemblies.SelectMany(assembly=>assembly.GetTypes())
                .Where(type=>type.IsClass&&!type.IsAbstract&&type.HasAttribute<BaseAttributeType>())
                .Distinct().ToArray();
            return types;
        }

    }
}
