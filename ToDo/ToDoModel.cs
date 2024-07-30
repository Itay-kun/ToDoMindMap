using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Windows.Forms;


namespace MindOrgenizerToDo
{
    //ToDo: add data source and data binding items to the project instead of loading everything manually, it might help with the search as well
    public enum TodoStatus
    {
        TODO,
        IN_PROGRESS,
        COMPLETED,
        DELETED //Do we actually need this?
    }

    public class ToDoItem
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        //[JsonPropertyName("parent_task_id")]
        [JsonPropertyName("parentTaskId")]
        public long ParentTaskId { get; set; }

        [JsonPropertyName("assignee")]
        public int Assignee { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        public TodoStatus Status { get; set; }

        [JsonPropertyName("tags")]
        public string Tags { get; set; }

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("dueDate")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        public static List<ToDoItem> ToDoList { get; set; } = new List<ToDoItem>();

        public BubbleControl bubble { get; set; }

        public ToDoItem(long id, long parentTaskId, string title, string description, DateTime startDate, DateTime dueDate, DateTime endDate, int assignee, int level = 0, TodoStatus status = TodoStatus.TODO, String tags = "")
        {
            Id = id;
            Title = title;
            Assignee = assignee;
            Description = description;
            Tags = tags;
            ParentTaskId = parentTaskId;
            StartDate = startDate;
            DueDate = dueDate;
            EndDate = endDate;
            Status = status; //Initialize the task as not compleated
            Level = level;

            Console.WriteLine("Created: "+this.ToString());
        }

        public override string ToString()
        {
            Console.WriteLine("");
            return $"Assignee: {Assignee}, Key: {Id}, Status: {Status}, Title: {Title}, Description: {Description}, " +
                   $"Level: {Level}, ParentTaskID: {ParentTaskId}, StartDate: {StartDate}, EndDate: {EndDate}";
        }

        public BubbleControl ToBubble()
        {
            if (this.bubble != null)
            {
                MessageBox.Show("Bubble already created at location: "+this.bubble.Location);
                return this.bubble;
            }
            else
            {
                return new BubbleControl(this, new Point(0, 0));
            }
        }

        public BubbleControl ToBubble(Point location)
        {
            if (this.bubble != null)
            {
                MessageBox.Show("Bubble already created at location: " + this.bubble.Location);
                this.bubble.Location = location;
                return this.bubble;
            }
            else
            {
                return new BubbleControl(this, location);
            }
        }

        public JsonObject ToJson()
        {
            JsonObject taskAsJson = new JsonObject
            {
                ["key"] = Id,
                ["title"] = Title,
                ["assignee"] = Assignee,
                ["description"] = Description,
                ["tags"] = Tags,
                ["parentTaskId"] = ParentTaskId,
                ["startDate"] = StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ["dueDate"] = DueDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ["endDate"] = EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ["status"] = ((TodoStatus)Status).ToString(),
                ["level"] = Level
            };

            return taskAsJson;
        }

        public bool hasParentTask()
        {
            bool hasParentTask = (this.ParentTaskId != 0);
            Console.WriteLine(this.Id + " has parent task: " + hasParentTask);
            return (hasParentTask);
        }
    }
}