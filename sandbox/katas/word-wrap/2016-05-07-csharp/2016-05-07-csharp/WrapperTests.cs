using FluentAssertions;
using NUnit.Framework;

namespace Word_wrap_2016_05_07_csharp
{
    [TestFixture]
    public class WrapperTests
    {
        [Test]
        public void when_given_an_empty_string_wrap_returns_an_empty_string()
        {
            Wrapper.Wrap("", 4).Should().Be("");
        }

        [Test]
        public void when_given_a_short_string_wrap_returns_that_string_unmolested()
        {
            Wrapper.Wrap("falabala", 10).Should().Be("falabala");
        }

        [Test]
        public void when_given_a_long_string_wrap_returns_that_string_with_a_line_break_after_the_last_column()
        {
            Wrapper.Wrap("foobar", 3).Should().Be("foo\nbar");
        }

        [Test]
        public void when_given_a_very_long_string_wrap_adds_multiple_line_breaks()
        {
            Wrapper.Wrap("foobarbaz", 3).Should().Be("foo\nbar\nbaz");
        }

        [Test]
        public void when_given_a_long_string_with_spaces_wrap_breaks_at_the_last_space_on_the_line()
        {
            Wrapper.Wrap("foo bar", 5).Should().Be("foo\nbar");
        }

        // Added this requirement but it seems sensible
        [Test]
        public void when_splitting_on_several_consecutive_spaces_wrap_eats_them_all()
        {
            Wrapper.Wrap("foo   bar", 6).Should().Be("foo\nbar");
            Wrapper.Wrap("foo   bar", 4).Should().Be("foo\nbar");
        }
    }
}
