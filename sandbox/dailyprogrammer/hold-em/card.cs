namespace HoldEm
{
    public enum Suit { Clubs, Diamonds, Hearts, Spades };
    public enum Value { Two,
                        Three,
                        Four,
                        Five,
                        Six,
                        Seven,
                        Eight,
                        Nine,
                        Ten,
                        Jack,
                        Queen,
                        King,
                        Ace };

    public class Card
    {
        public Card(Suit s, Value v)
        {
            suit = s;
            val = v;
        }

        public Suit suit { get; private set; }
        public Value val { get; private set; }

        public override string ToString() {
            return val.ToString() + " of " + suit.ToString();
        }
    }
}
