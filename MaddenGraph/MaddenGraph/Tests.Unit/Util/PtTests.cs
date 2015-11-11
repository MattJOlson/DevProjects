using FluentAssertions;
using MaddenGraph.Util;
using NUnit.Framework;

namespace MaddenGraph.Tests.Unit.Util
{
    [TestFixture]
    class PtTests
    {
        [Test]
        public void origin_works()
        {
            Pt.O.Should().Be(Pt.At(0, 0));
        }

        [Test]
        public void addition_works()
        {
            var u = Pt.At(2, 3);
            var v = Pt.At(1, 5);

            (u + v).Should().Be(Pt.At(3, 8));
        }

        [Test]
        public void subtraction_works()
        {
            var u = Pt.At(2, 3);
            var v = Pt.At(1, 5);

            (u - v).Should().Be(Pt.At(1,-2));
        }
    }
}