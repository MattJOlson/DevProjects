namespace Kata04
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.IO;

    public class TeamRecord
    {
        public TeamRecord(string line)
        {
            var fields = line.Split(new []{' '},
                                    StringSplitOptions.RemoveEmptyEntries)
                             .ToList();
            name = fields[1];
            pointsFor = Convert.ToInt32(fields[6]);
            pointsAgainst = Convert.ToInt32(fields[8]);
        }

        public int pointSpread() 
        { 
            return Math.Abs(pointsFor - pointsAgainst);
        }

        public string name { get; private set; }
        public int pointsFor { get; private set; }
        public int pointsAgainst { get; private set; }
    }

    public class SeasonStandings
    {
        public SeasonStandings(List<string> lines)
        {
            teams = new List<TeamRecord>();
            foreach(string line in lines) {
                if(isTeamRecord(line)) {
                    teams.Add(new TeamRecord(line));
                }
            }
        }

        // should be explicitly tested
        bool isTeamRecord(string line)
        {
            string header = "       Team";
            string cutoff = "   --------";
            var regex = new Regex("^" + header + "|" + cutoff);

            // if this doesn't match, it's a success
            return(!regex.Match(line).Success);
        }

        public TeamRecord smallestSpread()
        {
            return teams.OrderBy(t => t.pointSpread()).First();
        }

        public int numTeams { 
            get { return teams.Count; }
            set {} 
        }

        List<TeamRecord> teams { get; set; }
    }
}
