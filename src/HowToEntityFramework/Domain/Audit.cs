using System;
using HowToEntityFramework.Support;

namespace HowToEntityFramework.Domain
{
    public class Audit
    {
        public DateTime UpdatedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void BeingCreated()
        {
            CreatedAt = App.Clock();
            UpdatedAt = App.Clock();
        }

        public void BeingUpdated()
        {
            UpdatedAt = App.Clock();
        }
    }
}