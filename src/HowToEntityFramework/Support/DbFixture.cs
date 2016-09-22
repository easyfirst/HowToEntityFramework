using Respawn;

namespace HowToEntityFramework.Support
{
    public class DbFixture
    {
        private static readonly Checkpoint Checkpoint = new Checkpoint();

        public DbFixture()
        {
            Checkpoint.Reset(App.ConnectionString);
        }
    }
}
