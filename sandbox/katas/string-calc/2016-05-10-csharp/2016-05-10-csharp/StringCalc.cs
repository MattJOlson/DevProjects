using System.Linq;

namespace String_calc_2016_05_10_csharp
{
    public static class StringCalc
    {
        public static int Calc(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            return input.Split(',', '\n')
                        .Select(int.Parse)
                        .Aggregate(0, (x, y) => x + y);
        }
    }
}