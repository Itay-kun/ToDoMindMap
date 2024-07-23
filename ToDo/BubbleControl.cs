using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using MindOrgenizerToDo;
using System.Drawing.Drawing2D;
using MindOrgenizerToDo.ToDo;
using MindOrgenizerToDo.Services;


public class BubbleControl : UserControl
{
    private Panel Background;
    private Label titleLabel;
    private Label assigneeLabel;
    private Label statusLabel;
    public int depth; // Depth in the hierarchy
    public List<BubbleControl> ChildBubbles { get; private set; } // List to store child BubbleControls

    private bool isOverlapping { get; set; }

    public ToDoItem Item { get; private set; }
    public List<ToDoItem> ChildTasks { get; private set; } // Field to store child tasks

    public BubbleControl(ToDoItem item, Point location)
    {
        //Console.WriteLine("BubbleControl | constructor with item"+item.ToString());
        this.Item = item;
        this.depth = this.Item.Level;
        this.ChildTasks = new List<ToDoItem>();
        this.AllowDrop = true;
        this.isOverlapping = false;

        InitializeControl(item);
        RegisterEventHandlers();
    }

    private void InitializeControl(ToDoItem task)
    {
         /*
          * Console.WriteLine("BubbleControl | initializeControl | "+task.Title+" | Level: "+task.Level);
       
        // 
        // Background
        // 
        this.Background = new System.Windows.Forms.Panel();
        this.Background.BackColor = System.Drawing.Color.DarkKhaki;
        this.Background.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.Background.Location = new System.Drawing.Point(105, 18);
        this.Background.Name = "Background";
        this.Background.Padding = new System.Windows.Forms.Padding(3);
        this.Background.Dock = System.Windows.Forms.DockStyle.Fill;
        this.Background.Size = new System.Drawing.Size(42, 116);
        this.Background.TabIndex = 0;*/

        this.assigneeLabel = new System.Windows.Forms.Label();
        
        this.statusLabel = new System.Windows.Forms.Label();
        //this.SuspendLayout();
        
        
        // 
        // titleLabel
        //
        this.titleLabel = new System.Windows.Forms.Label();
        this.titleLabel.Padding = new System.Windows.Forms.Padding(0, this.Height/3, 3, 0);
        
        this.titleLabel.AutoSize = true;
        this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
        this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new System.Drawing.Size(35, 18);
        this.titleLabel.TabIndex = 1;
        this.titleLabel.Text = Item.Title;

        // 
        // assigneeLabel
        //
        if (UserSession.GetInstance().isUserAdmin())
        {
            this.assigneeLabel.Text = "Assigned to: " + Item.Assignee.ToString();
            this.assigneeLabel.AutoSize = true;
            this.assigneeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.assigneeLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.assigneeLabel.Name = "assigneeLabel";
            this.assigneeLabel.Size = new System.Drawing.Size(75, 18);
            this.assigneeLabel.TabIndex = 2;
            this.assigneeLabel.Text = "Assigned to: " + Item.Assignee.ToString(); //Add the assignee nickname instead?
            this.Controls.Add(this.assigneeLabel);
        }
        
        // 
        // statusLabel
        // 
        this.statusLabel.AutoSize = true;
        this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
        this.statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
        this.statusLabel.Name = "statusLabel";
        this.statusLabel.Size = new System.Drawing.Size(54, 18);
        this.statusLabel.TabIndex = 3;
        this.statusLabel.Text = "Status:" + Item.Status.ToString();

        // 
        // BubbleControl
        // 
        this.Controls.Add(this.statusLabel);
        this.Controls.Add(this.Background);
        this.Controls.Add(this.titleLabel);
        
        this.Name = "Task "+Item.Id.ToString()+" Bubble";
        this.ResumeLayout(false);
        UpdateColorBasedOnStatus();
        
        //this.Location = new Point(this.Item.Level * this.Width + 20, 0); Console.WriteLine(this.Item.Title+" Location: " + this.Location+" Level: " + this.Item.Level);

        this.PerformLayout();

    }

