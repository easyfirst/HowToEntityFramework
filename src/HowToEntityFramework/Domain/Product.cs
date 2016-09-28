namespace HowToEntityFramework.Domain
{
    public class Product : IAuditable, IEntity
    {
        public long Id { get; private set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Audit Audit { get; set; } = new Audit();

        private Product()
        {
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}