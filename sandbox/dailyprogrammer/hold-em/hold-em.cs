namespace HoldEm
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static int getNumPlayers()
        {
            while (true) {
                int nPlayers = Convert.ToInt32(Console.ReadLine());
                if(nPlayers <= 8) return nPlayers;
                Console.WriteLine("lol, try again");
            }
        }

        static List<Player> buildPlayerList(int nPlayers)
        {
            List<Player> players = new List<Player>();

            players.Add(new Player()); // user
            for(int i = 1; i < nPlayers; i++) {
                players.Add(new Player("CPU" + i));
            }

            return players;
        }

        static void dealPlayerHands(List<Player> players, Deck deck)
        {
            for(int i = 0; i < 2; i++) {
                players.ForEach(p => p.dealTo(deck.draw()));
            }
        }

        static void burnFrom(Deck deck)
        {
            Card c = deck.draw();
            Console.WriteLine("<burned: {0}>", c);
        }

        static void deal(string which, int howMany, Deck deck)
        {
            burnFrom(deck);
            Console.Write("{0}: ", which);
            for(int i = 0; i < howMany; i++) {
                if(0 < i) { Console.Write(", "); }
                Card c = deck.draw();
                Console.Write(c);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("How many players(2-8)?");

            int nPlayers = getNumPlayers();
            List<Player> players = buildPlayerList(nPlayers);

            Deck deck = new Deck();
            deck.shuffle();

            dealPlayerHands(players, deck);

            players.ForEach(p => Console.WriteLine(p));

            deal("Flop", 3, deck);

            deal("Turn", 1, deck);

            deal("River", 1, deck);

            // deck.list();
        }
    }
}
