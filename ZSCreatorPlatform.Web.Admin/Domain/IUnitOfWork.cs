using System.Threading;
using System.Threading.Tasks;

namespace ZSCreatorPlatform.Web.Admin.Domain
{
    public interface IUnitOfWork
    {

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <returns></returns>
        bool Commit();

        /// <summary>
        /// 事务异步提交
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> CommitAsync(CancellationToken token);

    }
}