using System;

namespace Demand
{
    public class DemandResult
    {
        private readonly Func<bool> _predicate;

        public DemandResult(Func<bool> predicate)
        {
            _predicate = predicate;
        }

        public void Because(string reason)
        {
            if (!_predicate()) throw new InvalidOperationException(reason);
        }
    }
}