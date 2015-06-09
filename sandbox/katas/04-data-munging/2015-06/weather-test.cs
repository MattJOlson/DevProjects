namespace Kata04
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Kata04;

    [TestFixture]
    public class WeatherTest
    {
        public List<string> TestLines = new List<string> {
   "1  88    59    74          53.8       0.00 F       280  9.6 270  17 1.6  93 23 1004.5",
   "2  79    63    71          46.5       0.00         330  8.7 340  23 3.3  70 28 1004.5",
   "9  86    32*   59       6  61.5       0.00         240  7.6 220  12 6.0  78 46 1018.6"
        };

        [Test]
        public void WeatherDayConstruction()
        {
            WeatherDay day = new WeatherDay(TestLines[0]);
            Assert.AreEqual(1, day.dayNumber);
            Assert.AreEqual(88, day.maxTemp);
            Assert.AreEqual(59, day.minTemp);
        }

        [Test]
        public void WeatherDaySpreadCalc()
        {
            WeatherDay day = new WeatherDay(TestLines[0]);
            Assert.AreEqual(88-59, day.tempSpread());
        }

        [Test]
        public void WeatherParseTaggedInt()
        {
            WeatherDay day = new WeatherDay(TestLines[2]);
            Assert.AreEqual(32, day.minTemp);
        }

        [Test]
        public void WeatherSpreadFinder()
        {
            List<WeatherDay> days = new List<WeatherDay>();
            TestLines.ForEach(l => days.Add(new WeatherDay(l)));

            WeatherDay widest = WeatherDay.WidestTempSpread(days);
            Assert.AreEqual(9, widest.dayNumber);
        }
    }
}
