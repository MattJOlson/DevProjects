namespace MaddenGraph.Util
{
    public struct Pt
    {
        public int X { get; }
        public int Y { get; }

        private Pt(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Pt O => Pt.At(0, 0);

        public static Pt At(int x, int y)
        {
            return new Pt(x, y);
        }

        public static Pt operator +(Pt lhs, Pt rhs)
        {
            return Pt.At(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Pt operator -(Pt p)
        {
            return Pt.At(-p.X, -p.Y);
        }

        public static Pt operator -(Pt lhs, Pt rhs)
        {
            return lhs + -rhs;
        }
    }
}