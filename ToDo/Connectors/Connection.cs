using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MindOrgenizerToDo.ToDo.Connectors
{
    public class Connection
    {
        public BubbleControl Source { get; set; }
        public BubbleControl Target { get; set; }
        private Pen connectionPen;

        public Connection(BubbleControl source, BubbleControl target)
        {
            Source = source;
            Target = target;
            Console.WriteLine("create a conection between "+source.Name + " and " + target.Name);

            connectionPen = new Pen(Color.Black, 2) { CustomEndCap = new AdjustableArrowCap(5, 5) };
            if (Source.Parent == null) return;
            Graphics g = Source.Parent.CreateGraphics();
            //this.Draw(Source.Parent.CreateGraphics());
        }

        public void Draw(Graphics g)
        {
            if (Source == null || Target == null) return;

            Point sourceCenter = Source.GetCenterPoint();
            Point targetCenter = Target.GetCenterPoint();

            g.DrawLine(connectionPen, sourceCenter, targetCenter);
         }
    }
}
