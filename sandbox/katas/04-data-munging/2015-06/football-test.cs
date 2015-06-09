namespace Kata04
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Kata04;

    [TestFixture]
    public class FootballTest
    {
        public List<string> TestLines = new List<string> {
           "       Team            P     W    L   D    F      A     Pts",
           "    1. Arsenal         38    26   9   3    79  -  36    87",
           "   17. Sunderland      38    10  10  18    29  -  51    40",
           "   -------------------------------------------------------",
           "   18. Ipswich         38     9   9  20    41  -  64    36"
        };

        [Test]
        public void TeamRecord_Construction()
        {
            var team = new TeamRecord(TestLines[2]);

            Assert.That(team.name,          Is.EqualTo("Sunderland"));
            Assert.That(team.pointsFor,     Is.EqualTo(29));
            Assert.That(team.pointsAgainst, Is.EqualTo(51));
        }

        [Test]
        public void TeamRecord_PointSpread()
        {
            var team = new TeamRecord(TestLines[2]);

            Assert.That(team.pointSpread(), Is.EqualTo(Math.Abs(29-51)));
        }

        [Test]
        public void SeasonStandings_Construction()
        {
            var season = new SeasonStandings(TestLines);

            Assert.That(season.numTeams, Is.EqualTo(3));
        }

        [Test]
        public void SeasonStandings_SmallestSpread()
        {
            var season = new SeasonStandings(TestLines);

            Assert.That(season.smallestSpread().name,
                        Is.EqualTo("Sunderland"));
        }
    }
}
