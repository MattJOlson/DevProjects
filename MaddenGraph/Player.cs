using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Gtk;

namespace MaddenGraph
{

    public class Player
    {
        public Player(string position, int x, int y, ReceiverTag rec)
        {
            Position = position;
            X = x;
            Y = y;
            Receiver = rec;
        }

        public void accept(IVisitor visitor)
        {
            visitor.visitPlayer(this);
        }

        public string Position { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public ReceiverTag Receiver { get; private set; }
    }
    
}
