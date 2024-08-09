// TaskManager.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TaskTrackerCLI
{
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        private int nextId = 1;
        private const string FilePath = "tasks.json";

        public TaskManager()
        {
            LoadTasks();
        }

        public void AddTask(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Error: Task title cannot be empty.");
                return;
            }

            var task = new Task { Id = nextId++, Title = title, Status = TaskStatus.NotStarted };
            tasks.Add(task);
            SaveTasks();
            Console.WriteLine($"Task added with ID {task.Id}.");
        }

        public void UpdateTask(int id, string newTitle, TaskStatus newStatus)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(newTitle))
                task.Title = newTitle;

            task.Status = newStatus;
            SaveTasks();
            Console.WriteLine($"Task with ID {id} updated.");
        }

        public void DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }

            tasks.Remove(task);
            SaveTasks();
            Console.WriteLine($"Task with ID {id} deleted.");
        }

        public void ListAllTasks()
        {
            ListTasks(); // Call without any filter to list all tasks
        }

        public void ListDoneTasks()
        {
            ListTasks(TaskStatus.Done);
        }

        public void ListNotDoneTasks()
        {
            var notDoneStatuses = new[] { TaskStatus.NotStarted, TaskStatus.InProgress };
            var filteredTasks = tasks.Where(t => notDoneStatuses.Contains(t.Status));
            PrintTasks(filteredTasks);
        }

        public void ListInProgressTasks()
        {
            ListTasks(TaskStatus.InProgress);
        }

        public void ListTasks(TaskStatus? status = null)
        {
            var filteredTasks = status.HasValue ? tasks.Where(t => t.Status == status.Value) : tasks;
            PrintTasks(filteredTasks);
        }

        private void PrintTasks(IEnumerable<Task> tasksToPrint)
        {
            if (!tasksToPrint.Any())
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in tasksToPrint)
            {
                Console.WriteLine(task);
            }
        }

        private void SaveTasks()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(tasks, Formatting.Indented));
        }

        private void LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                tasks = JsonConvert.DeserializeObject<List<Task>>(File.ReadAllText(FilePath)) ?? new List<Task>();
                nextId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            }
            else
            {
                nextId = 1;
            }
        }
    }
}
