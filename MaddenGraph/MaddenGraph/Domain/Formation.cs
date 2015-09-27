using System;
using System.Collections.Generic;
using System.Linq;

namespace MaddenGraph.Domain
{
    public class Formation
    {
        public Formation(int weak, int strong)
        {
            Positions = Enumerable.Range(0, 11);
        }

        public List<object> Positions { get; }
    }
}
