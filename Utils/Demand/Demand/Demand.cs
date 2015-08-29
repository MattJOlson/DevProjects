using System;
using NUnit.Framework;

namespace Demand
{
    public class Demand
    {
        public static DemandResult That(Func<bool> predicate) => new DemandResult(predicate);

        public static void That(bool predicate, string reason)
        {
            if (!predicate) throw new InvalidOperationException(reason);
        }
    }
}
