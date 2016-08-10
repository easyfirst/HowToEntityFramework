using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace HowToEntityFramework.HowTo
{
    /// <summary>
    /// How? Create a private empty constructor to let EF initialize the entity on runtime
    /// </summary>
    [TestFixture]
    public class Required_constructor
    {
        public class Product
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

            private Product()
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

        [SetUp]
        public void Scenario()
        {
            using (var db = new DatabaseContext())
            {
                db.Products.Add(new Product("iPhone 6", 699.99m));
                db.Products.Add(new Product("Samsung Galaxy S7", 799.99m));
                db.SaveChanges();
            }
        }

        [Test]
        public void Assert()
        {
            using (var db = new DatabaseContext())
            {
                var products = db.Products.ToList();

                products[0].Name.ShouldBe("iPhone 6");
                products[0].Price.ShouldBe(699.99m);

                products[1].Name.ShouldBe("Samsung Galaxy S7");
                products[1].Price.ShouldBe(799.99m);
            }
        }
    }
}
