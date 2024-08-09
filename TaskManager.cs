// TaskManager.cs


namespace TaskTrackerCLI
{
    public class TaskManager
    {
        private readonly List<Task> _tasks = new List<Task>();
        private int _nextId = 1;

        public void AddTask(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Error: Task title cannot be empty.");
                return;
            }

            var task = new Task { Id = _nextId++, Title = title, IsCompleted = false };
            _tasks.Add(task);
            Console.WriteLine($"Task added with ID {task.Id}.");
        }

        public void ListTasks()
        {
            if (_tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in _tasks)
            {
                Console.WriteLine(task);
            }
        }

        public void MarkTaskAsCompleted(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }

            if (task.IsCompleted)
            {
                Console.WriteLine("Task is already marked as completed.");
                return;
            }

            task.IsCompleted = true;
            Console.WriteLine($"Task with ID {id} marked as completed.");
        }

        public void DeleteTask(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }

            _tasks.Remove(task);
            Console.WriteLine($"Task with ID {id} deleted.");
        }
    }
}