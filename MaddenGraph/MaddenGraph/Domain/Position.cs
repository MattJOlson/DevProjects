using MaddenGraph.Util;

namespace MaddenGraph.Domain
{
    public class Position
    {
        public Position(Pt pos)
        {
            Pos = pos;
        }

        public Pt Pos { get; }
    }
}