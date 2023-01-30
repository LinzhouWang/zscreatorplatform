using System;
using System.Collections.Generic;
using System.Text;

namespace ZSSmartInfrastructure.Finders
{
    /// <summary>
    /// 定义一个查找接口
    /// </summary>
    /// <typeparam name="TItem">要查找的项</typeparam>
    public interface IFinder<out TItem>
    {

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <param name="fromCache">是否来自缓存</param>
        /// <returns></returns>
        TItem[] Find(Func<TItem,bool> predicate,bool fromCache=false);

        /// <summary>
        /// 查找所有项
        /// </summary>
        /// <param name="fromCache">是否来自缓存</param>
        /// <returns></returns>
        TItem[] FindAll(bool fromCache=true);
    }
}
