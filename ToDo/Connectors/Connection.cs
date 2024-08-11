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

        public Panel bubblesPanel { get; set; }
        private bool IsVisible { get; set; }

        private bool ShouldBeVisible
        {
            get
            {
                return Source.Visible && Target.Visible;
            }
        }



        private Graphics g { get; set; }

        private Pen connectionPen;

        public Connection(BubbleControl source, BubbleControl target, Panel panel)
        {
            Source = source;
            Target = target;

            this.bubblesPanel = panel;

            Console.WriteLine("create a conection between " + source.Name + " and " + target.Name);

            connectionPen = new Pen(Color.Black, 2) { CustomEndCap = new AdjustableArrowCap(5, 5) };
            if (Source.Parent == null) return;

            IsVisible = false;
        }

        public void Draw(Graphics g)
        {
            if (Source == null || Target == null) return;
            if (IsVisible) { Console.WriteLine("connection between " + Source.Name + " and " + Target.Name+" is visible"); return; }
            Source.Item.bubble = Source.Item.ToBubble();
            Console.WriteLine("Connection.Draw()");
            Console.WriteLine(Source.Visible + " " + Target.Visible);
            
            if (ShouldBeVisible && !IsVisible)
            {
            this.g = g ?? Source.CreateGraphics();
            if (g == null) { Console.ForegroundColor = System.ConsoleColor.Red; Console.WriteLine("graphic context is null"); }
            //Console.WriteLine("Redrawing connection between " + Source.Name + " and " + Target.Name);


                Point sourceCenter = Source.GetCenterPoint();
                Point targetCenter = Target.GetCenterPoint();

                g.DrawLine(connectionPen, sourceCenter, targetCenter);

                IsVisible = true;
            }
        }

        public override string ToString()
        {
            return "Connection between " + Source.Name + " and " + Target.Name;
        }

    }
}