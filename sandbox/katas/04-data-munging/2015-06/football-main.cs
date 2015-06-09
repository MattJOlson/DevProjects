namespace Kata04
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    class FootballMain
    {
        public static void Main(string[] args)
        {
            SeasonStandings standings = new SeasonStandings(
                File.ReadAllLines(args[0]).ToList());
            Console.WriteLine(standings.smallestSpread().name);
        }
    }
}
