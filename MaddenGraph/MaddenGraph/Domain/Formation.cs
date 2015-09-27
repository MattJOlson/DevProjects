using System;
using System.Collections.Generic;
using System.Linq;

namespace MaddenGraph.Domain
{
    public class Formation
    {
        public Formation(int weak, int strong)
        {
            Positions = Enumerable.Range(0, 11).ToList();
            StrongSideReceivers = Positions.Take(strong).ToList();
            WeakSideReceivers = Positions.Skip(strong).Take(weak).ToList();

            var backfield = 5 - (strong + weak);
            BackfieldReceivers = Positions.Skip(strong + weak).Take(backfield).ToList();
        }

        public List<int> Positions { get; }
        public List<int> EligibleReceivers => WeakSideReceivers.Concat(StrongSideReceivers).Concat(BackfieldReceivers).ToList();

        public List<int> StrongSideReceivers { get; }
        public List<int> WeakSideReceivers { get; }
        public List<int> BackfieldReceivers { get; }
    }
}
