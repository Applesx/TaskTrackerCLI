// TaskManager.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskTrackerCLI
{
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        private int nextId = 1;

        public void AddTask(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Error: Task title cannot be empty");
                return;
            }
            
            var task = new Task { Id = nextId++, Title = title, IsCompleted = false };
            tasks.Add(task);
            Console.WriteLine($"Task added with Id {task.Id}.");
        }

        public void ListTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        public void MarkTaskAsCompleted(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            task.IsCompleted = true;
            Console.WriteLine("Task marked as completed.");
        }

        public void DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            tasks.Remove(task);
            Console.WriteLine("Task deleted.");
        }
    }
}