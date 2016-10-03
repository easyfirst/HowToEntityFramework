namespace HowToEntityFramework.Infra
{
    public interface IDatabaseMigrator
    {
        void UpdateSchema();
    }
}