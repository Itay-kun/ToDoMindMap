using MindOrgenizerToDo;
using System.Collections.Generic;
using System;

public class TaskHierarchyBuilder
{
    public static Dictionary<long, List<ToDoItem>> BuildTaskHierarchy(List<ToDoItem> tasks)
    {
        Console.WriteLine("TaskHierarchyBuilder | BuildTaskHierarchy");
        var taskDictionary = new Dictionary<long, List<ToDoItem>>();

        // Sort tasks into a dictionary based on ParentTaskId
        foreach (var task in tasks)
        {
            if (!taskDictionary.ContainsKey(task.ParentTaskId))
            {
                taskDictionary[task.ParentTaskId] = new List<ToDoItem>();
            }
            if (task.hasParentTask() || task.ParentTaskId == 0) // Use method to check if task has a parent
            {
                taskDictionary[task.ParentTaskId].Add(task);
            }
        }

        return taskDictionary;
    }

    public static void PrintTaskHierarchy(Dictionary<long, List<ToDoItem>> taskDictionary, long parentId = 0, string indent = "")
    {
        Console.WriteLine("TaskHierarchyBuilder | PrintTaskHierarchy");
        if (taskDictionary.ContainsKey(parentId))
        {
            foreach (var task in taskDictionary[parentId])
            {
                Console.WriteLine($"{indent}- {task.Title} (ID: {task.Id})");
                PrintTaskHierarchy(taskDictionary, task.Id, indent + "  ");
            }
        }
    }
}
