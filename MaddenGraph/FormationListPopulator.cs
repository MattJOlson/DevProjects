using System;
using Gtk;

namespace MaddenGraph
{

    public class FormationListPopulator : IVisitor
    {
        // Assuming we have a bare TreeView, we'll need to populate it
        public FormationListPopulator(TreeView list)
        {
            Controller = new FormationListController(list);
        }

        public void visitFormation(Formation formation)
        {
            Controller.AddFormation(formation);
        }

        public void visitPlayer(Player player)
        {
            // do nothing
        }

        public FormationListController Controller { get; private set; }
    }
}
