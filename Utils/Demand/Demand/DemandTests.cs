using System;
using NUnit.Framework;

namespace Demand
{
    [TestFixture]
    public class DemandTests
    {
        [Test]
        public void demanding_a_true_predicate_silently_succeeds()
        {
            var trueIsSuccess = Demand.That(() => true);

            Assert.DoesNotThrow(() => trueIsSuccess.Because("We don't throw on success"));
        }

        [Test]
        public void demanding_a_false_predicate_throws()
        {
            var trueIsSuccess = Demand.That(() => false);

            var ex = Assert.Throws<InvalidOperationException>(
                () => trueIsSuccess.Because("We throw on failure")
            );
            Assert.That(ex.Message, Is.EqualTo("We throw on failure"));
        }
    }
}