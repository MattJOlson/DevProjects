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
            var twoclubs = new Card(Suit.Clubs, Value.Two);

            Assert.AreEqual(twoclubs.suit, Suit.Clubs);
            Assert.AreEqual(twoclubs.val, Value.Two);
        }

        [Test]
        public void CardToString()
        {
            var twoclubs = new Card(Suit.Clubs, Value.Two);

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
                foreach(Value val in Enum.GetValues(typeof(Value))) {
                    var card = deck.draw();
                    ++drawn;

                    Assert.AreEqual(card.suit, suit);
                    Assert.AreEqual(card.val, val);
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

            var flag = player.dealTo(new Card(Suit.Clubs, Value.Two));

            Assert.True(flag);
            Assert.AreEqual(player.hand.Count, 1);
            Assert.AreEqual(player.hand[0].suit, Suit.Clubs);
            Assert.AreEqual(player.hand[0].val, Value.Two);

            flag = player.dealTo(new Card(Suit.Diamonds, Value.Seven));
            Assert.True(flag);
            Assert.AreEqual(player.hand.Count, 2);
            Assert.AreEqual(player.hand[1].suit, Suit.Diamonds);
            Assert.AreEqual(player.hand[1].val, Value.Seven);

            flag = player.dealTo(new Card(Suit.Spades, Value.Ace));
            Assert.False(flag);
            Assert.AreEqual(player.hand.Count, 2);
        }

        [Test]
        public void PlayerToString()
        {
            var player = new Player("CPU 1");
            player.dealTo(new Card(Suit.Clubs, Value.Two));
            player.dealTo(new Card(Suit.Diamonds, Value.Seven));

            Assert.AreEqual(player.ToString(),
                            "CPU 1: Two of Clubs, Seven of Diamonds");
        }
    }
}
