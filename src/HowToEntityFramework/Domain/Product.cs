using System;

namespace HowToEntityFramework.Domain
{
    public class Product : IAuditable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        //private Product()
        //{
        //}

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}