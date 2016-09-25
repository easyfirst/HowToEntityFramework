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

        public override int SaveChanges()
        {
            var entitiesBeingCreated = ChangeTracker.Entries<IAuditable>()
                    .Where(p => p.State == EntityState.Added)
                    .Select(p => p.Entity);

            foreach (var entityBeingCreated in entitiesBeingCreated)
            {
                entityBeingCreated.CreatedAt = App.Clock();
                entityBeingCreated.UpdatedAt = App.Clock();
            }

            var entitiesBeingUpdated = ChangeTracker.Entries<IAuditable>()
                    .Where(p => p.State == EntityState.Modified)
                    .Select(p => p.Entity);

            foreach (var entityBeingUpdated in entitiesBeingUpdated)
            {
                entityBeingUpdated.UpdatedAt = App.Clock();
            }

            return base.SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}