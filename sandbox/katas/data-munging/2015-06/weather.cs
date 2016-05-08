namespace Kata04
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class WeatherDay
    {
        public WeatherDay(string data)
        {
            parseWeatherLine(data);
        }

        int? parseIntFromField(string field)
        {
            var match = new Regex(@"\d+").Match(field);

            if(!match.Success) { return null; }

            return Convert.ToInt32(match.Value);
        }

        void parseWeatherLine(string data)
        {
            string[] tokens = data.Split(new string[] {" "},
                StringSplitOptions.RemoveEmptyEntries);
            dayNumber = parseIntFromField(tokens[0]) ?? -1;
            maxTemp   = parseIntFromField(tokens[1]) ?? -1;
            minTemp   = parseIntFromField(tokens[2]) ?? -1;
            // discard rest
        }

        public int tempSpread()
        {
            return maxTemp - minTemp;
        }

        public int dayNumber { get; private set; }
        public int maxTemp { get; private set; }
        public int minTemp { get; private set; }

        public static WeatherDay WidestTempSpread(List<WeatherDay> days)
        {
            return days.OrderByDescending(d => d.tempSpread()) .First();
        }
    }
}
