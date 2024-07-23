namespace MindOrgenizerToDo.ToDo
{
    partial class SingleToDoEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleToDoEdit));
            this.AssigneeSelectionPanel = new System.Windows.Forms.Panel();
            this.assigneeComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.descriptionPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.descriptionTextbox = new System.Windows.Forms.TextBox();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.datesPannel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dueByPicker = new System.Windows.Forms.DateTimePicker();
            this.parentTaskPanel = new System.Windows.Forms.Panel();
            this.title_ParrentTask = new System.Windows.Forms.Label();
            this.parentTaskComboBox = new System.Windows.Forms.ComboBox();
            this.backgroundPanel = new System.Windows.Forms.Panel();
            this.AssigneeSelectionPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.descriptionPanel.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.datesPannel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.parentTaskPanel.SuspendLayout();
            this.backgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AssigneeSelectionPanel
            // 
            this.AssigneeSelectionPanel.BackColor = System.Drawing.Color.Transparent;
            this.AssigneeSelectionPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.AssigneeSelectionPanel.Controls.Add(this.assigneeComboBox);
            this.AssigneeSelectionPanel.Controls.Add(this.label7);
            this.AssigneeSelectionPanel.Location = new System.Drawing.Point(25, 14);
            this.AssigneeSelectionPanel.Name = "AssigneeSelectionPanel";
            this.AssigneeSelectionPanel.Size = new System.Drawing.Size(269, 20);
            this.AssigneeSelectionPanel.TabIndex = 18;
            // 
            // assigneeComboBox
            // 
            this.assigneeComboBox.DisplayMember = "Name";
            this.assigneeComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.assigneeComboBox.FormattingEnabled = true;
            this.assigneeComboBox.Location = new System.Drawing.Point(93, 0);
            this.assigneeComboBox.Name = "assigneeComboBox";
            this.assigneeComboBox.Size = new System.Drawing.Size(176, 21);
            this.assigneeComboBox.TabIndex = 1;
            this.assigneeComboBox.ValueMember = "Id";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.ForeColor = System.Drawing.Color.Gold;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Assignee:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.Transparent;
            this.statusPanel.Controls.Add(this.label6);
            this.statusPanel.Controls.Add(this.statusComboBox);
            this.statusPanel.Location = new System.Drawing.Point(27, 240);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(202, 22);
            this.statusPanel.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.ForeColor = System.Drawing.Color.Gold;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 22);
            this.label6.TabIndex = 2;
            this.label6.Text = "Status:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusComboBox
            // 
            this.statusComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.statusComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            "TODO",
            "IN_PROGRESS",
            "COMPLETED"});
            this.statusComboBox.Location = new System.Drawing.Point(66, 0);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(136, 21);
            this.statusComboBox.TabIndex = 4;
            this.statusComboBox.Tag = "todo_status";
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Controls.Add(this.updateButton);
            this.buttonsPanel.Controls.Add(this.deleteButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 390);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(460, 49);
            this.buttonsPanel.TabIndex = 19;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = true;
            this.cancelButton.Location = new System.Drawing.Point(344, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(84, 29);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(26, 7);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(93, 29);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(190, 7);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(74, 29);
            this.deleteButton.TabIndex = 8;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // descriptionPanel
            // 
            this.descriptionPanel.BackColor = System.Drawing.Color.Transparent;
            this.descriptionPanel.Controls.Add(this.label3);
            this.descriptionPanel.Controls.Add(this.descriptionTextbox);
            this.descriptionPanel.Location = new System.Drawing.Point(26, 100);
            this.descriptionPanel.Name = "descriptionPanel";
            this.descriptionPanel.Size = new System.Drawing.Size(387, 80);
            this.descriptionPanel.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 80);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descriptionTextbox
            // 
            this.descriptionTextbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.descriptionTextbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.descriptionTextbox.Dock = System.Windows.Forms.DockStyle.Right;
            this.descriptionTextbox.HideSelection = false;
            this.descriptionTextbox.Location = new System.Drawing.Point(92, 0);
            this.descriptionTextbox.Multiline = true;
            this.descriptionTextbox.Name = "descriptionTextbox";
            this.descriptionTextbox.Size = new System.Drawing.Size(295, 80);
            this.descriptionTextbox.TabIndex = 3;
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.Transparent;
            this.titlePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.titlePanel.Controls.Add(this.label2);
            this.titlePanel.Controls.Add(this.titleTextBox);
            this.titlePanel.Location = new System.Drawing.Point(24, 60);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(389, 22);
            this.titlePanel.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Title:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.titleTextBox.Location = new System.Drawing.Point(94, 0);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(295, 20);
            this.titleTextBox.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.startDatePicker);
            this.panel3.Location = new System.Drawing.Point(-4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 25);
            this.panel3.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Start Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "yyyy-MM-dd";
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.Location = new System.Drawing.Point(95, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.ShowUpDown = true;
            this.startDatePicker.Size = new System.Drawing.Size(84, 20);
            this.startDatePicker.TabIndex = 5;
            // 
            // datesPannel
            // 
            this.datesPannel.BackColor = System.Drawing.Color.Transparent;
            this.datesPannel.Controls.Add(this.panel3);
            this.datesPannel.Controls.Add(this.panel4);
            this.datesPannel.Location = new System.Drawing.Point(27, 286);
            this.datesPannel.Name = "datesPannel";
            this.datesPannel.Size = new System.Drawing.Size(266, 67);
            this.datesPannel.TabIndex = 20;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dueByPicker);
            this.panel4.Location = new System.Drawing.Point(-4, 34);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(181, 24);
            this.panel4.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.ForeColor = System.Drawing.Color.Gold;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "Due By:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dueByPicker
            // 
            this.dueByPicker.CustomFormat = "yyyy-MM-dd";
            this.dueByPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dueByPicker.Location = new System.Drawing.Point(94, 0);
            this.dueByPicker.Name = "dueByPicker";
            this.dueByPicker.ShowUpDown = true;
            this.dueByPicker.Size = new System.Drawing.Size(84, 20);
            this.dueByPicker.TabIndex = 6;
            this.dueByPicker.Value = new System.DateTime(2024, 7, 17, 0, 0, 0, 0);
            // 
            // parentTaskPanel
            // 
            this.parentTaskPanel.BackColor = System.Drawing.Color.Transparent;
            this.parentTaskPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.parentTaskPanel.Controls.Add(this.title_ParrentTask);
            this.parentTaskPanel.Controls.Add(this.parentTaskComboBox);
            this.parentTaskPanel.Location = new System.Drawing.Point(27, 199);
            this.parentTaskPanel.Name = "parentTaskPanel";
            this.parentTaskPanel.Size = new System.Drawing.Size(322, 21);
            this.parentTaskPanel.TabIndex = 22;
            // 
            // title_ParrentTask
            // 
            this.title_ParrentTask.BackColor = System.Drawing.Color.Transparent;
            this.title_ParrentTask.Dock = System.Windows.Forms.DockStyle.Left;
            this.title_ParrentTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title_ParrentTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.title_ParrentTask.ForeColor = System.Drawing.Color.Gold;
            this.title_ParrentTask.Location = new System.Drawing.Point(0, 0);
            this.title_ParrentTask.Name = "title_ParrentTask";
            this.title_ParrentTask.Size = new System.Drawing.Size(102, 21);
            this.title_ParrentTask.TabIndex = 2;
            this.title_ParrentTask.Text = "Parrent Task:";
            this.title_ParrentTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // parentTaskComboBox
            // 
            this.parentTaskComboBox.DisplayMember = "Title";
            this.parentTaskComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parentTaskComboBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.parentTaskComboBox.FormattingEnabled = true;
            this.parentTaskComboBox.Location = new System.Drawing.Point(0, 0);
            this.parentTaskComboBox.Name = "parentTaskComboBox";
            this.parentTaskComboBox.Size = new System.Drawing.Size(322, 21);
            this.parentTaskComboBox.Sorted = true;
            this.parentTaskComboBox.TabIndex = 3;
            this.parentTaskComboBox.Tag = "parent_task";
            this.parentTaskComboBox.ValueMember = "Title";
            this.parentTaskComboBox.SelectedIndexChanged += new System.EventHandler(this.parrentTaskComboBox_SelectedIndexChanged);
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.backgroundPanel.Controls.Add(this.buttonsPanel);
            this.backgroundPanel.Controls.Add(this.datesPannel);
            this.backgroundPanel.Controls.Add(this.statusPanel);
            this.backgroundPanel.Controls.Add(this.parentTaskPanel);
            this.backgroundPanel.Controls.Add(this.AssigneeSelectionPanel);
            this.backgroundPanel.Controls.Add(this.titlePanel);
            this.backgroundPanel.Controls.Add(this.descriptionPanel);
            this.backgroundPanel.Location = new System.Drawing.Point(24, 24);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(460, 439);
            this.backgroundPanel.TabIndex = 23;
            // 
            // SingleToDoEdit
            // 
            this.AcceptButton = this.updateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::MindOrgenizerToDo.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(510, 512);
            this.Controls.Add(this.backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleToDoEdit";
            this.Opacity = 0.98D;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Task Edit Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SingleToDoEdit_Load);
            this.AssigneeSelectionPanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            this.descriptionPanel.ResumeLayout(false);
            this.descriptionPanel.PerformLayout();
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.datesPannel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.parentTaskPanel.ResumeLayout(false);
            this.backgroundPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AssigneeSelectionPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel descriptionPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox descriptionTextbox;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox titleTextBox;
        public System.Windows.Forms.ComboBox assigneeComboBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Panel datesPannel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dueByPicker;
        private System.Windows.Forms.Panel parentTaskPanel;
        private System.Windows.Forms.Label title_ParrentTask;
        private System.Windows.Forms.ComboBox parentTaskComboBox;
        private System.Windows.Forms.Panel backgroundPanel;
        private System.Windows.Forms.Button cancelButton;
    }
}