    public void AddChild(BubbleControl child)
    {
        MessageBox.Show("addChild");
        Console.WriteLine("Add child");
        child.depth = child.Item.Level;

        int childXOffset = this.Width + 20*child.depth; // Start displaying child bubbles below the parent bubble
        //TODO: replace 20 with a variable
        //TODO: replace child y offset or remove it?

        int childYOffset = 10 + ChildBubbles.Count * (child.Height + 10); // Start displaying child bubbles below the parent bubble
        Console.WriteLine("Adding child: " + child.Item.Title + " to parent: " + Item.Title);

        ChildBubbles.Add(child);
        child.Location = new Point(childXOffset, childYOffset);  // Position child relative to parent
        this.Controls.Add(child);
        DrawArrow(this, child);
    }

    private void RegisterEventHandlers()
    {
        this.DoubleClick += BubbleControl_DoubleClick;
        //this.MouseDown += BubbleControl_MouseDown;
        this.DragDrop += BubbleControl_DragDrop;
        this.DragEnter += BubbleControl_DragEnter;
        this.DragOver += BubbleControl_DragOver;
        this.Paint += BubbleControl_Paint;
    }

    public void AddChildTask(ToDoItem childTask)
    {
        MessageBox.Show("AddChildTask");
        Console.WriteLine("Add child task");
        ChildTasks.Add(childTask);
        // Update UI to reflect the addition of a child task
        UpdateChildTaskDisplay();
    }

    private void UpdateChildTaskDisplay()
    {
        Console.WriteLine("BubbleControl | UpdateChildTaskDisplay");

        int childYOffset = 0;//this.Height + 10; // Start displaying child tasks below the parent task
        
        foreach (var childTask in ChildTasks)
        {
            int childDepth = childTask.Level; // Get the depth of the childTask
            int childXOffset = (this.Width + 20)*childDepth; // Start displaying child bubbles below the parent bubble
            Location = new Point(childXOffset, childYOffset);
            //BubbleControl childBubble = new BubbleControl(childTask, this.Location);
            BubbleControl childBubble = childTask.ToBubble(this.Location);
            this.Controls.Add(childBubble);
            childYOffset += childBubble.Height + 10;
        }
    }

    private void UpdateColorBasedOnStatus()
    {
        switch (Item.Status)
        {
            case TodoStatus.COMPLETED:
                this.BackColor = Color.Green;
                break;
            case TodoStatus.IN_PROGRESS:
                this.BackColor = Color.Yellow;
                break;
            case TodoStatus.TODO:
                this.BackColor = Color.Red;
                break;
            default:
                this.BackColor = Color.Gold;
                break;
        }
    }

