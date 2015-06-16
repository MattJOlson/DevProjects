namespace Kata04
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class NamedRange
    {
        public NamedRange(string n, int f, int s)
        {
            name = n;
            first = f;
            second = s;
        }

        public int interval()
        {
            return Math.Abs(second - first);
        }

        public string name { get; private set; }
        public int first { get; private set; }
        public int second { get; private set; }
    }

    public interface IRangeParser
    {
        NamedRange parseEntry(string entry);
    }

    public class WeatherParser : IRangeParser
    {
        public WeatherParser() { }

        bool isWeatherEntry(string line)
        {
            if(line.Length == 0) { return false; } // empty line

            var match = new Regex(@"^  Dy").Match(line);
            if(match.Success) { return false; } // header line

            match = new Regex(@"^  mo").Match(line);
            if(match.Success) { return false; } // summary line

            return true;
        }

        public NamedRange parseEntry(string entry)
        {
            if(!isWeatherEntry(entry)) { return null; }

            string[] tokens = entry.Split(new []{" ", "*"},
                StringSplitOptions.RemoveEmptyEntries);
            string day = tokens[0];
            int high   = Convert.ToInt32(tokens[1]);
            int low    = Convert.ToInt32(tokens[2]);

            return new NamedRange(day, high, low);
        }
    }

    public class FootballParser : IRangeParser
    {
        public FootballParser() { }

        bool isFootballEntry(string[] tokens)
        { 
            // relegation line, hyphens treated as separators
            if(tokens.Length == 0) { return false; }

            // Header line
            if(tokens[0] == "Team") { return false; }

            return true;
        }

        public NamedRange parseEntry(string entry)
        {
            string[] tokens = entry.Split(new []{" ", "-"},
                StringSplitOptions.RemoveEmptyEntries);
            if(!isFootballEntry(tokens)) { return null; }

            string team = tokens[1];
            int pts_for = Convert.ToInt32(tokens[6]);
            int pts_vs  = Convert.ToInt32(tokens[7]);

            return new NamedRange(team, pts_for, pts_vs);
        }
    }
}
