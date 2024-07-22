﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using MindOrgenizerToDo.Services;
using MindOrgenizerToDo.ToDo;
using MindOrgenizerToDo.ToDo.Connectors;
using MindOrgenizerToDo.User;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//ToDo: add data source and data binding items to the project instead of loading everything manually, it might help with the search as well
//ToDo: add code to detect if a task's windoiw is already open and move it forward
//ToDo: Clean repetative code
namespace MindOrgenizerToDo
{
    public partial class ToDoListForm : Form
    {
        TodoService todoService = new TodoService("http://localhost:5000");
        UserService userService = new UserService("http://localhost:5000");
        UserSession session;
        Point mouseLoacation;
        HttpResponseMessage data;
        public ConnectionsManager connectionsManager;


        public ToDoListForm(UserSession session)
        {
            //Console.WriteLine("ToDoListForm: " + session.Email);
            this.session = session;
            InitializeComponent();
            DoubleBuffered = true;
            this.AllowDrop = true;

            connectionsManager = new ConnectionsManager(bubblesPanel);

            LoadAssignees();
        }

        bool isEditing = false;
        private async void ToDoListWindow_Load(object sender, EventArgs e)
        {
            HttpResponseMessage data = await todoService.GetAllTodos();
            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            this.userUpdateForm.Visible = false;
            updateDates(DateTime.Now.Date);

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);

            if (session.isUserAdmin())
            {
                Console.WriteLine("user is admin"); //for debug

                stateComboBox.Items.Add("For User:");
                stateComboBox.Items.Add("All");
            }

            UpdateBubblesPanel(todoList);
        }

