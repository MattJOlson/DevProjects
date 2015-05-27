namespace HoldEm
{
    using System;
    using System.Collections.Generic;

    public enum Suit { Clubs, Diamonds, Hearts, Spades };
    public enum Rank { Two, Three, Four, Five, Six, Seven, Eight, Nine,
                       Ten, Jack, Queen, King, Ace };

    public class Card
    {
        public Card(Suit s, Rank r)
        {
            suit = s;
            rank = r;
        }

        public Suit suit { get; private set; }
        public Rank rank { get; private set; }

        public override string ToString() {
            return string.Format("{0} of {1}", rank, suit);
        }
    }

    public class Deck
    {
        public Deck()
        {
            deck_ = new List<Card>();

            foreach(Suit suit in Enum.GetValues(typeof(Suit))) {
                foreach(Rank rank in Enum.GetValues(typeof(Rank))) {
                    deck_.Add(new Card(suit, rank));
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
            deck_.ForEach(c => Console.WriteLine(c));
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

        public void dealTo(Card c)
        {
            if(hand.Count == 2) {
                throw new InvalidOperationException(
                    string.Format("{0} hand is full", name)
                );
            }

            hand.Add(c);
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
