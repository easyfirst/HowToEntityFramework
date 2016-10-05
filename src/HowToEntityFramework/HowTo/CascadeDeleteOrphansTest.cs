using System;
using System.Data.Entity;
using System.Linq;
using DelegateDecompiler;
using HowToEntityFramework.Concerns;
using HowToEntityFramework.Domain;
using HowToEntityFramework.Infra;
using NUnit.Framework;
using Shouldly;

namespace HowToEntityFramework.HowTo
{
    [TestFixture]
    public class CascadeDeleteOrphansTest : IntegratedTest
    {
        [Test]
        public void Should_save_encapsulated_collections()
        {
            // arrange
            var dublin = new Store("Dublin");
            var london = new Store("London");

            var iphone = new Product("iPhone", 499);
            iphone.AddQuantityInStock(dublin, 10);
            iphone.AddQuantityInStock(london, 20);
            
            using (var db = new DatabaseContext())
            {
                db.Products.Add(iphone);
                db.SaveChanges();
            }

            // act
            using (var db = new DatabaseContext())
            {
                var fetch = db.Products
                    .Where(x => x.Id == iphone.Id)
                    .Include(x => x.Stocks)
                    .Include(x => x.Stocks.Select(s => s.Store))
                    .FirstOrDefault();

                fetch.RemoveStock(dublin);
                fetch.Stocks.Count().ShouldBe(1);

                db.SaveChanges();
            }

            // assert
            using (var db = new DatabaseContext())
            {
                var result = db.Products
                    .Where(x => x.Name == iphone.Name)
                    .Include(x => x.Stocks)
                    .Include(x => x.Stocks.Select(s => s.Store))
                    .SingleOrDefault();

                result.Stocks.Count().ShouldBe(2);

                result.Stocks.ElementAt(0).Store.Id.ShouldBe(dublin.Id);
                result.Stocks.ElementAt(0).Quantity.ShouldBe(10);

                result.Stocks.ElementAt(1).Store.Id.ShouldBe(london.Id);
                result.Stocks.ElementAt(1).Quantity.ShouldBe(20);
            }
        }
    }
}
