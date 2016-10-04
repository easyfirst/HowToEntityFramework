using HowToEntityFramework.Concerns;

namespace HowToEntityFramework.Domain
{
    public class Store : IEntity
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        private Store()
        {
        }

        public Store(string name)
        {
            Name = name;
        }
    }
}