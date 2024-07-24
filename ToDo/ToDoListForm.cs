using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

//ToDo: add data source and data binding items to the project instead of loading everything manually, it might help with the search as well
//ToDo: add code to detect if a todo_item's windoiw is already open and move it forward
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
        private void ToDoListWindow_Load(object sender, EventArgs e)
        {
            this.userUpdateForm.Visible = false;
            /*HttpResponseMessage data = await todoService.GetAllTodos();
            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);

            UpdateBubblesPanel(todoList);
            */

            updateDates(DateTime.Now.Date);
            assigneeComboBox.Visible = false;

            
            
            if (session.isUserAdmin())
            {
                Console.WriteLine("user is admin"); //for debug

                stateComboBox.Items.Add("For User:");
                stateComboBox.Items.Add("All");
                
                stateComboBox.Text = "All";
            }
            else
            {
                stateComboBox.Text = "For Me";
            }

            UpdateBubblesPanel();

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

            foreach (var todo in todoList)
            {
                //Console.Clear();
                Console.WriteLine("ToDoListForm | UpdateBubblesPanel | Parent task is "+todo.ParentTaskId.ToString());
            }

            // Layout the bubbles
            //foreach (ToDoItem todo_item in todoList) { Console.WriteLine("task " + todo_item.Id + "'s parent task is " + todo_item.ParentTaskId); };
            LayoutBubbles(tasks: todoList, bubblesPanel: bubblesPanel);
        }



        // Function to layout the bubbles
        private void LayoutBubbles(List<ToDoItem> tasks, Panel bubblesPanel)
        {
            // Define the necessary spacing and padding
            int spaceBetweenUnrelatedTasks = 60;
            int bubblePadding = 10;

            // Clear existing controls
            bubblesPanel.Controls.Clear();

            // Group tasks by ParentTaskId
            var groupedTasks = tasks.GroupBy(t => t.ParentTaskId);

            // Dictionary to hold the BubbleControls and their positions
            Dictionary<BubbleControl, Point> bubblePositions = new Dictionary<BubbleControl, Point>();

            // Function to set positions based on the Level property
            void SetBubblePosition(ToDoItem task, int x, int y)
            {
                var bubble = task.ToBubble();

                // Calculate the offset based on the todo_item's Level
                int offsetX = (bubble.Width + bubblePadding) * task.Level;
                int posY = y;

                if (!bubblePositions.ContainsKey(bubble))
                {
                    bubblePositions[bubble] = new Point(x + offsetX, posY);
                }

                int childX = x + offsetX + bubble.Width + bubblePadding;
                int childY = posY;

                if (groupedTasks.Any(g => g.Key == task.Id))
                {
                    var childTasks = groupedTasks.First(g => g.Key == task.Id).ToList();
                    for (int i = 0; i < childTasks.Count; i++)
                    {
                        Console.WriteLine("positioning child "+i+" of task "+task.Id); 
                        SetBubblePosition(childTasks[i], childX, childY);
                        childY += (childTasks.Count > 1 ? bubble.Height + spaceBetweenUnrelatedTasks : 0);
                    }
                }

            }

            // Layout the top-level tasks
            int initialX = bubblePadding;
            int initialY = bubblePadding;

            if (groupedTasks.Any(g => g.Key == 0))
            {
                var topLevelTasks = groupedTasks.First(g => g.Key == 0).ToList();
                foreach (ToDoItem todo_item in topLevelTasks)
                {
                    var myChildTasks = tasks.FindAll(t => t.ParentTaskId == todo_item.Id);
                    
                    /*
                    Console.WriteLine(todo_item.ToString());
                    Console.WriteLine("task "+todo_item.Id+" has "+myChildTasks.Count()+" child tasks and it's parent task is "+todo_item.ParentTaskId+" and it's on level "+todo_item.Level);
                    */
                    SetBubblePosition(todo_item, initialX, initialY);
                    initialY += todo_item.ToBubble().Height + spaceBetweenUnrelatedTasks;
                }
            }

            // Add the BubbleControls to the panel
            foreach (var kvp in bubblePositions)
            {
                var bubble = kvp.Key;
                var position = kvp.Value;

                // Check for overlaps and shift if necessary
                bool hasOverlap;
                do
                {
                    hasOverlap = false;
                    foreach (var otherBubble in bubblePositions.Keys)
                    {
                        if (bubble != otherBubble)
                        {
                            var bubbleRect = new Rectangle(position, bubble.Size);
                            var otherBubbleRect = new Rectangle(bubblePositions[otherBubble], otherBubble.Size);
                            if (bubbleRect.IntersectsWith(otherBubbleRect))
                            {
                                position.Y += bubble.Height + bubblePadding;
                                hasOverlap = true;
                                break;
                            }
                        }
                    }
                } while (hasOverlap);

                bubble.Location = position;
                bubblesPanel.Controls.Add(bubble);
            }
        }

        // Function to print the current state of bubblePositions
        private void PrintBubblePositions(Dictionary<BubbleControl, Point> bubblePositions)
        {
            Console.WriteLine("Bubble Positions:");
            foreach (var kvp in bubblePositions)
            {
                Console.WriteLine($"{kvp.Key.Text}: {kvp.Value}");
            }
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
                }*/
                

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
                //Console.WriteLine();  Console.WriteLine("#################### | ToDoListForm | UpdateBubblesPanel | ####################"); Console.WriteLine();
                Point newPoint = new Point(10, 10);
                //BubbleControl bubble = new BubbleControl(todo, newPoint);
                BubbleControl bubble = todo.ToBubble(newPoint);
                bubble.TabIndex = (int)todo.Id;
                taskDict[todo.Id] = bubble;
            }

            // Arrange tasks with parent-child relationships
            /*
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
            }*/

            connectionsManager = new ConnectionsManager(bubblesPanel);

            // Layout the bubbles
            //LayoutBubbles(bubblesPanel.Controls);
            LayoutBubbles(tasks: todoList, bubblesPanel: bubblesPanel);
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

        private long getSelectedUser()
        {

            long selectedUser;
            if (!UserSession.GetInstance().isUserAdmin())
            {
                selectedUser = UserSession.id;
            }
            else
            {
                //ToDo: make sure usere selection combo box is visible and active
                selectedUser = long.Parse(((ToDoListForm)this.ParentForm).assigneeComboBox.SelectedValue.ToString());
            }
            return selectedUser;
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
            //Am i doing this twice?
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

        private async void stateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // שינוי הנראות של הפילטרים בהתאם לבחירה בתיבה המשולבת
            todosDtateFilter.Visible = (stateComboBox.Text == "For Date:");

            switch (stateComboBox.Text)
            {
                case "All":
                    await LoadAndDisplayTodos(() => todoService.GetAllTodos());
                    break;
                case "For User:": //Will show only if admin
                    {
                    assigneeComboBox.Visible = true;
                        LoadAssignees();
                    }
                    break;
                case "For Me":
                    long my_id = session.getUserID();
                    await LoadAndDisplayTodos(() => todoService.GetTodoByAssignee(my_id.ToString()));
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
            if (session.isUserAdmin())  {
                stateComboBox.SelectedValue = "For User:"; 
            };
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

        private void updateInfoButton_Click(object sender, EventArgs e)
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
            ToDoItem newToDo = new ToDoItem(assignee:UserSession.id,id:0,parentTaskId:42,title:"new todo",description:"",startDate:DateTime.Now,dueDate:DateTime.Now,endDate:DateTime.Now,level:0,status:TodoStatus.TODO, tags:"");
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
    }
}
