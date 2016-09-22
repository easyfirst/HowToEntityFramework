using System;

namespace HowToEntityFramework.Domain
{
    public interface IAuditable
    {
        DateTime CreatedAt { get; }

        DateTime UpdatedAt { get; }
    }
}