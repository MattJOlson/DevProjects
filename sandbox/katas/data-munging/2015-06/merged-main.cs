namespace Kata04
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using NRList = System.Collections.Generic.List<NamedRange>;

    class MergedMain
    {
        public static void Main(string[] args)
        {
            IRangeParser parser;
            Func<NRList, NamedRange> selector;

            if(new Regex(@"weather\.dat").Match(args[0]).Success) {
                parser = new WeatherParser();
                selector = (NRList list) => {
                    return list.OrderBy(e => e.interval())
                               .Last();
                };
            } else if(new Regex(@"football\.dat").Match(args[0]).Success) {
                parser = new FootballParser();
                selector = (NRList list) => {
                    return list.OrderBy(e => e.interval())
                               .Last();
                };
            } else {
                Console.WriteLine("I don't know how to parse that\n");
                parser = null; // to shut up mcs
                selector = null;
                Environment.Exit(1);
            }

            NRList ranges = new NRList();
            foreach(string line in File.ReadLines(args[0])) {
                var range = parser.parseEntry(line);
                if(range != null) { ranges.Add(range); }
            }

            Console.WriteLine(selector(ranges).name);
        }
    }
}
