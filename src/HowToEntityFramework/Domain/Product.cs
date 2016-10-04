using System.Collections;
using System.Collections.Generic;
using HowToEntityFramework.Concerns;

namespace HowToEntityFramework.Domain
{
    public class Product : IAuditable, IEntity
    {
        public ICollection<Stock> Stocks { get; set; } = new HashSet<Stock>();
        
        public long Id { get; private set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Audit Audit { get; set; } = new Audit();
        //public IEnumerable<Stock> Stocks => Stocks;

        private Product()
        {
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void AddQuantityInStock(Store store, int quantity)
        {
            Stocks.Add(new Stock(this, store, quantity));
        }
    }
}