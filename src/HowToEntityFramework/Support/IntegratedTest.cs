using System.Data.Entity.Migrations;
using NUnit.Framework;
using Respawn;

namespace HowToEntityFramework.Support
{
    public class IntegratedTest
    {
        private static readonly Checkpoint Checkpoint = new Checkpoint
        {
            TablesToIgnore = new[]
            {
                "sysdiagrams",
                "tblUser",
                "tblObjectType",
                "__MigrationHistory"
            },
            SchemasToExclude = new string[] { }
        };

        [SetUp]
        public void IntegratedBeforeEachTest()
        {
            Log.App.Info("Cleaning all Tables");
            Checkpoint.Reset(App.ConnectionString);
        }

        [TestFixtureSetUp]
        public void IntegratedBeforeEachTestFixture()
        {
            Log.App.Info("Running Database Migration");

            var migrator = new DbMigrator(new MigrationConfiguration());

            migrator.Update();
        }

        public class MigrationConfiguration : DbMigrationsConfiguration<DatabaseContext>
        {
            public MigrationConfiguration()
            {
                AutomaticMigrationsEnabled = true;
            }
        }
    }
}