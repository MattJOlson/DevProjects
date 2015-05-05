using System;
using Gtk;

namespace MaddenGraph
{
    public class FormationListController
    {
        public FormationListController (TreeView list)
        {
            list_ = list;

            col_ = new TreeViewColumn();
            col_.Title = "Formation";
            list_.AppendColumn(col_);

            data_ = new ListStore(typeof(Formation));
            list_.Model = data_;

            renderer_ = new CellRendererText();
            col_.PackStart(renderer_, true);
            col_.SetCellDataFunc(renderer_, new TreeCellDataFunc(renderFormationTitle));

            list_.Selection.Changed += FormationListSelected;
        }

        public void FormationListSelected(object o, EventArgs args)
        {
            TreeSelection s = o as TreeSelection;
            TreeIter iter; TreeModel model;

            if(s.GetSelected(out model, out iter)) {
                Formation f = model.GetValue(iter, 0) as Formation;
                FormationLister l = new FormationLister();
                f.accept(l);
            }
        }

        public void renderFormationTitle(TreeViewColumn col, CellRenderer cell, TreeModel model, TreeIter iter)
        {
            Formation f = (Formation)model.GetValue(iter, 0);
            (cell as CellRendererText).Text = f.Title;
        }

        public void AddFormation(Formation formation)
        {
            data_.AppendValues(formation);
        }

        TreeView list_;
        TreeViewColumn col_;
        ListStore data_;
        CellRenderer renderer_;
    }
}

