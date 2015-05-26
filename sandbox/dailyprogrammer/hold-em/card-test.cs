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
}
