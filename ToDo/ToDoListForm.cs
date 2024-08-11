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
using MindOrgenizerToDo.User;

//ToDo: add data source and data binding items to the project instead of loading everything manually, it might help for creating the search as well
//ToDo: add code to detect if a todo_item's windoiw is already open and move it forward instead of making a new one

namespace MindOrgenizerToDo
{
    public partial class ToDoListForm : Form
    {
        TodoService todoService = UserSession.GetInstance().GetTodoService();
        UserService userService = UserSession.GetInstance().GetUserService();
        UserSession session;
        HttpResponseMessage data;
        public ConnectionsManager connectionsManager;
        


        public ToDoListForm(UserSession session)
        {
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
            this.assigneeComboBox.Visible = false;

            updateDates(DateTime.Now.Date);

            
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
            TodoService todoService = UserSession.GetInstance().GetTodoService();

            switch (stateComboBox.Text)
            {
                case "All":
                    //await LoadAndDisplayTodos(() => todoService.GetAllTodos());
                    
                    break;
                case "For User:": //Will show only if admin
                    {
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
                Console.WriteLine("ToDoListForm | UpdateBubblesPanel | Parent task is "+todo.ParentTaskId.ToString());
            }

            // Layout the bubbles
            LayoutBubbles(tasks: todoList, bubblesPanel: bubblesPanel);
        }



        // Function to layout the bubbles
        private void LayoutBubbles(List<ToDoItem> tasks, Panel bubblesPanel)
        {
            // Define the necessary spacing and padding
            int spaceBetweenUnrelatedTasks = 30;
            int bubblePadding = 10;

            // Clear existing controls
            bubblesPanel.Controls.Clear();
            connectionsManager = new ConnectionsManager(bubblesPanel);

            // Group tasks by ParentTaskId
            var groupedTasks = tasks.GroupBy(t => t.ParentTaskId);

            // Dictionary to hold the BubbleControls and their positions
            Dictionary<BubbleControl, Point> bubblePositions = new Dictionary<BubbleControl, Point>();

            // Function to set positions based on the Level property
            void SetBubblePosition(ToDoItem task, int x, int y)
            {
                var bubble = task.ToBubble();

                // Calculate the offset based on the todo_item's Level
                int offsetX = (bubble.Width/2 * task.Level) + bubblePadding;
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

                        
                        //if (!childTasks[i].bubble.Visible)    { connectionsManager.RemoveConnection(task.ToBubble(), childTasks[i].ToBubble()); }

                        //visually connect the children
                        connectionsManager.AddConnection(task.ToBubble(), childTasks[i].ToBubble());


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
                BubbleControl bubble = kvp.Key;
                Point position = kvp.Value;

                // Check for overlaps and shift if necessary
                bool hasOverlap;
                do
                {
                    hasOverlap = false;
                    foreach (BubbleControl otherBubble in bubblePositions.Keys)
                    {
                        if (bubble != otherBubble)
                        {
                            Rectangle bubbleRect = new Rectangle(position, bubble.Size);
                            Rectangle otherBubbleRect = new Rectangle(bubblePositions[otherBubble], otherBubble.Size);
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
                bubble.Show();
            }
            connectionsManager = new ConnectionsManager(bubblesPanel);
        }

        private void UpdateBubblesPanel(List<ToDoItem> todoList)
        {
            foreach (BubbleControl todo in bubblesPanel.Controls)
            {
                todo.Hide();
            }
            Console.WriteLine(); Console.WriteLine("#################### | ToDoListForm | UpdateBubblesPanel | ####################"); Console.WriteLine();
            // Create a dictionary to store the tasks by their ID for easy lookup
            Dictionary<long, BubbleControl> taskDict = new Dictionary<long, BubbleControl>();

            foreach (var todo in todoList)
            {
                Console.WriteLine("adding task: " + todo.ToJson());
                Point newPoint = new Point(10, 10);
                BubbleControl bubble = todo.ToBubble(newPoint);
                bubble.TabIndex = (int)todo.Id;
                taskDict[todo.Id] = bubble;
            }

            if (connectionsManager == null) { connectionsManager = new ConnectionsManager(bubblesPanel); }

            // Layout the bubbles
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
                //ToDoListForm might be current form
                if (this.assigneeComboBox.Visible)
                {
                    selectedUser = long.Parse(((ToDoListForm)this.ParentForm).assigneeComboBox.SelectedValue.ToString());
                }
                else
                selectedUser = long.Parse(((ToDoListForm)this.ParentForm).assigneeComboBox.SelectedValue.ToString());
            }
            return selectedUser;
        }

        public async void LoadAssignees()
        {
            //Console.WriteLine("loading assignees_list: ");
            try
            {
                data = await userService.GetPossibleAssigns();
                
                string json = await data.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var assignees = JsonSerializer.Deserialize<List<UserModel>>(json,options);
                session.SetAssignees(assignees);

                foreach (var assignee in assignees)
                {
                    Console.WriteLine("assignee: " + assignee.Name + "added");
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

            UpdateBubblesPanel(todoList);
        }

        private async Task LoadAndDisplayCompletedTodos(Func<Task<HttpResponseMessage>> getTodosTask)
        {
            Console.Clear();
            HttpResponseMessage data = await getTodosTask();

            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            List<ToDoItem> temp_list = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);

            //var assignee = UserSession.id;
            //if(UserSession.GetInstance().IsAdmin) { assignee = (int)getSelectedUser(); };

            List<ToDoItem> todoList = temp_list.FindAll(x => x.Status == TodoStatus.COMPLETED);
            if (!UserSession.GetInstance().isUserAdmin()) { todoList = todoList.FindAll(x => x.Assignee == UserSession.id); };

            foreach (ToDoItem todo in todoList)
            {
               Console.WriteLine("should add todo: " + todo.ToJson()+" to list "+todoList.Count.ToString());
            }
            UpdateBubblesPanel(todoList);
        }

        private async void stateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // שינוי הנראות של הפילטרים בהתאם לבחירה בתיבה המשולבת
            todosDtateFilter.Visible = (stateComboBox.Text == "For Date:");
            assigneeComboBox.Visible = (stateComboBox.Text == "For User:");

            switch (stateComboBox.Text)
            {
                case "All":
                    await LoadAndDisplayTodos(() => todoService.GetAllTodos());
                    break;
                case "For User:": //Will show only if admin
                    {
                    //assigneeComboBox.Visible = true;
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
            //bubblesPanel.Invalidate();
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
            updateUserInfoButton.Enabled = !isVisible;
            updateUserInfoButton.Visible = !isVisible;
        }

        internal void UpdateButtonVisibility(bool isVisible)
        {
            //Console.WriteLine("Button visibility updated to "+isVisible);
            updateUserInfoButton.Enabled = isVisible;
            updateUserInfoButton.Visible = isVisible;
        }

        private void bubblesPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {   
                //ToDo: Move the arrows by or remove them based on the new scroll location?
            }
        }
    }
}