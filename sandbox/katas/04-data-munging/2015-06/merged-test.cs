namespace Kata04
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class MergedTest
    {
        [Test]
        public void NamedRange_Construction()
        {
            var range = new NamedRange("Foo", 2, 3);

            Assert.That(range.name,   Is.EqualTo("Foo"));
            Assert.That(range.first,  Is.EqualTo(2));
            Assert.That(range.second, Is.EqualTo(3));
        }

        [Test]
        public void NamedRange_Interval()
        {
            var range = new NamedRange("Foo", 2, 3);
            
            Assert.That(range.interval(), Is.EqualTo(1));
        }

        [Test]
        public void NamedRange_PositiveIntervals()
        {
            var range = new NamedRange("Bar", 2, -3);

            Assert.That(range.interval(), Is.EqualTo(5));
        }

        public List<string> WeatherEntries = new List<string> {
  "  Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
  "",
   "  1  88    59    74          53.8       0.00 F       280  9.6 270  17 1.6  93 23 1004.5",
   "  2  79    63    71          46.5       0.00         330  8.7 340  23 3.3  70 28 1004.5",
   "  9  86    32*   59       6  61.5       0.00         240  7.6 220 12 6.0  78 46 1018.6",
   "  mo  82.9  60.5  71.7    16  58.8       0.00              6.9 5.3",
        };

        [Test]
        public void WeatherParser_ParseEntry()
        {
            var wp = new WeatherParser();
            var nr = wp.parseEntry(WeatherEntries[2]);

            Assert.That(nr.name,   Is.EqualTo("1"));
            Assert.That(nr.first,  Is.EqualTo(88));
            Assert.That(nr.second, Is.EqualTo(59));
        }

        [Test]
        public void WeatherParser_HandleAsterisk()
        {
            var wp = new WeatherParser();
            var nr = wp.parseEntry(WeatherEntries[4]);

            Assert.That(nr.second, Is.EqualTo(32));
        }

        [Test]
        public void WeatherParser_NonEntriesReturnNull()
        {
            var wp = new WeatherParser();

            Assert.That(wp.parseEntry(WeatherEntries[0]),
                        Is.Null);
            Assert.That(wp.parseEntry(WeatherEntries[1]),
                        Is.Null);
            Assert.That(wp.parseEntry(WeatherEntries[5]),
                        Is.Null);
        }

        public List<string> FootballEntries = new List<string> {
           "       Team            P     W    L   D    F      A     Pts",
           "    1. Arsenal         38    26   9   3    79  -  36    87",
           "   17. Sunderland      38    10  10  18    29  -  51    40",
           "   -------------------------------------------------------",
           "   18. Ipswich         38     9   9  20    41  -  64    36"
        };

        [Test]
        public void FootballParser_ParseEntry()
        {
            var fp = new FootballParser();
            var nr = fp.parseEntry(FootballEntries[1]);

            Assert.That(nr.name,   Is.EqualTo("Arsenal"));
            Assert.That(nr.first,  Is.EqualTo(79));
            Assert.That(nr.second, Is.EqualTo(36));
        }

        [Test]
        public void FootballParser_NonEntriesReturnNull()
        {
            var fp = new FootballParser();

            Assert.That(fp.parseEntry(FootballEntries[0]),
                        Is.Null);
            Assert.That(fp.parseEntry(FootballEntries[3]),
                        Is.Null);
        }
    }
}
