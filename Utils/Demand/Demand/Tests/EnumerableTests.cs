using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Linq;

namespace Demand.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        // I want to be able to do LINQ-ish calls on IEnumerables (and IObservables?). I don't really have any use cases yet, but something like:
        // List<Foo> fooList;
        // Demand.That(fooList).IsNonEmpty(); // This isn't really LINQ, it's just kind of fluent
        // Yeah, I dunno, gotta think about the interface and the requirements a bit more I guess
        [Test]
        public void placeholder_test()
        {
            Assert.That(true, Is.True);
        }

        [Test]
        public void trying_out_params_keyword()
        {
            Assert.That(CountMyArgs("foo", "bar", "baz"), Is.EqualTo(3));
        }

        private int CountMyArgs(params string[] args) { return args.Count(); }
    }
}