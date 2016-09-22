using System;

namespace HowToEntityFramework.Domain
{
    public class User : ISoftDeletable
    {
        public long Id { get; set; }
        public string Name { get; private set; }
        public virtual int YearOfBirth { get; private set; }
        public bool IsDeleted { get; set; }

        private User()
        {
        }

        public User(string name, int actualAge)
        {
            Name = name;
            YearOfBirth = DateTime.Now.AddYears(-actualAge).Year;
        }
    }
}