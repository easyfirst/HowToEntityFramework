namespace HowToEntityFramework.Domain
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}