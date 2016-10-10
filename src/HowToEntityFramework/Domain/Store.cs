using HowToEntityFramework.Concerns;
using NodaTime;

namespace HowToEntityFramework.Domain
{
    public class Store : IEntity
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public LocalTime CloseAt { get; private set; }
        public LocalTime OpenAt { get; private set; }

        private Store()
        {
        }

        public Store(string name)
        {
            Name = name;
        }

        public Store(string name, LocalTime openAt, LocalTime closeAt) : this(name)
        {
            OpenAt = openAt;
            CloseAt = closeAt;
        }
    }
}