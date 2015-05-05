using System;
using Gtk;

namespace MaddenGraph
{
    public class PlayCanvasController
    {
        public PlayCanvasController (DrawingArea canvas)
        {
            canvas_ = canvas;
            canvas_.ConfigureEvent += OnConfigureEvent;
            canvas_.ExposeEvent += OnExposeEvent;
        }

        private Cairo.Matrix ComputeBasis()
        {
            double scalefac = YdsWidth / PixWidth;

            return new Cairo.Matrix(1/scalefac, 0,
                                    0,          -1/scalefac,
                                    PixWidth/2, PixHeight-(20/scalefac));
        }

        public void OnConfigureEvent(object o, ConfigureEventArgs args)
        {
            PixWidth = args.Event.Width; PixHeight = args.Event.Height;
            YdsHeight = PixHeight / (YdsWidth / PixWidth);
            basis_ = ComputeBasis();
            DrawBackground();
        }

        public void OnExposeEvent(object o, ExposeEventArgs args)
        {
            PixWidth = args.Event.Area.Width; PixHeight = args.Event.Area.Height;
            YdsHeight = PixHeight / (YdsWidth / PixWidth);
            basis_ = ComputeBasis();
            DrawBackground();
        }

        private void SetSaneBasis(Cairo.Context g)
        {
            g.Antialias = Cairo.Antialias.Subpixel;
            g.Transform(basis_);
        }

        void ClearCanvas (Cairo.Context g)
        {
            g.Transform(new Cairo.Matrix()); // back to the identity
            g.LineWidth = 0;
            g.SetSourceRGB(0.98, 0.98, 0.98);
            g.MoveTo(       0,         0);
            g.LineTo(       0, PixHeight);
            g.LineTo(PixWidth, PixHeight);
            g.LineTo(PixWidth,         0);
            g.ClosePath();
            g.Fill();
            g.Transform(basis_); // back to sanity
        }

        private void DrawFieldLine(Cairo.Context g, double xMin, double xMax, double y)
        {
            g.SetSourceRGB(0.65, 0.65, 0.65);
            g.LineWidth = 0.1;
            g.MoveTo(xMin,   y);
            g.LineTo(xMax,   y);
            g.LineTo(xMax, y+0.25);
            g.LineTo(xMin, y+0.25);
            g.ClosePath();
            g.Stroke();
        }

        private void DrawYardLine(Cairo.Context g, double y)
        {
            DrawFieldLine(g, -LinesBound, LinesBound, y);
        }

        private void DrawHashMarks(Cairo.Context g, double y)
        {
            DrawFieldLine(g, -LinesBound, -(LinesBound-0.67), y);
            DrawFieldLine(g, -HashStart,  -HashEnd, y);
            DrawFieldLine(g,  HashStart,   HashEnd, y);
            DrawFieldLine(g,  LinesBound,  (LinesBound-0.67), y);
        }

        public void DrawBackground()
        {
            using (Cairo.Context g = Gdk.CairoHelper.Create(canvas_.GdkWindow)) {
                ClearCanvas(g);
                //SetSaneBasis(g);

                for(int i = -20; i < YdsHeight - 20; i += 5) {
                    DrawYardLine(g, i);
                    for(int j = 1; j < 5; j++) {
                        DrawHashMarks(g, i+j);
                    }
                }

            }
        }

        DrawingArea canvas_;
        Cairo.Matrix basis_;

        private double PixWidth { get; set; }
        private double PixHeight { get; set; }
        private const double YdsWidth = 53.33;
        private double YdsHeight { get; set; }
        private double LinesBound = 0.95*YdsWidth/2.0;
        private double HashStart = 3; // roughly
        private double HashEnd = 3.67;
    }
}

