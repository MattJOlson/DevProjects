namespace Kata04
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    class WeatherMain
    {
        public static void Main(string[] args)
        {
            List<WeatherDay> days = new List<WeatherDay>();

            // assume args contains path to weather.dat
            using (StreamReader sr = new StreamReader(args[0])) {
                sr.ReadLine(); // read past header
                sr.ReadLine(); // read past empty line
                string line;
                while((line = sr.ReadLine()) != null) {
                    days.Add(new WeatherDay(line));
                }
            }

            Console.WriteLine(WeatherDay.WidestTempSpread(days).dayNumber);
        }
    }
}
