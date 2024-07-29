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
            this.tasksFiltersPanel = new System.Windows.Forms.Panel();
            this.assigneeComboBox = new System.Windows.Forms.ComboBox();
            this.todosDtateFilter = new System.Windows.Forms.DateTimePicker();
            this.titleTasksToSee = new System.Windows.Forms.Label();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.updateUserInfoButton = new System.Windows.Forms.Button();
            this.bubblesPanel = new System.Windows.Forms.Panel();
            this.userUpdateForm = new MindOrgenizerToDo.User.UserUpdateForm();
            this.bubblePanelBackground = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.datesPannel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tasksFiltersPanel.SuspendLayout();
            this.bubblePanelBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // titleTextBox
            // 
            resources.ApplyResources(this.titleTextBox, "titleTextBox");
            this.titleTextBox.Name = "titleTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // descriptionTextbox
            // 
            resources.ApplyResources(this.descriptionTextbox, "descriptionTextbox");
            this.descriptionTextbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.descriptionTextbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.descriptionTextbox.HideSelection = false;
            this.descriptionTextbox.Name = "descriptionTextbox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // newButton
            // 
            resources.ApplyResources(this.newButton, "newButton");
            this.newButton.AutoEllipsis = true;
            this.newButton.Name = "newButton";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // startDatePicker
            // 
            resources.ApplyResources(this.startDatePicker, "startDatePicker");
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDatePicker.MinDate = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.ShowUpDown = true;
            this.startDatePicker.Value = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // dueByPicker
            // 
            resources.ApplyResources(this.dueByPicker, "dueByPicker");
            this.dueByPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dueByPicker.Name = "dueByPicker";
            this.dueByPicker.ShowUpDown = true;
            this.dueByPicker.Value = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.titleTextBox);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.descriptionTextbox);
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.startDatePicker);
            this.panel3.Name = "panel3";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.dueByPicker);
            this.panel4.Name = "panel4";
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Controls.Add(this.saveButton);
            this.panel5.Controls.Add(this.newButton);
            this.panel5.Name = "panel5";
            // 
            // statusComboBox
            // 
            resources.ApplyResources(this.statusComboBox, "statusComboBox");
            this.statusComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.statusComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            resources.GetString("statusComboBox.Items"),
            resources.GetString("statusComboBox.Items1"),
            resources.GetString("statusComboBox.Items2")});
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Tag = "todo_status";
            // 
            // datesPannel
            // 
            resources.ApplyResources(this.datesPannel, "datesPannel");
            this.datesPannel.Controls.Add(this.panel3);
            this.datesPannel.Controls.Add(this.panel4);
            this.datesPannel.Name = "datesPannel";
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.statusComboBox);
            this.panel6.Name = "panel6";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Name = "label6";
            // 
            // tasksFiltersPanel
            // 
            resources.ApplyResources(this.tasksFiltersPanel, "tasksFiltersPanel");
            this.tasksFiltersPanel.Controls.Add(this.assigneeComboBox);
            this.tasksFiltersPanel.Controls.Add(this.todosDtateFilter);
            this.tasksFiltersPanel.Controls.Add(this.titleTasksToSee);
            this.tasksFiltersPanel.Controls.Add(this.stateComboBox);
            this.tasksFiltersPanel.Name = "tasksFiltersPanel";
            // 
            // assigneeComboBox
            // 
            resources.ApplyResources(this.assigneeComboBox, "assigneeComboBox");
            this.assigneeComboBox.DisplayMember = "Name";
            this.assigneeComboBox.FormattingEnabled = true;
            this.assigneeComboBox.Name = "assigneeComboBox";
            this.assigneeComboBox.ValueMember = "Id";
            this.assigneeComboBox.SelectedIndexChanged += new System.EventHandler(this.assigneeComboBox_SelectedIndexChanged);
            // 
            // todosDtateFilter
            // 
            resources.ApplyResources(this.todosDtateFilter, "todosDtateFilter");
            this.todosDtateFilter.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.todosDtateFilter.MinDate = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            this.todosDtateFilter.Name = "todosDtateFilter";
            this.todosDtateFilter.ShowUpDown = true;
            this.todosDtateFilter.Value = new System.DateTime(2024, 5, 8, 0, 0, 0, 0);
            this.todosDtateFilter.ValueChanged += new System.EventHandler(this.todosDtateFilter_ValueChanged);
            // 
            // titleTasksToSee
            // 
            resources.ApplyResources(this.titleTasksToSee, "titleTasksToSee");
            this.titleTasksToSee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.titleTasksToSee.Name = "titleTasksToSee";
            // 
            // stateComboBox
            // 
            resources.ApplyResources(this.stateComboBox, "stateComboBox");
            this.stateComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Items.AddRange(new object[] {
            resources.GetString("stateComboBox.Items"),
            resources.GetString("stateComboBox.Items1"),
            resources.GetString("stateComboBox.Items2"),
            resources.GetString("stateComboBox.Items3"),
            resources.GetString("stateComboBox.Items4")});
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Tag = "todo_status";
            this.stateComboBox.SelectedIndexChanged += new System.EventHandler(this.stateComboBox_SelectedIndexChanged);
            // 
            // updateUserInfoButton
            // 
            resources.ApplyResources(this.updateUserInfoButton, "updateUserInfoButton");
            this.updateUserInfoButton.Name = "updateUserInfoButton";
            this.updateUserInfoButton.UseVisualStyleBackColor = false;
            this.updateUserInfoButton.Click += new System.EventHandler(this.updateInfoButton_Click);
            // 
            // bubblesPanel
            // 
            resources.ApplyResources(this.bubblesPanel, "bubblesPanel");
            this.bubblesPanel.AllowDrop = true;
            this.bubblesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.bubblesPanel.Name = "bubblesPanel";
            this.bubblesPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bubblesPanel_MouseMove);
            // 
            // userUpdateForm
            // 
            resources.ApplyResources(this.userUpdateForm, "userUpdateForm");
            this.userUpdateForm.Name = "userUpdateForm";
            this.userUpdateForm.VisibleChanged += new System.EventHandler(this.userUpdateForm_VisibleChanged);
            // 
            // bubblePanelBackground
            // 
            resources.ApplyResources(this.bubblePanelBackground, "bubblePanelBackground");
            this.bubblePanelBackground.AllowDrop = true;
            this.bubblePanelBackground.BackColor = System.Drawing.Color.Transparent;
            this.bubblePanelBackground.BackgroundImage = global::MindOrgenizerToDo.Properties.Resources.background;
            this.bubblePanelBackground.Controls.Add(this.bubblesPanel);
            this.bubblePanelBackground.Name = "bubblePanelBackground";
            // 
            // ToDoListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bubblePanelBackground);
            this.Controls.Add(this.updateUserInfoButton);
            this.Controls.Add(this.tasksFiltersPanel);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.datesPannel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userUpdateForm);
            this.Name = "ToDoListForm";
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
            this.tasksFiltersPanel.ResumeLayout(false);
            this.bubblePanelBackground.ResumeLayout(false);
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
        private Panel tasksFiltersPanel;
        private Label titleTasksToSee;
        private ComboBox stateComboBox;
        private DateTimePicker todosDtateFilter;
        public ComboBox assigneeComboBox;
        private Button updateUserInfoButton;
        private User.UserUpdateForm userUpdateForm;
        private Panel bubblePanelBackground;
    }
}

