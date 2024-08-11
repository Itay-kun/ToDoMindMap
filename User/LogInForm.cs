using MindOrgenizerToDo.Services;
using System;
using System.Drawing;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace MindOrgenizerToDo
{
    public partial class LogInForm : Form
    {
        readonly private UserService _userService;

        public LogInForm()
        {
            InitializeComponent();
            emailTextBox.Focus();

            _userService = new UserService("http://localhost:5000");

        }

        private JsonObject GetFieldsAsJson()
        {
            JsonObject user_info = new JsonObject
            {
                ["email"] = emailTextBox.Text,
                ["password"] = passworddTextBox.Text,
                ["nickname"] = textBoxUserName.Text,
            };
            Console.WriteLine(user_info);
            return user_info;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            //todo: check somehow if the server is running!
            try
            {
                Console.WriteLine("Logging in user...");
                var response = await _userService.LoginUser(emailTextBox.Text, passworddTextBox.Text);

                //TODO: Add error handling for when server is not running, maybe ping the server on app start?
                if (response.IsSuccessStatusCode)
                {
                    this.LoginButton.Enabled = false;
                    this.WindowState = FormWindowState.Minimized;
                    UserSession session = UserSession.GetInstance(emailTextBox.Text);
                    
                    string responseString = await response.Content.ReadAsStringAsync();
                    bool isAdmin = UserService.ExtractIsAdmin(responseString);

                    session.SetIsAdmin(isAdmin);


                    string jwtToken = UserService.ExtractToken(responseString);
                    session.SetToken(jwtToken);
                    
                    int userID = UserService.ExtractUserID(responseString);
                    session.SetUserID(userID);
                    
                    session.SetClient(UserSession.GetClient()); // Ensure the client is set correctly
                    ToDoListForm todo_list_edit_form = new ToDoListForm(session);
                    todo_list_edit_form.ShowDialog();
                    
                    Console.WriteLine("\n\n\n");
                    this.Close();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Login failed: {errorContent}");
                    MessageBox.Show($"Login failed: {errorContent}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show($"Error logging in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Registering user...");
            try
            {
                var response = await _userService.RegisterUser(emailTextBox.Text, passworddTextBox.Text, textBoxUserName.Text);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Registration successful!", "Response from Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Registration failed: {errorContent}");
                    MessageBox.Show($"Registration failed: {errorContent}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show($"Error registering user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            userInfoPanel.BackColor = Color.FromArgb(150, 10, 10, 10);
        }

        private void TextBoxUserName_TextChanged(object sender, EventArgs e)
        {
            bool canRegister = (textBoxUserName.Text.Trim().Length > 0);
            RegisterButton.Enabled = canRegister;
        }

        private void TextBoxUserName_Enter(object sender, EventArgs e)
        {
            bool canRegister = (textBoxUserName.Text.Trim().Length > 0);
            RegisterButton.Enabled = canRegister;
        }

        private void TextBoxUserName_Leave(object sender, EventArgs e)
        {
            bool canRegister = (textBoxUserName.Text.Trim().Length > 0);
            RegisterButton.Enabled = canRegister;
        }
    }
}