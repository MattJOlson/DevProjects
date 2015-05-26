namespace HoldEm
{
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
}
