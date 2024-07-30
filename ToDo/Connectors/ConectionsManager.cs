using MindOrgenizerToDo.ToDo.Connectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class ConnectionsManager
{
    private List<Connection> connections;
    private Panel drawingPanel;
    private Graphics g;

    public ConnectionsManager(Panel panel)
    {
        Console.WriteLine("ConnectionsManager | Constructor called");
        this.drawingPanel = panel;
        this.connections = new List<Connection>();

        // Create a new Graphics object
        g = drawingPanel.CreateGraphics();

        // Subscribe to panel's Paint event to redraw connections
        this.drawingPanel.Paint += RedrawConnections;
    }

    public void AddConnection(BubbleControl source, BubbleControl target)
    {
        var connection = new Connection(source, target);
        connections.Add(connection);
        drawingPanel.Invalidate(); // Trigger a repaint to show the new connection
        
        Console.WriteLine(); 
        Console.Write("ConnectionsManager | AddConnection: ");
        Console.Write(connection.ToString());
        Console.Write("| Total of "+connections.Count+" connections.");
        Console.WriteLine();
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
        foreach (Connection connection in connections)
        {
            Console.WriteLine("Redrawing "+connection.ToString());
            connection.Draw(g);
        }
    }
}
