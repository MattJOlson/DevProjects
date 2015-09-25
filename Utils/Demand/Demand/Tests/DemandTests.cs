using System;
using FluentAssertions;
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

            Action vrfy = () => trueIsSuccess.Because("We don't throw on success");
            vrfy.ShouldNotThrow();
        }

        [Test]
        public void demanding_a_false_predicate_throws()
        {
            var falseIsFailure = Demand.That(() => false);

            Action vrfy = () => falseIsFailure.Because("We throw on failure");

            vrfy.ShouldThrow<InvalidOperationException>()
                .WithMessage("We throw on failure");
        }

        [Test]
        public void demanding_a_true_expression_succeeds_right_away()
        {
            Action vrfy = () => Demand.That(true, "because it's true!");

            vrfy.ShouldNotThrow();
        }

        [Test]
        public void demanding_a_false_expression_throws_right_away()
        {
            Action vrfy = () => Demand.That(false, "throws right away");

            vrfy.ShouldThrow<InvalidOperationException>()
                .WithMessage("throws right away");
        }
    }
}