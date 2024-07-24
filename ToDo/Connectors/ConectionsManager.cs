using MindOrgenizerToDo.ToDo.Connectors;
using System;
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

        MessageBox.Show("Connection between " + source.Name + " and " + target.Name + " added.");
        MessageBox.Show("Total of "+connections.Count+" connections.");
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


    private void RedrawConnections(object sender, PaintEventArgs e)
    {
        foreach (var connection in connections)
        {
            Console.WriteLine("Redrawing connection between " + connection.Source.Name + " and " + connection.Target.Name);
            connection.Draw(e.Graphics);
        }
    }
}
