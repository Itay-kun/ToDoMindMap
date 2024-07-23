using System.Drawing;
using System.Windows.Forms;

namespace MindOrgenizerToDo
{
    partial class ToDoListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToDoListForm));
            this.label1 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.descriptionTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dueByPicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.datesPannel = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.assigneeComboBox = new System.Windows.Forms.ComboBox();
            this.todosDtateFilter = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.updateUserInfoButton = new System.Windows.Forms.Button();
            this.bubblesPanel = new System.Windows.Forms.Panel();
            this.userUpdateForm = new MindOrgenizerToDo.User.UserUpdateForm();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.datesPannel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1156, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "MindToDO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(2, 25);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(322, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(303, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Title:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descriptionTextbox
            // 
            this.descriptionTextbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.descriptionTextbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.descriptionTextbox.HideSelection = false;
            this.descriptionTextbox.Location = new System.Drawing.Point(3, 19);
            this.descriptionTextbox.Multiline = true;
            this.descriptionTextbox.Name = "descriptionTextbox";
            this.descriptionTextbox.Size = new System.Drawing.Size(319, 56);
            this.descriptionTextbox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // newButton
            // 
            this.newButton.AutoEllipsis = true;
            this.newButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.newButton.Location = new System.Drawing.Point(184, 0);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(140, 39);
            this.newButton.TabIndex = 3;
            this.newButton.Text = "Clear";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.saveButton.Location = new System.Drawing.Point(0, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(137, 39);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "yyyy-MM-dd";
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.Location = new System.Drawing.Point(75, 0);
            this.startDatePicker.MinDate = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.ShowUpDown = true;
            this.startDatePicker.Size = new System.Drawing.Size(84, 20);
            this.startDatePicker.TabIndex = 8;
            this.startDatePicker.Value = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Start Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dueByPicker
            // 
            this.dueByPicker.CustomFormat = "yyyy-MM-dd";
            this.dueByPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dueByPicker.Location = new System.Drawing.Point(74, 0);
            this.dueByPicker.Name = "dueByPicker";
            this.dueByPicker.ShowUpDown = true;
            this.dueByPicker.Size = new System.Drawing.Size(84, 20);
            this.dueByPicker.TabIndex = 10;
            this.dueByPicker.Value = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Due By:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.titleTextBox);
            this.panel1.Location = new System.Drawing.Point(39, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 45);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.descriptionTextbox);
            this.panel2.Location = new System.Drawing.Point(41, 202);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 80);
            this.panel2.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.startDatePicker);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(310, 25);
            this.panel3.TabIndex = 11;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dueByPicker);
            this.panel4.Location = new System.Drawing.Point(3, 34);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(310, 24);
            this.panel4.TabIndex = 11;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.saveButton);
            this.panel5.Controls.Add(this.newButton);
            this.panel5.Location = new System.Drawing.Point(39, 425);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(324, 39);
            this.panel5.TabIndex = 12;
            // 
            // statusComboBox
            // 
            this.statusComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.statusComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.statusComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            "TODO",
            "IN PROGRESS",
            "COMPLETED"});
            this.statusComboBox.Location = new System.Drawing.Point(60, 7);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(121, 21);
            this.statusComboBox.TabIndex = 13;
            this.statusComboBox.Tag = "todo_status";
            this.statusComboBox.Text = "TODO";
            // 
            // datesPannel
            // 
            this.datesPannel.Controls.Add(this.panel3);
            this.datesPannel.Controls.Add(this.panel4);
            this.datesPannel.Location = new System.Drawing.Point(41, 333);
            this.datesPannel.Name = "datesPannel";
            this.datesPannel.Size = new System.Drawing.Size(325, 67);
            this.datesPannel.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.statusComboBox);
            this.panel6.Location = new System.Drawing.Point(44, 289);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(322, 38);
            this.panel6.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Location = new System.Drawing.Point(5, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Status:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.assigneeComboBox);
            this.panel8.Controls.Add(this.todosDtateFilter);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.stateComboBox);
            this.panel8.Location = new System.Drawing.Point(39, 94);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(385, 38);
            this.panel8.TabIndex = 16;
            // 
            // assigneeComboBox
            // 
            this.assigneeComboBox.DisplayMember = "Name";
            this.assigneeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.assigneeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.assigneeComboBox.FormattingEnabled = true;
            this.assigneeComboBox.Location = new System.Drawing.Point(258, 9);
            this.assigneeComboBox.Name = "assigneeComboBox";
            this.assigneeComboBox.Size = new System.Drawing.Size(112, 21);
            this.assigneeComboBox.TabIndex = 15;
            this.assigneeComboBox.ValueMember = "Id";
            this.assigneeComboBox.SelectedIndexChanged += new System.EventHandler(this.assigneeComboBox_SelectedIndexChanged);
            // 
            // todosDtateFilter
            // 
            this.todosDtateFilter.CustomFormat = "yyyy-MM-dd";
            this.todosDtateFilter.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todosDtateFilter.Location = new System.Drawing.Point(273, 9);
            this.todosDtateFilter.MinDate = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            this.todosDtateFilter.Name = "todosDtateFilter";
            this.todosDtateFilter.ShowUpDown = true;
            this.todosDtateFilter.Size = new System.Drawing.Size(84, 20);
            this.todosDtateFilter.TabIndex = 14;
            this.todosDtateFilter.Value = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            this.todosDtateFilter.Visible = false;
            this.todosDtateFilter.ValueChanged += new System.EventHandler(this.todosDtateFilter_ValueChanged);
            // 
            // label8
            // 
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label8.Location = new System.Drawing.Point(5, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "I Should See the tasks ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stateComboBox
            // 
            this.stateComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.stateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Items.AddRange(new object[] {
            "Overdue",
            "Completed",
            "For Today",
            "For Date:",
            "For Me"});
            this.stateComboBox.Location = new System.Drawing.Point(123, 9);
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size(121, 21);
            this.stateComboBox.TabIndex = 13;
            this.stateComboBox.Tag = "todo_status";
            this.stateComboBox.SelectedIndexChanged += new System.EventHandler(this.stateComboBox_SelectedIndexChanged);
            // 
            // updateUserInfoButton
            // 
            this.updateUserInfoButton.Location = new System.Drawing.Point(139, 481);
            this.updateUserInfoButton.Name = "updateUserInfoButton";
            this.updateUserInfoButton.Size = new System.Drawing.Size(115, 22);
            this.updateUserInfoButton.TabIndex = 17;
            this.updateUserInfoButton.Text = "Update Login Info";
            this.updateUserInfoButton.UseVisualStyleBackColor = false;
            this.updateUserInfoButton.Click += new System.EventHandler(this.updateInfoButton_Click);
            // 
            // bubblesPanel
            // 
            this.bubblesPanel.AllowDrop = true;
            this.bubblesPanel.AutoScroll = true;
            this.bubblesPanel.BackColor = System.Drawing.Color.Transparent;
            this.bubblesPanel.BackgroundImage = global::MindOrgenizerToDo.Properties.Resources.background;
            this.bubblesPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bubblesPanel.Location = new System.Drawing.Point(502, 74);
            this.bubblesPanel.Name = "bubblesPanel";
            this.bubblesPanel.Size = new System.Drawing.Size(607, 475);
            this.bubblesPanel.TabIndex = 16;
            this.bubblesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.bubblesPanel_Paint);
            this.bubblesPanel.DoubleClick += new System.EventHandler(this.bubblesPanel_DoubleClick);
            this.bubblesPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bubblesPanel_MouseMove);
            // 
            // userUpdateForm
            // 
            this.userUpdateForm.AutoSize = true;
            this.userUpdateForm.Location = new System.Drawing.Point(104, 481);
            this.userUpdateForm.Name = "userUpdateForm";
            this.userUpdateForm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userUpdateForm.Size = new System.Drawing.Size(194, 127);
            this.userUpdateForm.TabIndex = 0;
            this.userUpdateForm.Visible = false;
            this.userUpdateForm.VisibleChanged += new System.EventHandler(this.userUpdateForm_VisibleChanged);
            // 
            // ToDoListForm
            // 
            this.AccessibleName = "Task Creation Screen";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1180, 804);
            this.Controls.Add(this.updateUserInfoButton);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.bubblesPanel);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.datesPannel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userUpdateForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ToDoListForm";
            this.Text = "MOTD | To Do List";
            this.Load += new System.EventHandler(this.ToDoListWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.datesPannel.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descriptionTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Panel datesPannel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label6;
        private Panel bubblesPanel;
        private DateTimePicker dueByPicker;
        private Panel panel8;
        private Label label8;
        private ComboBox stateComboBox;
        private DateTimePicker todosDtateFilter;
        public ComboBox assigneeComboBox;
        private Button updateUserInfoButton;
        private User.UserUpdateForm userUpdateForm;
    }
}

