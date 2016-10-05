using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using HowToEntityFramework.Concerns;
using HowToEntityFramework.Domain;
using Z.EntityFramework.Plus;

namespace HowToEntityFramework.Infra
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base(App.ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DbInterception.Add(new NLogCommandInterceptor(Log.App));

            // TODO: IoC Configurable
            QueryFilterManager.Filter<ISoftDeletable>(q => q.Where(x => x.IsDeleted == false));

            modelBuilder.Entity<Product>().HasMany(x => x.Stocks);
        }

        public override int SaveChanges()
        {
            // TODO: IoC Before Save Handlers
            var entitiesBeingCreated = ChangeTracker.Entries<IAuditable>()
                    .Where(p => p.State == EntityState.Added)
                    .Select(p => p.Entity);

            foreach (var entityBeingCreated in entitiesBeingCreated)
            {
                entityBeingCreated.Audit.BeingCreated();
            }

            var entitiesBeingUpdated = ChangeTracker.Entries<IAuditable>()
                    .Where(p => p.State == EntityState.Modified)
                    .Select(p => p.Entity);

            foreach (var entityBeingUpdated in entitiesBeingUpdated)
            {
                entityBeingUpdated.Audit.BeingUpdated();
            }

            return base.SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}