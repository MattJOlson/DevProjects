using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Gtk;

namespace MaddenGraph
{
    public enum ReceiverTag { NONE, X, SQUARE, TRI, CIRCLE, R1 };

    public class Formation
    {
        public Formation(string title)
        {
            Title = title;
            players_ = new List<Player>();
            weaksideOffset_ = 1;
            strongsideOffset_ = 1;
            players_.Add(new Player("C1", 0, 0, ReceiverTag.NONE));
        }

        public void AddLinePlayer(string position,
                                  bool weakside,
                                  bool onLine,
                                  int split = 1,
                                  ReceiverTag which = ReceiverTag.NONE)
        {
            int x;
            if (weakside) {
                x = weaksideOffset_ + split;
                weaksideOffset_ += split + 1;
                x *= -1;
            } else { // strong side
                x = strongsideOffset_ + split;
                strongsideOffset_ += split + 1;
            }
            int y = onLine ? 0 : -1;

            players_.Add(new Player(position, x, y, which));
        }

        public void AddBackfieldPlayer(string position,
                                       int depth,
                                       int offset,
                                       ReceiverTag which = ReceiverTag.NONE)
        {
            players_.Add(new Player(position, offset, -depth, which));
        }

        public void accept(IVisitor visitor)
        {
            visitor.visitFormation(this);
        }

        public void applyToPlayers(IVisitor visitor)
        {
            foreach(Player p in players_) {
                p.accept(visitor);
            }
        }

        public override string ToString()
        {
            return Title;
        }

        public string Title { get; private set; }
        int weaksideOffset_, strongsideOffset_;
        List<Player> players_;
    }

}
