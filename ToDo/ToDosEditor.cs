using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using MindOrgenizerToDo.Services;
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
        HttpResponseMessage data;



        public ToDoListForm(UserSession session)
        {
            Console.WriteLine("ToDoListForm: " + session.Email);
            this.session = session;
            InitializeComponent();
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
                BubbleControl bubble = new BubbleControl(todo);
                taskDict[todo.Id] = bubble;
            }

            // Arrange tasks with parent-child relationships
            foreach (var bubble in taskDict.Values)
            {
                if (bubble.Item.ParentTaskId != 0 && taskDict.ContainsKey(bubble.Item.ParentTaskId))
                {
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
            Console.WriteLine("ToDosEditor | LayoutBubbles: " + bubbles.Count);
            int yOffset = 10; // Initial top margin
            int xOffset = 10; // Initial left margin

            for (int i = 0; i < bubbles.Count; i++)
            {
                BubbleControl bubble = bubbles[i] as BubbleControl;
                BubbleControl previousBubble = (i > 0) ? bubbles[i - 1] as BubbleControl : null;

                bool isSameDepth = previousBubble != null && previousBubble.depth == bubble.depth;
                Console.WriteLine("isSameDepth: " + isSameDepth);

                if (isSameDepth)
                {
                    yOffset += (previousBubble.Height/2) + 10;
                    xOffset = 10;
                }
                else
                {
                    yOffset = 10;
                    if (previousBubble != null)
                    {
                        xOffset += previousBubble.Width + 10;
                    }
                }

                Point newLocation = new Point(xOffset, yOffset);

                // Check if another bubble is already at the new location
                bool locationOccupied = false;
                foreach (BubbleControl otherBubble in bubbles)
                {
                    if (otherBubble != bubble && otherBubble.Location == newLocation)
                    {
                        locationOccupied = true;
                        break;
                    }
                }

                if (locationOccupied)
                {
                    yOffset += (bubble.Height/2) + 10;
                    newLocation = new Point(xOffset, yOffset);
                    Console.WriteLine("location occupied at: " + newLocation);
                }

                bubble.Location = newLocation;
                Console.WriteLine("bubble location: " + bubble.Location);

                // Always update yOffset for the next bubble
                yOffset += bubble.Height + 10;
            }
        }

        private void UpdateBubblesPanel(List<ToDoItem> todoList)
        {

           // todoList = SortToDoListByHierarchy(todoList);//Sort the items by hierarchy

            Console.WriteLine("ToDosEditor | UpdateBubblesPanel");
            bubblesPanel.Controls.Clear();
            //bubblesPanel.ControlAdded += new ControlEventHandler(bubblesPanel_ControlAdded);

            // Create a dictionary to store the tasks by their ID for easy lookup
            Dictionary<long, BubbleControl> taskDict = new Dictionary<long, BubbleControl>();

            foreach (var todo in todoList)
            {
                Console.WriteLine("##############################################################"); Console.WriteLine();
                BubbleControl bubble = new BubbleControl(todo);
                bubble.TabIndex = (int)todo.Id;
                taskDict[todo.Id] = bubble;
            }

            // Arrange tasks with parent-child relationships
            foreach (var bubble in taskDict.Values)
            {
                if (bubble.Item.ParentTaskId != 0 && taskDict.ContainsKey(bubble.Item.ParentTaskId))
                {
                    taskDict[bubble.Item.ParentTaskId].AddChild(bubble);
                    Console.WriteLine("adding child to " + bubble.Item.ParentTaskId);
                }
                else
                {
                    Console.WriteLine("ToDosEditor | adding bubble to panel | "+ bubble.Item.Title);
                    bubblesPanel.Controls.Add(bubble);
                }
            }

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
            Console.WriteLine("ToDosEditor | saveButton_Click | setting level manually?");
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
            Console.WriteLine("loading assignees: ");
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
                    Console.WriteLine("assignee: " + assignee.ToString());
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
            Console.WriteLine("Button visibility updated to "+isVisible);
            updateUserInfoButton.Enabled = isVisible;
            updateUserInfoButton.Visible = isVisible;
        }
    }
}
