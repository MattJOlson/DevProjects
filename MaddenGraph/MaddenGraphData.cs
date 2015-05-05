using System;
using System.IO;
using System.Collections.Generic;

namespace MaddenGraph
{
    public class MaddenGraphData
    {
        public MaddenGraphData ()
        {
            formations_ = new List<Formation>();
        }

        // Reads all the formations (*.mgf) from ./formations/
        // Apparently I have to be obnoxiously explicit about setting the working directory
        public void ReadFormations()
        {
            string[] formationFiles = Directory.GetFiles("./formations");
            foreach (string file in formationFiles) {
                Console.WriteLine(file);
                FormationParser parser = new FormationParser(file);
                formations_.Add(parser.Parse());
            }
        }

        public void applyToFormations(IVisitor visitor)
        {
            foreach (Formation f in formations_) {
                f.accept(visitor);
            }
        }

        public int FormationCount { get { return formations_.Count; } }

        List<Formation> formations_;
    }
}

