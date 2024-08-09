// Program.cs

namespace TaskTrackerCLI
{
   static class Program
    {
        static void Main(string[] args)
        {
            var taskManager = new TaskManager();

            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            switch (args[0].ToLower())
            {
                case "add":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Please provide a title for the task.");
                        return;
                    }
                    taskManager.AddTask(string.Join(" ", args.Skip(1)));
                    break;
                case "list":
                    taskManager.ListTasks();
                    break;
                case "complete":
                    if (args.Length < 2 || !int.TryParse(args[1], out int id))
                    {
                        Console.WriteLine("Please provide a valid task ID.");
                        return;
                    }
                    taskManager.MarkTaskAsCompleted(id);
                    break;
                case "delete":
                    if (args.Length < 2 || !int.TryParse(args[1], out int deleteId))
                    {
                        Console.WriteLine("Please provide a valid task ID.");
                        return;
                    }
                    taskManager.DeleteTask(deleteId);
                    break;
                default:
                    ShowHelp();
                    break;
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  add <title>      - Add a new task with the specified title.");
            Console.WriteLine("  list             - List all tasks.");
            Console.WriteLine("  complete <id>    - Mark the task with the specified ID as completed.");
            Console.WriteLine("  delete <id>      - Delete the task with the specified ID.");
        }
    }
}
