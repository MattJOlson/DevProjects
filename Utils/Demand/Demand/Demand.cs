using System;

namespace Demand
{
    public class Demand
    {
        public static IDemandResult That(Func<bool> predicate)
        {
            // Right now we're evaluating the predicate right away and returning an object
            //  that throws later.  We can maybe improve on that?
            if (predicate()) {
                return new SuccessResult();
            } else {
                return new FailureResult();
            }
        }
    }

    public class FailureResult : IDemandResult
    {
        public void Because(string reason) { throw new InvalidOperationException(reason); }
    }

    public class SuccessResult : IDemandResult
    {
        public void Because(string reason) { /* quietly do nothing */}
    }

    public interface IDemandResult
    {
        void Because(string reason);
    }


}
