using System;
using Gtk;

namespace MaddenGraph
{
    public interface IVisitor
    {
        void visitFormation(Formation formation);

        void visitPlayer(Player player);
    }

    public class FormationLister : IVisitor
    {
        public void visitFormation(Formation formation)
        {
            Console.WriteLine(formation.Title);

            formation.applyToPlayers(this);
        }

        public void visitPlayer(Player player)
        {
            Console.WriteLine("{0} at {1},{2} as {3}", player.Position, player.X, player.Y, player.Receiver.ToString());
        }
    }

}

