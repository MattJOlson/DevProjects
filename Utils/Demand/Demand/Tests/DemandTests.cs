using System;
using NUnit.Framework;

namespace Demand.Tests
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
            var falseIsFailure = Demand.That(() => false);

            var ex = Assert.Throws<InvalidOperationException>(
                () => falseIsFailure.Because("We throw on failure")
            );
            Assert.That(ex.Message, Is.EqualTo("We throw on failure"));
        }

        [Test]
        public void demanding_a_true_expression_succeeds_right_away()
        {
            Assert.DoesNotThrow(() => Demand.That(true, "because it's true!"));
        }
    }
}