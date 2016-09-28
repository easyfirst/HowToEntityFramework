using System;

namespace HowToEntityFramework.Domain
{
    public class Effective
    {
        public Effective(DateTime from, DateTime to)
        {
            To = to;
            From = from;
        }

        public Effective(DateTime from)
        {
            From = from;
            To = null;
        }

        public DateTime? From { get; private set; }
        public DateTime? To { get; private set; }

        public bool IsEffectiveFor(DateTime date)
        {
            if (From.HasValue && date < From)
                return false;

            if (To.HasValue && date > To)
                return false;

            return true;
        }
    }
}