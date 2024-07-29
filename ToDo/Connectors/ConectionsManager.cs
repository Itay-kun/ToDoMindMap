using MindOrgenizerToDo.ToDo.Connectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class ConnectionsManager
{
    private List<Connection> connections;
    private Panel drawingPanel;

    public ConnectionsManager(Panel panel)
    {
        Console.WriteLine("ConnectionsManager | Constructor called");
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
        
        Console.WriteLine(); 
        Console.Write("ConnectionsManager | AddConnection: ");
        Console.Write("Connection between " + source.Name + " and " + target.Name + " added.");
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
        Graphics g = e.Graphics;
        foreach (var connection in connections)
        {
            connection.Draw(g);
        }
    }

    public void SimulateDragDrop(BubbleControl sourceControl, BubbleControl targetControl)
    {
        if (drawingPanel == null || sourceControl == null || targetControl == null)
        {
            throw new ArgumentNullException("Panel, source control, or target control cannot be null.");
        }

        // Create the data object to be dragged
        DataObject data = new DataObject(sourceControl);

        // Get the target control's position relative to the panel
        Point targetPosition = targetControl.Location;

        Console.WriteLine($"Simulating DragEnter on {targetControl.Name}");
        DragEventArgs dragEnterArgs = new DragEventArgs(
            data,
            0,
            targetPosition.X,
            targetPosition.Y,
            DragDropEffects.Link,
            DragDropEffects.Link
        );
        drawingPanel.GetType().GetMethod("OnDragEnter", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(drawingPanel, new object[] { dragEnterArgs });

        Console.WriteLine($"Simulating DragDrop on {targetControl.Name}");
        DragEventArgs dragDropArgs = new DragEventArgs(
            data,
            0,
            targetPosition.X,
            targetPosition.Y,
            DragDropEffects.Link,
            DragDropEffects.Link
        );
        drawingPanel.GetType().GetMethod("OnDragDrop", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(drawingPanel, new object[] { dragDropArgs });
    }
}
