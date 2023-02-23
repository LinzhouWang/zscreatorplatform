using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ZSCreatorPlatform.Web.Admin.Domain.Models;

namespace ZSCreatorPlatform.Web.Admin.Domain
{
    /// <summary>
    /// ZSCreatorDbContext
    /// </summary>
    public class ZSCreatroDbContext:DbContext,IUnitOfWork
    {

        #region Contors

        /// <summary>
        /// Contors
        /// </summary>
        /// <param name="dbOptions"></param>
        public ZSCreatroDbContext(DbContextOptions<ZSCreatroDbContext> dbOptions):base(dbOptions)
        {
            
        }

        #endregion

        public DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// ModelCreatingConfig
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            return base.SaveChanges()>0;
        }
        
        /// <summary>
        /// 事务提交异步
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> CommitAsync(CancellationToken token)
        {
            var res = await base.SaveChangesAsync(token) > 0;
            return res;
        }
        
    }
}