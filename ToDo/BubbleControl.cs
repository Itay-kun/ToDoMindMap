using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using MindOrgenizerToDo;
using System.Drawing.Drawing2D;
using MindOrgenizerToDo.ToDo;



public class BubbleControl : UserControl
{
    private readonly Panel Background;
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
        this.Hide();

        InitializeControl(item);
        RegisterEventHandlers();
    }

    private void InitializeControl()
    {
        this.SuspendLayout();
        // 
        // BubbleControl
        // 
        this.AllowDrop = true;
        this.AutoSize = true;
        this.Name = "BubbleControl";
        this.Size = new System.Drawing.Size(200, 200);
        this.LocationChanged += new System.EventHandler(this.BubbleControl_LocationChanged);
        this.DragDrop += new System.Windows.Forms.DragEventHandler(this.BubbleControl_DragDrop);
        this.DragEnter += new System.Windows.Forms.DragEventHandler(this.BubbleControl_DragEnter);
        this.DragOver += new System.Windows.Forms.DragEventHandler(this.BubbleControl_DragOver);
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.BubbleControl_Paint);
        this.Move += new System.EventHandler(this.BubbleControl_Move);
    }

    private void InitializeControl(ToDoItem task)
    {
        this.SuspendLayout();
        this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;


        Font defaultFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
        
        this.Height = 180;
        this.Width = this.Height;

        this.assigneeLabel = new System.Windows.Forms.Label
        {
            Padding = new System.Windows.Forms.Padding(20, this.Height / 10, 0, this.Height / 10)
        };
    

        // 
        // titleLabel
        //
        this.titleLabel = new System.Windows.Forms.Label {
            Name = "titleLabel",
            Text = Item.Title,
            Padding = new System.Windows.Forms.Padding(20, this.Height / 8, 0, this.Height / 10),
            Size = new System.Drawing.Size(100, 18),
            AutoSize = true,
            Font = defaultFont,
            Dock = System.Windows.Forms.DockStyle.Top,
            TabIndex = 0
        };
        
        // 
        // statusLabel
        // 
        this.statusLabel = new System.Windows.Forms.Label
        {
            Name = "statusLabel",
            Text = "Status: " + task.Status.ToString(),
            Padding = new System.Windows.Forms.Padding(30, this.Height / 10, 0, this.Height / 10),
            Size = new System.Drawing.Size(100, 18),
            AutoSize = true,
            Font = defaultFont,
            Dock = System.Windows.Forms.DockStyle.Top,
            TabIndex = 1
        };  

        // 
        // assigneeLabel
        //
        if (UserSession.GetInstance().isUserAdmin())
        {
            //ToDo: if combobox value is me, no need to show assignee label
            this.assigneeLabel.Name = "assigneeLabel";
            this.assigneeLabel.Text = "Assigned to: " + UserSession.GetInstance().GetUserName(Item.Assignee);
            this.assigneeLabel.TabIndex = 2;

            this.assigneeLabel.Size = new System.Drawing.Size(75, 18);
            this.assigneeLabel.AutoSize = true;
            
            this.assigneeLabel.Font = defaultFont;
            this.assigneeLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
                        
            this.Controls.Add(this.assigneeLabel);
        }

   

        // 
        // BubbleControl
        // 
        this.Controls.Add(this.statusLabel);
        this.Controls.Add(this.Background);
        this.Controls.Add(this.titleLabel);
        
        this.Name = "Task "+Item.Id.ToString()+" Bubble";
        
        UpdateColorBasedOnStatus();

        //this.PerformLayout();
        this.ResumeLayout(true);
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
        Console.WriteLine("BubbleControl | Override | OnPaint");
        base.OnPaint(e);
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
        Console.WriteLine();    Console.Write("BubbleControl | OnMouseDown");
        base.OnMouseDown(e);
        
        // Start the drag only if the right part of the control is interacted with, if necessary
        // Begin the drag operation with this bubble as the source
        if (e.Button == MouseButtons.Middle)  // Check if the left button was pressed
        {
            Console.Write(" | MiddleButton");   Console.WriteLine();
            this.DoDragDrop(this, DragDropEffects.Link);
        }
        if (e.Button == MouseButtons.Right)
        {
            this.Parent.CreateGraphics().MeasureString(this.assigneeLabel.Text, this.assigneeLabel.Font);
        }
        this.BringToFront();
    }
    


    

    private void BubbleControl_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(BubbleControl)))
        {
            e.Effect = DragDropEffects.Link;
            //Console.WriteLine("BubbleControl_DragEnter " + sender.GetType()+" "+sender.ToString());
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
            Console.WriteLine($"DragOver on {this.Name}");
        }
        else
        {
           
            Console.WriteLine("BubbleControl_DragOver" + sender.GetType()+" "+e.Data.GetDataPresent(typeof(BubbleControl))); Console.Beep();
            e.Effect = DragDropEffects.None;  // Continue to disallow
        }
    }

    public Point GetCenterPoint() { 
        return new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2); 
    }

    private void DrawArrow(BubbleControl source, BubbleControl target)
    {
        if (source == null || target == null) return;
        Console.WriteLine($"Drawing arrow from {source.Name} to {target.Name}");
        Console.WriteLine("draw arrow from "+source.Name + " to " + target.Name);
        var panel = this.Parent as Panel;

        //Graphics g = panel.CreateGraphics();
        Graphics g = source.CreateGraphics();
        Pen pen = new Pen(Color.Black, 2);
        pen.CustomEndCap = new AdjustableArrowCap(5, 5);

        // Calculate center points of source and target for line drawing
        Point sourceCenter = source.GetCenterPoint();
        Point targetCenter = target.GetCenterPoint();

        g.DrawLine(pen, sourceCenter, targetCenter);
        g.Dispose();
    }

    public void DrawArrowToTarget(BubbleControl target)
    {
        DrawArrow(this, target);
    }

    private void BubbleControl_DragDrop(object sender, DragEventArgs e)
    {
        var target = sender as BubbleControl;  // The bubble where the mouse was released
        var source = e.Data.GetData(typeof(BubbleControl)) as BubbleControl; // The bubble where the drag started

        if (target != null && source != null && target != source)
        {
            // Execute your linking logic here
            Console.WriteLine($"DragDrop from {source.Name} to {target.Name}");
            ((ToDoListForm)this.ParentForm).connectionsManager.ConnectBubbles(source, target);
            Console.WriteLine("Connecting "+target.Name + " to " + source.Name);
            Console.WriteLine(target.Item.Id + " to " + source.Item.Id);
            DrawArrow(source, target);
        }
    }

    private void BubbleControl_LocationChanged(object sender, EventArgs e)
    {
        Console.WriteLine("BubbleControl | Location Changed");
        Console.WriteLine(e);
    }

    private void BubbleControl_Move(object sender, EventArgs e)
    {
        Console.WriteLine("BubbleControl | Move");
    }

    private void BubbleControl_Paint(object sender, PaintEventArgs e)
    {
        Console.WriteLine("bubble control paint");
    }

}
