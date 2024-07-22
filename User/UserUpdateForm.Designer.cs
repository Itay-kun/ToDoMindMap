namespace MindOrgenizerToDo.User
{
    partial class UserUpdateForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.emailBoxLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.passwordTextBox = new System.Windows.Forms.MaskedTextBox();
            this.passwordBoxLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.usernameBoxLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.deletUserButton = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(188, 186);
            this.panel4.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.emailTextBox);
            this.panel2.Controls.Add(this.emailBoxLabel);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(183, 38);
            this.panel2.TabIndex = 7;
            // 
            // emailTextBox
            // 
            this.emailTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.emailTextBox.Location = new System.Drawing.Point(0, 18);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(183, 20);
            this.emailTextBox.TabIndex = 0;
            this.emailTextBox.Tag = "email";
            // 
            // emailBoxLabel
            // 
            this.emailBoxLabel.AutoSize = true;
            this.emailBoxLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.emailBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.emailBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.emailBoxLabel.Name = "emailBoxLabel";
            this.emailBoxLabel.Size = new System.Drawing.Size(44, 15);
            this.emailBoxLabel.TabIndex = 5;
            this.emailBoxLabel.Text = "Email";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.passwordTextBox);
            this.panel3.Controls.Add(this.passwordBoxLabel);
            this.panel3.Location = new System.Drawing.Point(3, 127);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(183, 43);
            this.panel3.TabIndex = 8;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.AccessibleName = "password_field";
            this.passwordTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.passwordTextBox.HidePromptOnLeave = true;
            this.passwordTextBox.Location = new System.Drawing.Point(0, 23);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(183, 20);
            this.passwordTextBox.TabIndex = 3;
            // 
            // passwordBoxLabel
            // 
            this.passwordBoxLabel.AutoSize = true;
            this.passwordBoxLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.passwordBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.passwordBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.passwordBoxLabel.Name = "passwordBoxLabel";
            this.passwordBoxLabel.Size = new System.Drawing.Size(69, 15);
            this.passwordBoxLabel.TabIndex = 5;
            this.passwordBoxLabel.Text = "Password";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.usernameTextBox);
            this.panel1.Controls.Add(this.usernameBoxLabel);
            this.panel1.Location = new System.Drawing.Point(3, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 40);
            this.panel1.TabIndex = 6;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.usernameTextBox.Location = new System.Drawing.Point(0, 20);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(183, 20);
            this.usernameTextBox.TabIndex = 2;
            this.usernameTextBox.Tag = "username";
            // 
            // usernameBoxLabel
            // 
            this.usernameBoxLabel.AutoSize = true;
            this.usernameBoxLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.usernameBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.usernameBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.usernameBoxLabel.Name = "usernameBoxLabel";
            this.usernameBoxLabel.Size = new System.Drawing.Size(73, 15);
            this.usernameBoxLabel.TabIndex = 5;
            this.usernameBoxLabel.Text = "Username";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(113, 195);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(76, 37);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(6, 195);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(76, 37);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // deletUserButton
            // 
            this.deletUserButton.BackColor = System.Drawing.Color.RosyBrown;
            this.deletUserButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.deletUserButton.Location = new System.Drawing.Point(0, 254);
            this.deletUserButton.Name = "deletUserButton";
            this.deletUserButton.Size = new System.Drawing.Size(198, 27);
            this.deletUserButton.TabIndex = 11;
            this.deletUserButton.Text = "Delete User";
            this.deletUserButton.UseVisualStyleBackColor = false;
            this.deletUserButton.Click += new System.EventHandler(this.deletUserButton_Click);
            // 
            // UserUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deletUserButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.panel4);
            this.Name = "UserUpdateForm";
            this.Size = new System.Drawing.Size(198, 281);
            this.Load += new System.EventHandler(this.UserUpdateForm_Load);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label emailBoxLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MaskedTextBox passwordTextBox;
        private System.Windows.Forms.Label passwordBoxLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label usernameBoxLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button deletUserButton;
    }
}