    private void BubbleControl_DoubleClick(object sender, EventArgs e)
    {
        // Open detail form
        SingleToDoEdit detailForm = new SingleToDoEdit(Item, this.ParentForm as ToDoListForm);
        detailForm.Show();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        //Console.WriteLine("BubbleControl | OnPaint");
        base.OnPaint(e);
        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Create a circular clip region
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(0, 0, this.Width, this.Height);
        this.Region = new Region(path);

        // Draw the circle border
        graphics.DrawEllipse(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
    }

    
    protected override void OnMouseDown(MouseEventArgs e)
    {
        Console.WriteLine("BubbleControl | OnMouseDown");
        base.OnMouseDown(e);
        // Start the drag only if the right part of the control is interacted with, if necessary
        // Begin the drag operation with this bubble as the source
        if (e.Button == MouseButtons.Middle)  // Check if the left button was pressed
        {
            this.DoDragDrop(this, DragDropEffects.Link);
            //this.DoDragDrop(this, DragDropEffects.Move);
        }
        this.BringToFront();
    }
    

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // BubbleControl
            // 
            this.AllowDrop = true;
            this.AutoSize = true;
            this.Name = "BubbleControl";
            this.Size = new System.Drawing.Size(169, 147);
            this.LocationChanged += new System.EventHandler(this.BubbleControl_LocationChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.BubbleControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.BubbleControl_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.BubbleControl_DragOver);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BubbleControl_Paint);
            this.Move += new System.EventHandler(this.BubbleControl_Move);
            this.ResumeLayout(false);
    }
    

    private void BubbleControl_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(BubbleControl)))
        {
            e.Effect = DragDropEffects.Link;
            Console.WriteLine("BubbleControl_DragEnter " + sender.GetType()+" "+sender.ToString());
        }
        else
        {
            e.Effect = DragDropEffects.None;
            Console.Beep();
        }
    }

    private void BubbleControl_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(BubbleControl)))
        {
            e.Effect = DragDropEffects.Link;  // Continue to allow linking here
        }
        else
        {
           
            Console.WriteLine("BubbleControl_DragOver" + sender.GetType()+" "+e.Data.GetDataPresent(typeof(BubbleControl))); Console.Beep();
            e.Effect = DragDropEffects.None;  // Continue to disallow
        }
    }

    private Point GetCenterPoint() { 
        return new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2); 
    }

    private void DrawArrow(BubbleControl source, BubbleControl target)
    {
        Console.WriteLine("draw arrow from "+source.Name + " to " + target.Name);
        var panel = this.Parent as Panel;  // Assuming the Parent of bubbles is a Panel

        Graphics g = panel.CreateGraphics();
        Pen pen = new Pen(Color.Black, 2);
        pen.CustomEndCap = new AdjustableArrowCap(5, 5);

        // Calculate center points of source and target for line drawing
        Point sourceCenter = source.GetCenterPoint();
        Point targetCenter = new Point(target.Location.X + target.Width / 2, target.Location.Y + target.Height / 2);

        g.DrawLine(pen, sourceCenter, targetCenter);
        g.Dispose();
    }

    private void BubbleControl_DragDrop(object sender, DragEventArgs e)
    {
        var target = sender as BubbleControl;  // The bubble where the mouse was released
        var source = e.Data.GetData(typeof(BubbleControl)) as BubbleControl; // The bubble where the drag started

        if (target != null && source != null && target != source)
        {
            // Execute your linking logic here
            ((ToDoListForm)this.ParentForm).connectionsManager.ConnectBubbles(source, target);
            Console.WriteLine("Connecting "+target.Name + " to " + source.Name);
            Console.WriteLine(target.Item.Id + " to " + source.Item.Id);
            DrawArrow(source, target);
        }
    }

    private void BubbleControl_LocationChanged(object sender, EventArgs e)
    {
        Console.WriteLine(e.GetType());
        Console.WriteLine("BubbleControl | Location Changed");
        this.Refresh();
    }

    private void BubbleControl_Move(object sender, EventArgs e)
    {
        Console.WriteLine("BubbleControl | Move");
    }

    private void BubbleControl_Paint(object sender, PaintEventArgs e)
    {
        Control currentControl = sender as Control;
        Control ref_control = sender as Control;

        if (currentControl == null)
            return;
        
        BubbleControl bubble = sender as BubbleControl;

        foreach (Control control in currentControl.Parent.Controls)
        {
            if (control != currentControl && control.Bounds.IntersectsWith(currentControl.Bounds))
            {
                ref_control = control;
                isOverlapping = true;
                Console.WriteLine(); 
                Console.Write(currentControl.Name + " was Overlapping "+ control.Name);
                break;
            }
            if(!this.isOverlapping) break;
        }

        if (isOverlapping)
        {
            // Handle the overlap scenario
            //e.Graphics.DrawString("Overlapping", currentControl.Font, Brushes.Red, new PointF(10, 10));
            currentControl.Location = new Point(ref_control.Location.Y + ref_control.Height, 10);
            this.isOverlapping = false;
            Console.Write("but it was moved"); Console.WriteLine(); 
        }
        else
        {
            // Handle the non-overlap scenario
            Console.WriteLine(bubble.Item.Title + " Not Overlapping");
            //e.Graphics.DrawString("Not Overlapping", currentControl.Font, Brushes.Green, new PointF(10, 10));
        }
    }

}
