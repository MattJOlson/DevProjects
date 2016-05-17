using FluentAssertions;
using NUnit.Framework;

namespace String_calc_2016_05_10_csharp
{
    [TestFixture]
    public class StringCalcTests
    {
        [TestCase("", 0)]
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        public void when_given_at_most_one_input_calc_returns_the_input_integer(string input, int expected)
        {
            Check(input, expected);
        }

        [TestCase("1,1", 2)]
        [TestCase("1,2,3,4", 10)]
        public void when_given_many_inputs_calc_returns_their_sum(string input, int expected)
        {
            Check(input, expected);
        }

        [TestCase("1\n1", 2)]
        [TestCase("1,2\n3\n4", 10)]
        public void when_given_newlines_as_delims_calc_respects_them(string input, int expected)
        {
            Check(input, expected);
        }

        private void Check(string input, int expected)
        {
            StringCalc.Calc(input).Should().Be(expected);
        }
    }
}