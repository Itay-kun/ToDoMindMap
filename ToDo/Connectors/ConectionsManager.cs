using MindOrgenizerToDo.ToDo.Connectors;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ConnectionsManager
{
    private List<Connection> connections;
    private Panel drawingPanel;

    public ConnectionsManager(Panel panel)
    {
        this.drawingPanel = panel;
        this.connections = new List<Connection>();
        // Subscribe to panel's Paint event to redraw connections
        this.drawingPanel.Paint += RedrawConnections;
    }

    public void AddConnection(BubbleControl source, BubbleControl target)
    {
        var connection = new Connection(source, target);
        connections.Add(connection);
        drawingPanel.Invalidate(); // Trigger a repaint to show the new connection
    }

    public void ConnectBubbles(BubbleControl source, BubbleControl target)
    {
        AddConnection(source, target);
    }

    public void RemoveConnection(Connection connection)
    {
        connections.Remove(connection);
        drawingPanel.Invalidate(); // Trigger a repaint to update the view
    }

    public IEnumerable<Connection> GetConnections()
    {
        return connections.AsReadOnly();
    }

    private void DrawArrow(Graphics g, Connection connection)
    {
        if (connection.Source == null || connection.Target == null) return;

        Point sourceCenter = new Point(
            connection.Source.Location.X + connection.Source.Width / 2,
            connection.Source.Location.Y + connection.Source.Height / 2
        );
        Point targetCenter = new Point(
            connection.Target.Location.X + connection.Target.Width / 2,
            connection.Target.Location.Y + connection.Target.Height / 2
        );

        Pen pen = new Pen(Color.Black, 2) { CustomEndCap = new AdjustableArrowCap(5, 5) };
        g.DrawLine(pen, sourceCenter, targetCenter);
        pen.Dispose();
    }

    private void RedrawConnections(object sender, PaintEventArgs e)
    {
        foreach (var connection in connections)
        {
            DrawArrow(e.Graphics, connection);
        }
    }
}
