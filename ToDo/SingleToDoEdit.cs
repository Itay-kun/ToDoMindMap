using MindOrgenizerToDo.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace MindOrgenizerToDo.ToDo
{
    public partial class SingleToDoEdit : Form
    {
        private ToDoItem item;
        ToDoListForm parentForm;

        TodoService todoService = new TodoService("http://localhost:5000"); //TODO: make it a singleton in user session?
        UserService userService = new UserService("http://localhost:5000"); //TODO: make it a singleton in user session?
        string todoID;

        public SingleToDoEdit()
        {
            InitializeComponent();
        }

        public SingleToDoEdit(ToDoItem item, ToDoListForm parentForm) { 
            InitializeComponent();

            //TODO: add the parent task in parentTaskComboBox, but the combobox should be able to be set to null
            //ToDo: maybe pre-create a window for each task and just show it?
            
            this.parentForm = parentForm;
            
            this.item = item;
            this.Text = "Editing: " + item.Title;
            this.titleTextBox.Text = item.Title;
            //this.assigneeEmailTextbox.Text = item.Assignee;
            this.descriptionTextbox.Text = item.Description;
            this.startDatePicker.Value = item.StartDate;
            this.startDatePicker.Value = item.DueDate;

            //ToDo: add the parent task in parentTaskComboBox
            string todoID = this.item.Id.ToString();
            
            LoadAssignees();
            this.assigneeComboBox.SelectedValue = item.Assignee;
        }

        private async Task LoadTasksIntoComboBox(Func<Task<HttpResponseMessage>> getTodosTask)
        {
            //ToDo: hide the parent task selector if no options for parrent task
            HttpResponseMessage data = await getTodosTask();

            string json = await data.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            List<ToDoItem> todoList = JsonSerializer.Deserialize<List<ToDoItem>>(json, options);


            // TODO: any way to avoide loop in any case? (for example parent of a parent, etc.)
            // Filter out the current task and parent task to avoid loops, and only let the parent task be one created befor current task
            // only let hyrarchy be crated for the same user
            //How do i let user keep the combo box empty?
            todoList = todoList.Where(task => (task.Id < item.Id)&&(task.Assignee==item.Assignee)&&(task.ParentTaskId != item.Id)).ToList();
            
            Console.WriteLine("ToDoList: " + todoList.Count);
            
            this.parentTaskComboBox.DisplayMember = "Title"; // Display task title
            this.parentTaskComboBox.ValueMember = "Id"; // Use task ID as value
            this.parentTaskComboBox.DataSource = todoList;

            this.parentTaskComboBox.SelectedValue = 0;
        }


        private async void SingleToDoEdit_Load(object sender, EventArgs e)
        {
            await LoadTasksIntoComboBox(() => todoService.GetAllTodos());

            // Set the selected value to the parent task ID of the current task
            if (item.ParentTaskId != 0)
            {
                Console.WriteLine("setting parent task id: " + item.ParentTaskId);
                this.parentTaskComboBox.SelectedValue = item.ParentTaskId;
            }
            this.statusComboBox.SelectedIndex = (int)item.Status; // Set the selected value to the status
        }

        private async void updateButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
            //MessageBox.Show("updateButton_Click: " + todoID);
            try
            {
                Console.WriteLine("SingleToDoEdit | updateButton_Click | setting level manually" );

                long parent_task_id = 0;
                if (this.parentTaskComboBox.SelectedValue != null)
                {
                    parent_task_id = long.Parse(this.parentTaskComboBox.SelectedValue.ToString());
                }

                ToDoItem updatedToDo = new ToDoItem(
                    id: item.Id,
                    parentTaskId: parent_task_id,
                    title: this.titleTextBox.Text,
                    description: this.descriptionTextbox.Text,
                    startDate: this.startDatePicker.Value,
                    dueDate: this.dueByPicker.Value,
                    endDate: this.dueByPicker.Value,
                    assignee: int.Parse(this.assigneeComboBox.SelectedValue.ToString()), //Add an if to check if the assignee pannel is enabled, if not set to item.Assignee
                    status: (TodoStatus)this.statusComboBox.SelectedIndex,
                    level: item.Level//how do i calculate the level here? should be calculated at creation level instead of set manually
                    );

                //MessageBox.Show("try: updateButton_Click: " + updatedToDo.ToString());
                this.item.Title = this.titleTextBox.Text;
                //item.Assignee = this.assigneeEmailTextbox.Text;
                this.item.Description = this.descriptionTextbox.Text;
                this.item.StartDate = this.startDatePicker.Value;
                this.item.DueDate = this.dueByPicker.Value;
                this.item.Status = (TodoStatus)this.statusComboBox.SelectedIndex;
                this.item.ParentTaskId = parent_task_id;

                //MessageBox.Show("parentTaskID:" + updatedToDo.ParentTaskId.ToString());  // Debugging purpose, remove after testing
                var response = await todoService.UpdateTodo(this.item.Id.ToString(), updatedToDo.ToJson());
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Todo updated successfully!");
                    // Optionally refresh the UI here, if necessary
                    this.parentForm.updateUI();
                    
                    Console.WriteLine(sender);
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Todo: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Todo: " + ex.Message);
            }
         
            
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await todoService.DeleteTodo(this.item.Id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Todo deleted successfully!");
                    // Optionally refresh the UI here or close the form if necessary
                     this.parentForm.updateUI();
                     this.Close();  // Close the form if no longer needed
                }
                else
                {
                    MessageBox.Show("Failed to delete Todo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Todo: " + ex.Message);
            }
        }

        private async void LoadAssignees()
        {
            Console.WriteLine("loading assignees: ");
            try
            {
                HttpResponseMessage data = await userService.GetPossibleAssigns();

                string json = await data.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                //ToDo: check deserialization for if the user is an admin or not everywhere ther is a deserialization
                var assignees = JsonSerializer.Deserialize<List<UserModel>>(json, options);
                UserSession.GetInstance().SetAssignees(assignees);
                foreach (var assignee in assignees)
                {
                    Console.WriteLine("assignee: " + assignee.ToString());
                }

                // todo: disable or hide the AssigneeSelectionPanel if there is only one assignee
                assigneeComboBox.DisplayMember = "Name";
                assigneeComboBox.ValueMember = "Id";
                assigneeComboBox.DataSource = assignees;  // Set DataSource last
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load assignees: " + ex.Message);
            }
        }

        private void parrentTaskComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.parentTaskComboBox.SelectedValue is long selectedParentTaskId)
                {
                    if (this.item.ParentTaskId != selectedParentTaskId)
                    {
                        this.item.ParentTaskId = selectedParentTaskId;
                        MessageBox.Show($"Parent task updated to {selectedParentTaskId}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating parent task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dueByPicker_ValueChanged(object sender, EventArgs e)
        {
            if (this.dueByPicker.Value < this.startDatePicker.Value)
            {
                this.dueByPicker.Value = this.startDatePicker.Value.AddDays(1);
                MessageBox.Show("Due date must be after start date.");
            }
        }
    }
}
