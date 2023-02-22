using System.Reflection;
using System.Runtime.Loader;
using Microsoft.EntityFrameworkCore;
using ZSCreatorPlatform.Web.Admin.Domain.Models;

namespace ZSCreatorPlatform.Web.Admin.Domain
{
    /// <summary>
    /// ZSCreatorDbContext
    /// </summary>
    public class ZSCreatroDbContext:DbContext
    {

        #region Contors

        public ZSCreatroDbContext(DbContextOptions<ZSCreatroDbContext> dbOptions):base(dbOptions)
        {
            
        }

        #endregion

        public DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}