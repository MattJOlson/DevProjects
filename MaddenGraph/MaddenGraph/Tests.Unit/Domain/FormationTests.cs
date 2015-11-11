using System.Linq;
using FluentAssertions;
using MaddenGraph.Domain;
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

        [Test]
        public void default_formations_are_created_with_five_eligible_receivers()
        {
            var formation = new Formation(2, 2);

            formation.EligibleReceivers.Count.Should().Be(5);
        }

        [Test]
        public void formation_construction_respects_per_side_receiver_params()
        {
            var formation = new Formation(2,2);

            formation.WeakSideReceivers.Count.Should().Be(2);
            formation.StrongSideReceivers.Count.Should().Be(2);
            formation.BackfieldReceivers.Count.Should().Be(1);
        }

        [Test]
        public void formation_construction_allows_quads_formations_too()
        {
            var formation = new Formation(1,4);

            formation.WeakSideReceivers.Count.Should().Be(1);
            formation.StrongSideReceivers.Count.Should().Be(4);
            formation.BackfieldReceivers.Count.Should().Be(0);
        }

        [Test]
        public void all_eligible_receivers_in_a_formation_really_are_eligible()
        {
            var formation = new Formation(2,2);

            foreach (var p in formation.EligibleReceivers) {
                p.IsEligible.Should().BeTrue();
            }
        }
    }
}
