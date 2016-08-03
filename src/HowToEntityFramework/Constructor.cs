using System.Data.Entity;
using System.Linq;
using Shouldly;
using Xunit;

namespace HowToEntityFramework
{
    // SAMPLE: Constructor
    public class Constructor : IClassFixture<DbFixture>
    {
        [Fact]
        public void How_model_entity_with_required_constructor()
        {
            using (var db = new DatabaseContext())
            {
                db.Products.Add(new Product("iPhone 6", 699.99m));
                db.Products.Add(new Product("Samsung Galaxy S7", 799.99m));
                db.SaveChanges();
            }

            using (var db = new DatabaseContext())
            {
                var products = db.Products.ToList();

                products[0].Name.ShouldBe("iPhone 6");
                products[0].Price.ShouldBe(699.99m);

                products[1].Name.ShouldBe("Samsung Galaxy S7");
                products[1].Price.ShouldBe(799.99m);
            }
        }

        public class Product
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

            protected Product()
            {
            }

            public Product(string name, decimal price)
            {
                Name = name;
                Price = price;
            }
        }

        public class DatabaseContext : BaseDbContext
        {
            public DbSet<Product> Products { get; set; }
        }
    }
    // END: Constructor
}
