using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MindOrgenizerToDo.ToDo.Connectors
{
    public class Connection
    {
        public BubbleControl Source { get; set; }
        public BubbleControl Target { get; set; }

        private Graphics g { get; set; }

        private Pen connectionPen;

        public Connection(BubbleControl source, BubbleControl target)
        {
            Source = source;
            Target = target;

            Console.WriteLine("create a conection between " + source.Name + " and " + target.Name);

            connectionPen = new Pen(Color.Black, 2) { CustomEndCap = new AdjustableArrowCap(5, 5) };
            if (Source.Parent == null) return;


        }

        public void Draw(Graphics g)
        {
            //g = g ?? Source.Parent.CreateGraphics();
            g = g ?? Source.CreateGraphics();
            if (g == null) { Console.ForegroundColor = System.ConsoleColor.Red; Console.WriteLine("graphic context is null"); }
            if (Source == null || Target == null) return;
            //Console.WriteLine("Redrawing connection between " + Source.Name + " and " + Target.Name);

            Point sourceCenter = Source.GetCenterPoint();
            Point targetCenter = Target.GetCenterPoint();

            g.DrawLine(connectionPen, sourceCenter, targetCenter);
        }

    }
}
