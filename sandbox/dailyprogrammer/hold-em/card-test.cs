namespace HoldEm
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CardTest
    {
        [Test]
        public void CardConstruction()
        {
            var twoclubs = new Card(Suit.Clubs, Rank.Two);

            Assert.AreEqual(twoclubs.suit, Suit.Clubs);
            Assert.AreEqual(twoclubs.rank, Rank.Two);
        }

        [Test]
        public void CardToString()
        {
            var twoclubs = new Card(Suit.Clubs, Rank.Two);

            Assert.AreEqual("Two of Clubs", twoclubs.ToString());
        }
    }

    [TestFixture]
    public class DeckTest
    {
        [Test]
        public void DeckConstruction()
        {
            var deck = new Deck();
            Assert.AreEqual(deck.cardsLeft(), 52);
        }

        [Test]
        public void DeckDrawing()
        {
            var deck = new Deck();
            var drawn = 0;
            foreach(Suit suit in Enum.GetValues(typeof(Suit))) {
                foreach(Rank rank in Enum.GetValues(typeof(Rank))) {
                    var card = deck.draw();
                    ++drawn;

                    Assert.AreEqual(card.suit, suit);
                    Assert.AreEqual(card.rank, rank);
                    Assert.AreEqual(deck.cardsLeft(), 52-drawn);
                }
            }
        }
    }

    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void PlayerConstruction()
        {
            var player = new Player();

            Assert.AreEqual(player.hand.Count, 0);
        }

        [Test]
        public void PlayerDealTo()
        {
            var player = new Player();

            player.dealTo(new Card(Suit.Clubs, Rank.Two));

            Assert.AreEqual(player.hand.Count, 1);
            Assert.AreEqual(player.hand[0].suit, Suit.Clubs);
            Assert.AreEqual(player.hand[0].rank, Rank.Two);

            player.dealTo(new Card(Suit.Diamonds, Rank.Seven));
            Assert.AreEqual(player.hand.Count, 2);
            Assert.AreEqual(player.hand[1].suit, Suit.Diamonds);
            Assert.AreEqual(player.hand[1].rank, Rank.Seven);

            Assert.Throws(typeof(InvalidOperationException),
                () => player.dealTo(new Card(Suit.Spades, Rank.Ace)),
                string.Format("{0} hand is full", player.name));
            Assert.AreEqual(player.hand.Count, 2);
        }

        [Test]
        public void PlayerToString()
        {
            var player = new Player("CPU 1");
            player.dealTo(new Card(Suit.Clubs, Rank.Two));
            player.dealTo(new Card(Suit.Diamonds, Rank.Seven));

            Assert.AreEqual(player.ToString(),
                            "CPU 1: Two of Clubs, Seven of Diamonds");
        }
    }
}
