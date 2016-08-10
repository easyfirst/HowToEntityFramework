using System;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace HowToEntityFramework.HowTo
{
    /// <summary>
    /// How? Set property as virtual
    /// </summary>
    [TestFixture]
    public class Private_setter
    {
        public class User
        {
            public long Id { get; set; }
            public string Name { get; private set; }
            public virtual int YearOfBirth { get; private set; }

            private User()
            {
            }

            public User(string name, int actualAge)
            {
                Name = name;
                YearOfBirth = DateTime.Now.AddYears(-actualAge).Year;
            }
        }

        public class DatabaseContext : BaseDbContext
        {
            public DbSet<User> Users { get; set; }
        }

        [SetUp]
        public void Arrange()
        {
            using (var db = new DatabaseContext())
            {
                db.Users.Add(new User("John", 20));
                db.Users.Add(new User("Paul", 30));
                db.SaveChanges();
            }
        }

        [Test]
        public void Assert()
        {
            using (var db = new DatabaseContext())
            {
                var result = db.Users.ToList();

                result[0].Name.ShouldBe("John");
                result[0].YearOfBirth.ShouldBe(DateTime.Now.Year - 20);

                result[1].Name.ShouldBe("Paul");
                result[1].YearOfBirth.ShouldBe(DateTime.Now.Year - 30);
            }
        }
    }
}
