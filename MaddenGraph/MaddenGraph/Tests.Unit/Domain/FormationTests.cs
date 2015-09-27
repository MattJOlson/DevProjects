using NUnit.Framework;

namespace MaddenGraph.Tests.Unit.Domain
{
    [TestFixture]
    class FormationTests
    {
        [Test]
        public void formations_are_created_with_eleven_player_positions()
        {
            var formation = new Formation(2, 2);

            formation.Positions.Count.Should().Be(11);
        }
    }
}
