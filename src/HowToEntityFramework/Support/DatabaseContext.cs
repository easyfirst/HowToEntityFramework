using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using HowToEntityFramework.Domain;
using Z.EntityFramework.Plus;

namespace HowToEntityFramework.Support
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base(App.ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DbInterception.Add(new NLogCommandInterceptor(Log.App));

            QueryFilterManager.Filter<ISoftDeletable>(q => q.Where(x => x.IsDeleted == false));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}