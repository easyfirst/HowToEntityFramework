namespace HowToEntityFramework.Concerns
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}