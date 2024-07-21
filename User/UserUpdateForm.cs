using MindOrgenizerToDo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindOrgenizerToDo.User
{
    public partial class UserUpdateForm : UserControl
    {
        private UserService _userService;
        private AdminService _adminService = new AdminService("http://localhost:5000");

        public UserUpdateForm()
        {
        }

        public event EventHandler VisibleChangedCustom;

        protected virtual void OnVisibleChangedCustom()
        {
            VisibleChangedCustom?.Invoke(this, EventArgs.Empty);
        }

        public new bool Visible
        {
            get { return base.Visible; }
            set
            {
                base.Visible = value;
                if (this.ParentForm is ToDoListForm)
                {
                    Console.WriteLine("Update visibility, form visibility is "+value);
                    ((ToDoListForm)this.ParentForm).UpdateButtonVisibility(!value);
                }
            }
        }

        private long selectUser()
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

        //How do i pass the active user?
        public UserUpdateForm(UserService userService)
        {
            this._userService = userService;
            Console.WriteLine("UserUpdateForm constructor called");
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Console.WriteLine("Edit Canceled");
            this.ParentForm.Update();
        }

        async private void saveButton_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response;

            if (UserSession.GetInstance().isUserAdmin())
            {
                long selectedUserID = selectUser();
                Console.WriteLine("userID from save button: " + UserSession.id.ToString());
                response = await _adminService.UpdateUserInfo(selectedUserID,emailTextBox.Text, usernameTextBox.Text, passwordTextBox.Text);
            } else
            {
                response = await _userService.UpdateUserInfo(emailTextBox.Text, usernameTextBox.Text, passwordTextBox.Text);
            }

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("User updated successfully!", "Response from Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;
                ((ToDoListForm)this.ParentForm).LoadAssignees();
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void deletUserButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("delete user button clicked");
            long selectedUserID = selectUser();
            MessageBox.Show(selectedUserID.ToString());
            var response = await _adminService.DeleteUser(selectedUserID);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("User deleted successfully!", "Response from Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;
                ((ToDoListForm)this.ParentForm).LoadAssignees();
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserUpdateForm_Load(object sender, EventArgs e)
        {
            deletUserButton.Visible = UserSession.GetInstance().isUserAdmin(); //Hide it from non-admins
            //ToDo: pre-pull the data of selected user
        }
    }
}
