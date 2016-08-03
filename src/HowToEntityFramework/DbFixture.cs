using Respawn;

namespace HowToEntityFramework
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
