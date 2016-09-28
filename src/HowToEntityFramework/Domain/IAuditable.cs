using System;

namespace HowToEntityFramework.Domain
{
    public interface IAuditable
    {
        Audit Audit { get; }
    }
}