using MaddenGraph.Util;

namespace MaddenGraph.Domain
{
    public class Position
    {
        private Position(Pt pos, int tag)
        {
            Pos = pos;
            Tag = tag;
        }

        public static Position Eligible(Pt pos, int tag) // TODO: Is tag really an int?
        {
            return new Position(pos, tag); 
        }

        public static Position Ineligible(Pt pos)
        {
            return new Position(pos, -1);
        }

        public Pt Pos { get; }
        public int Tag { get; }
        public bool IsEligible => 0 < Tag;
    }
}