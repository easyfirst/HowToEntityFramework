using NUnit.Framework;

namespace HowToEntityFramework.Infra
{
    public class IntegratedTest
    {
        private readonly IDatabaseCleaner _databaseCleaner = new RespawnDatabaseCleaner();
        private readonly IDatabaseMigrator _databaseMigrator = new DatabaseMigrator();
        
        [SetUp]
        public void IntegratedBeforeEachTest()
        {
            Log.App.Info("Cleaning all Tables");
            _databaseCleaner.CleanAllTables(App.ConnectionString);
        }

        [TestFixtureSetUp]
        public void IntegratedBeforeEachTestFixture()
        {
            Log.App.Info("Running Database Migration");
            _databaseMigrator.UpdateSchema();  
        }        
    }
}