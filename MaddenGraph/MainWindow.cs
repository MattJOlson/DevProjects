using System;
using Gtk;
using Cairo;
using MaddenGraph;

public partial class MainWindow: Gtk.Window
{    
    const int id = 1;

    public MainWindow (): base (Gtk.WindowType.Toplevel)
    {       
        Build ();
        data_ = new MaddenGraphData();
        data_.ReadFormations();
        statusbar.Push(id, String.Format("Read {0} formations", data_.FormationCount));
        //FormationLister lister = new FormationLister();
        //data.applyToFormations(lister);
        FormationListPopulator pop = new FormationListPopulator(formationList);
        data_.applyToFormations(pop);

        PlayCanvasController playCanvas = new PlayCanvasController(PlayCanvas);
    }

    protected void OnDeleteEvent (object sender, DeleteEventArgs a)
    {
        Application.Quit ();
        a.RetVal = true;
    }

    protected void FileQuit (object sender, EventArgs e)
    {
        Application.Quit();
    }

    MaddenGraphData data_;
}
