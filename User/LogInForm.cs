using MindOrgenizerToDo.Services;
using System;
using System.Drawing;
using System.Text.Json.Nodes;
using System.Windows.Forms;

namespace MindOrgenizerToDo
{
    public partial class LogInForm : Form
    {
        private UserService _userService;
        private AdminService _adminService;

        public LogInForm()
        {
            InitializeComponent();
            _userService = new UserService("http://localhost:5000");
            _adminService = new AdminService("http://localhost:5000");

            // Prepopulate email and password
            this.emailTextBox.Text = "itay.work.study@gmail.com";
            this.passworddTextBox.Text = "1234";
        }

        private JsonObject getFieldsAsJson()
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

        private async void loginButton_Click(object sender, EventArgs e)
        {
            //todo: check somehow if the server is running!
            try
            {
                Console.WriteLine("Logging in user...");
                var response = await _userService.LoginUser(emailTextBox.Text, passworddTextBox.Text);

                //TODO: Add error handling for when server is not running, maybe ping the server on app start?
                if (response.IsSuccessStatusCode)
                {
                    this.lgoinButton.Enabled = false;
                    this.WindowState = FormWindowState.Minimized;
                    UserSession session = UserSession.GetInstance(emailTextBox.Text);
                    
                    string responseString = await response.Content.ReadAsStringAsync();
                    bool isAdmin = UserService.ExtractIsAdmin(responseString);

                    if (isAdmin)
                    {
                        //MessageBox.Show("Admin login successful!", "Response from Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

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

        private async void registerButton_Click(object sender, EventArgs e)
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
    }
}