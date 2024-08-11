namespace MindOrgenizerToDo
{
    partial class LogInForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.LoginButton = new System.Windows.Forms.Button();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.passworddTextBox = new System.Windows.Forms.MaskedTextBox();
            this.emailBoxLabel = new System.Windows.Forms.Label();
            this.usernameBoxLabel = new System.Windows.Forms.Label();
            this.passwordBoxLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.userInfoPanel = new System.Windows.Forms.Panel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.userInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoginButton.Location = new System.Drawing.Point(205, 275);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(93, 37);
            this.LoginButton.TabIndex = 5;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(3, 23);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(208, 20);
            this.emailTextBox.TabIndex = 1;
            this.emailTextBox.Tag = "email";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(3, 23);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(205, 20);
            this.textBoxUserName.TabIndex = 2;
            this.textBoxUserName.Tag = "username";
            this.textBoxUserName.TextChanged += new System.EventHandler(this.TextBoxUserName_TextChanged);
            this.textBoxUserName.Enter += new System.EventHandler(this.TextBoxUserName_Enter);
            this.textBoxUserName.Leave += new System.EventHandler(this.TextBoxUserName_Leave);
            // 
            // passworddTextBox
            // 
            this.passworddTextBox.AccessibleName = "password_field";
            this.passworddTextBox.HidePromptOnLeave = true;
            this.passworddTextBox.Location = new System.Drawing.Point(0, 18);
            this.passworddTextBox.Name = "passworddTextBox";
            this.passworddTextBox.PasswordChar = '*';
            this.passworddTextBox.Size = new System.Drawing.Size(208, 20);
            this.passworddTextBox.TabIndex = 3;
            // 
            // emailBoxLabel
            // 
            this.emailBoxLabel.AutoSize = true;
            this.emailBoxLabel.BackColor = System.Drawing.Color.Transparent;
            this.emailBoxLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.emailBoxLabel.Enabled = false;
            this.emailBoxLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.emailBoxLabel.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.emailBoxLabel.Name = "emailBoxLabel";
            this.emailBoxLabel.Size = new System.Drawing.Size(50, 19);
            this.emailBoxLabel.TabIndex = 0;
            this.emailBoxLabel.Text = "Email";
            // 
            // usernameBoxLabel
            // 
            this.usernameBoxLabel.AutoSize = true;
            this.usernameBoxLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameBoxLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.usernameBoxLabel.Enabled = false;
            this.usernameBoxLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.usernameBoxLabel.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.usernameBoxLabel.Name = "usernameBoxLabel";
            this.usernameBoxLabel.Size = new System.Drawing.Size(86, 19);
            this.usernameBoxLabel.TabIndex = 0;
            this.usernameBoxLabel.Text = "Username";
            // 
            // passwordBoxLabel
            // 
            this.passwordBoxLabel.AutoSize = true;
            this.passwordBoxLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordBoxLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.passwordBoxLabel.Enabled = false;
            this.passwordBoxLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.passwordBoxLabel.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.passwordBoxLabel.Name = "passwordBoxLabel";
            this.passwordBoxLabel.Size = new System.Drawing.Size(81, 19);
            this.passwordBoxLabel.TabIndex = 0;
            this.passwordBoxLabel.Text = "Password";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.textBoxUserName);
            this.panel1.Controls.Add(this.usernameBoxLabel);
            this.panel1.Location = new System.Drawing.Point(88, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 59);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.emailTextBox);
            this.panel2.Controls.Add(this.emailBoxLabel);
            this.panel2.Location = new System.Drawing.Point(88, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 53);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.passworddTextBox);
            this.panel3.Controls.Add(this.passwordBoxLabel);
            this.panel3.Location = new System.Drawing.Point(88, 206);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(211, 46);
            this.panel3.TabIndex = 8;
            // 
            // userInfoPanel
            // 
            this.userInfoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.userInfoPanel.Controls.Add(this.TitleLabel);
            this.userInfoPanel.Controls.Add(this.panel2);
            this.userInfoPanel.Controls.Add(this.panel3);
            this.userInfoPanel.Controls.Add(this.RegisterButton);
            this.userInfoPanel.Controls.Add(this.panel1);
            this.userInfoPanel.Controls.Add(this.LoginButton);
            this.userInfoPanel.Location = new System.Drawing.Point(85, 71);
            this.userInfoPanel.Name = "userInfoPanel";
            this.userInfoPanel.Size = new System.Drawing.Size(396, 371);
            this.userInfoPanel.TabIndex = 9;
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.CausesValidation = false;
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLabel.Enabled = false;
            this.TitleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(396, 66);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Tag = "title";
            this.TitleLabel.Text = "Log In / Register";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RegisterButton
            // 
            this.RegisterButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.RegisterButton.Enabled = false;
            this.RegisterButton.Location = new System.Drawing.Point(87, 275);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(90, 37);
            this.RegisterButton.TabIndex = 4;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = false;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // LogInForm
            // 
            this.AcceptButton = this.LoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MindOrgenizerToDo.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(570, 571);
            this.Controls.Add(this.userInfoPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogInForm";
            this.Text = "MOTD | Login";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.userInfoPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.MaskedTextBox passworddTextBox;
        private System.Windows.Forms.Label emailBoxLabel;
        private System.Windows.Forms.Label usernameBoxLabel;
        private System.Windows.Forms.Label passwordBoxLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel userInfoPanel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button RegisterButton;
    }
}