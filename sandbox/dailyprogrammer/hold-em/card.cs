namespace HoldEm
{
    using System;
    using System.Collections.Generic;

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
            return string.Format("{0} of {1}",
                                 val.ToString(), suit.ToString());
        }
    }

    public class Deck
    {
        public Deck()
        {
            deck_ = new List<Card>();

            foreach(Suit suit in Enum.GetValues(typeof(Suit))) {
                foreach(Value val in Enum.GetValues(typeof(Value))) {
                    deck_.Add(new Card(suit, val));
                }
            }
        }

        public int cardsLeft() { return deck_.Count; }
        public Card draw()
        {
            Card c = deck_[0];
            deck_.RemoveAt(0);
            return c;
        }

        public void shuffle() // XXX: not unit tested!
        {
            Random rng = new Random();

            // Fisher-Yates shuffle
            for(int i = deck_.Count-1; 0 <= i; i--) {
                int j = rng.Next(0, i+1);
                Card c = deck_[i];
                deck_[i] = deck_[j];
                deck_[j] = c;
            }
        }

        public void list() // kind of hacky but mostly for debugging
        {
            Console.WriteLine("{0} cards in deck:", deck_.Count);
            foreach (Card c in deck_) {
                Console.WriteLine(c.ToString());
            }
        }

        private List<Card> deck_;
    }

    public class Player
    {
        public Player(string n="Player")
        {
            name = n;
            hand = new List<Card>();
        }

        public bool dealTo(Card c)
        {
            if(hand.Count == 2) return false;

            hand.Add(c);
            return true;
        }

        public override string ToString()
        {
            string s = string.Format("{0}: ", name);
            for(int i = 0; i < hand.Count; i++) {
                if(0 < i) { s += ", "; }
                s += hand[i].ToString();
            }
            return s;
        }

        public List<Card> hand { get; private set; }
        public string name { get; private set; }
    }
}