        public async void updateUI()
        {
            bubblesPanel.Controls.Clear();
            HttpResponseMessage data = await todoService.GetAllTodos();
            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);
            UpdateBubblesPanel(todoList);
        }


        private async void UpdateBubblesPanel()
        {
            data = await todoService.GetAllTodos();

            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);

            bubblesPanel.Controls.Clear();

            // Create a dictionary to store the tasks by their ID for easy lookup
            Dictionary<long, BubbleControl> taskDict = new Dictionary<long, BubbleControl>();

            foreach (var todo in todoList)
            {
                BubbleControl bubble = new BubbleControl(todo, new Point(10, 10));
                taskDict[todo.Id] = bubble;
            }

            // Arrange tasks with parent-child relationships
            foreach (var bubble in taskDict.Values)
            {
                if (bubble.Item.ParentTaskId != 0 && taskDict.ContainsKey(bubble.Item.ParentTaskId))
                {
                    Console.WriteLine("ToDoListForm | UpdateBubblesPunel | showld draw the arrows from here?");
                    taskDict[bubble.Item.ParentTaskId].AddChildTask(bubble.Item);
                }
                else
                {
                    bubblesPanel.Controls.Add(bubble);
                }
            }

            // Layout the bubbles
            LayoutBubbles(bubblesPanel.Controls);
        }

        private void LayoutBubbles(Control.ControlCollection bubbles)
        {
            Console.WriteLine("ToDoListForm | LayoutBubbles: " + bubbles.Count);
            int yOffset = 10; // Initial top margin
            int xOffset = 10; // Initial left margin
            int padding = 40;

            for (int i = 0; i < bubbles.Count; i++)
            {
                BubbleControl bubble = bubbles[i] as BubbleControl;
                BubbleControl previousBubble = (i > 0) ? bubbles[i - 1] as BubbleControl : null;

                bool isSameDepth = previousBubble != null && previousBubble.depth == bubble.depth;
                Console.WriteLine("isSameDepth: " + isSameDepth);

                if (isSameDepth)
                {
                    yOffset = (previousBubble.Location.Y) + previousBubble.Height;
                    xOffset = 10;
                }
                else
                {
                    yOffset = 10;
                    if (previousBubble != null)
                    {
                        Console.WriteLine(bubble.Location.Y.ToString() + " == " + previousBubble.Location.Y + " ? " + (bubble.Location.Y == previousBubble.Location.Y));
                        xOffset = (int)bubble.depth * bubble.Width+padding;
                    }
                }

                Point newLocation = new Point(xOffset, yOffset);

                // Check if another bubble is already at the new location
                bool locationOccupied = false;

                foreach (BubbleControl otherBubble in bubbles)
                {
                    if ((otherBubble != bubble) && (otherBubble.Location == newLocation))
                    {
                        locationOccupied = true;
                        break;
                    }
                }

                /*
                foreach (BubbleControl otherBubble in bubbles)
                {
                    if (otherBubble != bubble)
                    {
                        // Create rectangles that represent the bounds of each control
                        Rectangle bubbleRect = new Rectangle(bubble.Location, bubble.Size);
                        Rectangle otherBubbleRect = new Rectangle(otherBubble.Location, otherBubble.Size);

                        // Check if these rectangles overlap
                        if (bubbleRect.IntersectsWith(otherBubbleRect))
                        {
                            locationOccupied = true;
                            //newLocation = new Point(xOffset, bubble.Location.Y+otherBubbleRect.Height);
                            break;
                        }
                    }
                }
                */

                if (locationOccupied)
                {
                    yOffset += bubble.Height + 10;
                    newLocation = new Point(xOffset, yOffset);
                    //Console.WriteLine("location occupied at: " + newLocation);
                }

                bubble.Location = newLocation;
                //Console.WriteLine("bubble location: " + bubble.Location);

                // Always update yOffset for the next bubble
                yOffset += bubble.Height + 10;
            }
            connectionsManager = new ConnectionsManager(bubblesPanel);
            
            bubblesPanel.Invalidate();
        }

        private void UpdateBubblesPanel(List<ToDoItem> todoList)
        {

           // todoList = SortToDoListByHierarchy(todoList);//Sort the items by hierarchy

            bubblesPanel.Controls.Clear();
            //bubblesPanel.ControlAdded += new ControlEventHandler(bubblesPanel_ControlAdded);

            // Create a dictionary to store the tasks by their ID for easy lookup
            Dictionary<long, BubbleControl> taskDict = new Dictionary<long, BubbleControl>();

            foreach (var todo in todoList)
            {
                Console.WriteLine();
                //Console.WriteLine("#################### | ToDoListForm | UpdateBubblesPanel | ####################"); Console.WriteLine();
                Point newPoint = new Point(10, 10);
                //BubbleControl bubble = new BubbleControl(todo, newPoint);
                BubbleControl bubble = todo.ToBubble(newPoint);
                bubble.TabIndex = (int)todo.Id;
                taskDict[todo.Id] = bubble;
            }

            // Arrange tasks with parent-child relationships
            //foreach (var bubble in taskDict.Values)
            foreach (var bubble in taskDict.Values)
            {
                if (bubble.Item.ParentTaskId != 0 && taskDict.ContainsKey(bubble.Item.ParentTaskId))
                {
                    taskDict[bubble.Item.ParentTaskId].AddChild(bubble);
                    //Console.WriteLine("adding child to " + bubble.Item.ParentTaskId);
                    bubblesPanel.Controls.Add(bubble);
                    connectionsManager.AddConnection(taskDict[bubble.Item.ParentTaskId], bubble);
                }
                else
                {
                    //Console.WriteLine("ToDoListForm | adding bubble to panel | " + bubble.Item.Title);
                    bubblesPanel.Controls.Add(bubble);
                }
            }

            connectionsManager = new ConnectionsManager(bubblesPanel);

            // Layout the bubbles
            LayoutBubbles(bubblesPanel.Controls);
        }



        private void newButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "";
            descriptionTextbox.Text = "";
            startDatePicker.Text = "";
            dueByPicker.Text = "";
        }


        private async void saveButton_Click(object sender, EventArgs e)
        {
            
            // Create the new todo
                ToDoItem myTask = new ToDoItem(
                    id: long.Parse(DateTime.Now.ToString("yyMMddHHmmss")),
                    parentTaskId: 0,
                    assignee: int.Parse(assigneeComboBox.SelectedValue.ToString()),
                    title: titleTextBox.Text,
                    description: descriptionTextbox.Text,
                    startDate: startDatePicker.Value,
                    dueDate: dueByPicker.Value, //ToDo: set the real value
                    endDate: dueByPicker.Value,
                    status: (TodoStatus)statusComboBox.SelectedIndex,
                    level: 0 //how do i calculate the level? will the 0 be ignored?
                );

                //MessageBox.Show(myTask.ToJson());
                var response = await todoService.CreateTodo(myTask.ToJson());

                // Output the response from the server
                Console.WriteLine(response);
                UpdateBubblesPanel();
                stateComboBox.SelectedIndex = 0;

            // Clear fields
            titleTextBox.Text = "";
            descriptionTextbox.Text = "";

            //Maybe add a function to reset dates for reusability?
            updateDates(DateTime.Now.Date);
            isEditing = false;
        }

        private void updateDates(DateTime date = new DateTime())
        {
            startDatePicker.Text = date.ToString();
            dueByPicker.Text = date.AddDays(2).ToString();
        }

        public async void LoadAssignees()
        {
            //Console.WriteLine("loading assignees: ");
            try
            {
                data = await userService.GetPossibleAssigns();
                
                string json = await data.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var assignees = JsonSerializer.Deserialize<List<UserModel>>(json,options);
                foreach (var assignee in assignees)
                {
                    //Console.WriteLine("assignee: " + assignee.ToString());
                }

                assigneeComboBox.DisplayMember = "Name";
                assigneeComboBox.ValueMember = "Id";
                assigneeComboBox.DataSource = assignees;  // Set DataSource last
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load assignees: " + ex.Message);
            }
        }

        private async Task LoadAndDisplayTodos(Func<Task<HttpResponseMessage>> getTodosTask)
        {
            HttpResponseMessage data = await getTodosTask();

            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);
            foreach (ToDoItem todo in todoList)
            {
                ToDoItem.ToDoList.Add(todo);
                //Todo: make sure it ignores duplicates
            }
            UpdateBubblesPanel(todoList);
        }

        private async Task LoadAndDisplayCompletedTodos(Func<Task<HttpResponseMessage>> getTodosTask)
        {
            HttpResponseMessage data = await getTodosTask();

            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            List<ToDoItem> temp_list = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);
            List<ToDoItem> todoList = new List<ToDoItem>();
            foreach (ToDoItem todo in temp_list)
            {
                if (todo.Status == TodoStatus.COMPLETED) {
                todoList.Add(todo);
                }
            }
            UpdateBubblesPanel(todoList);
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // שינוי הנראות של הפילטרים בהתאם לבחירה בתיבה המשולבת
            todosDtateFilter.Visible = (stateComboBox.Text == "For Date:");
            assigneeComboBox.Visible = (stateComboBox.Text == "For User:");

            switch (stateComboBox.Text)
            {
                case "All":
                    await LoadAndDisplayTodos(() => todoService.GetAllTodos());
                    break;
                case "For User:":
                    assigneeComboBox.Visible = true;
                    LoadAssignees();
                    break;
                case "For Me":
                    assigneeComboBox.SelectedValue = session.getUserID();
                    assigneeComboBox.Visible = true;
                    break;
                case "Overdue":
                    await LoadAndDisplayTodos(() => todoService.GetOverdueTodos());
                    break;
                case "For Today":
                    string dateAsJson = DateTime.Now.ToString("yyyy-MM-dd");
                    await LoadAndDisplayTodos(() => todoService.GetTodosForDate(dateAsJson));
                    break;
                case "Completed":
                    await LoadAndDisplayCompletedTodos(() => todoService.GetAllTodos());
                    break;
            }
        }

        private async void todosDtateFilter_ValueChanged(object sender, EventArgs e)
        {
            stateComboBox.SelectedIndex=3;

            todosDtateFilter.Visible = true;
            string dateJson = todosDtateFilter.Value.ToString("yyyy-MM-dd");
            HttpResponseMessage data = await todoService.GetTodosForDate(dateJson);

            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);
            UpdateBubblesPanel(todoList);
        }

        private async void assigneeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (session.isUserAdmin())  {stateComboBox.SelectedValue = "For User:";}
            HttpResponseMessage data = await todoService.GetTodoByAssignee(assigneeComboBox.SelectedValue.ToString());

            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);
            UpdateBubblesPanel(todoList);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UserUpdateForm userUpdateForm = new UserUpdateForm(userService);

                UserUpdateForm userEditPanel = this.userUpdateForm;
                userEditPanel.Visible = true;

                userEditPanel.Controls.Clear(); 
                userEditPanel.Controls.Add(userUpdateForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void userUpdateForm_VisibleChanged(object sender, EventArgs e)
        {
            bool isVisible = userUpdateForm.Visible;
            MessageBox.Show("userUpdateForm is visible? " + isVisible);
            updateUserInfoButton.Enabled = !isVisible;
            updateUserInfoButton.Visible = !isVisible;
        }

        internal void UpdateButtonVisibility(bool isVisible)
        {
            //Console.WriteLine("Button visibility updated to "+isVisible);
            updateUserInfoButton.Enabled = isVisible;
            updateUserInfoButton.Visible = isVisible;
        }

        private void DrawArrow(Graphics g, Control source, Control target)
        {
            if (source == null || target == null) return;

            Point sourcePoint = new Point(source.Right, source.Top + source.Height / 2);
            Point targetPoint = new Point(target.Left, target.Top + target.Height / 2);

            using (Pen pen = new Pen(Color.Black, 2))
            {
                pen.CustomEndCap = new AdjustableArrowCap(5, 5);
                g.DrawLine(pen, sourcePoint, targetPoint);
            }
        }

        private void DrawArrowToRightControl()
        {
            // Clear previous drawings
            bubblesPanel.Invalidate();

            // Add paint event handler if not already added
            bubblesPanel.Paint += (sender, e) =>
            {
                foreach (Control source in bubblesPanel.Controls)
                {
                    // Find the closest control to the right of the current control
                    Control target = FindRightControl(source);

                    if (target != null)
                    {
                        // Draw an arrow from source to target
                        DrawArrow(e.Graphics, source, target);
                    }
                }
            };

            // Trigger a repaint to show the arrows
            bubblesPanel.Refresh();
        }

        private Control FindRightControl(Control source)
        {
            Control rightControl = null;
            int minimumDistance = source.Width;

            foreach (Control target in bubblesPanel.Controls)
            {
                if (target != source)
                {
                    // Check if target is to the right of the source
                    if (target.Left > source.Right)
                    {
                        int distance = target.Left - source.Right;
                        if (distance < minimumDistance)
                        {
                            minimumDistance = distance;
                            rightControl = target;
                        }
                    }
                }
            }

            return rightControl;
        }


        private void bubblesPanel_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("you've clicked on "+mouseLoacation.ToString());
            ToDoItem newToDo = new ToDoItem(assignee:UserSession.id,id:0,parentTaskId:0,title:"new todo",description:"",startDate:DateTime.Now,dueDate:DateTime.Now,endDate:DateTime.Now,level:0,status:TodoStatus.TODO, tags:"");
            BubbleControl newBubble = newToDo.ToBubble(mouseLoacation);
            //SingleToDoEdit detailForm = new SingleToDoEdit(newBubble.Item, this.ParentForm as ToDoListForm);  detailForm.Show();
        }

        private void bubblesPanel_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLoacation = e.Location;
        }

        private void bubblesPanel_Paint(object sender, PaintEventArgs e)
        {
            /*
            IEnumerable<Connection> connections = connectionsManager.GetConnections();
            foreach (Connection connection in connections)
            {
                connectionsManager.ConnectBubbles(connection.Source, connection.Target);
            }*/
        }

        /*
        private void bubblesPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            BubbleControl current_bubble = (BubbleControl)e.Control;
            Console.WriteLine("location.X = "+current_bubble.Location.X);
            Console.Beep();
            //GetChildAtPoint(current_bubble.Location.X + current_bubble.Width, current_bubble.Location.Y);
        }*/
    }
}