// Program.cs

namespace TaskTrackerCLI
{
    class Program
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
                        Console.WriteLine("Error: Please provide a title for the task.");
                        return;
                    }
                    taskManager.AddTask(string.Join(" ", args.Skip(1)));
                    break;

                case "update":
                    if (args.Length < 3 || !int.TryParse(args[1], out int updateId) || !Enum.TryParse(args[2], true, out TaskStatus newStatus))
                    {
                        Console.WriteLine("Error: Please provide a valid task ID and status.");
                        return;
                    }
                    var newTitle = args.Length > 3 ? string.Join(" ", args.Skip(3)) : null;
                    taskManager.UpdateTask(updateId, newTitle, newStatus);
                    break;

                case "delete":
                    if (args.Length < 2 || !int.TryParse(args[1], out int deleteId))
                    {
                        Console.WriteLine("Error: Please provide a valid task ID.");
                        return;
                    }
                    taskManager.DeleteTask(deleteId);
                    break;

                case "list":
                    if (args.Length == 1)
                    {
                        taskManager.ListAllTasks();
                    }
                    else if (args.Length == 2)
                    {
                        if (Enum.TryParse(args[1], true, out TaskStatus status))
                        {
                            taskManager.ListTasks(status);
                        }
                        else if (args[1].Equals("done", StringComparison.OrdinalIgnoreCase))
                        {
                            taskManager.ListDoneTasks();
                        }
                        else if (args[1].Equals("notdone", StringComparison.OrdinalIgnoreCase))
                        {
                            taskManager.ListNotDoneTasks();
                        }
                        else if (args[1].Equals("inprogress", StringComparison.OrdinalIgnoreCase))
                        {
                            taskManager.ListInProgressTasks();
                        }
                        else
                        {
                            Console.WriteLine($"Error: Unknown list filter '{args[1]}'.");
                            ShowHelp();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid arguments for 'list' command.");
                        ShowHelp();
                    }
                    break;

                default:
                    Console.WriteLine($"Error: Unknown command '{args[0]}'.");
                    ShowHelp();
                    break;
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  add <title>                - Add a new task with the specified title.");
            Console.WriteLine("  update <id> <title> <status> - Update a task with the specified ID. Status can be NotStarted, InProgress, or Done.");
            Console.WriteLine("  delete <id>                - Delete the task with the specified ID.");
            Console.WriteLine("  list [status]              - List all tasks. Optionally, filter by status: NotStarted, InProgress, Done, notdone, or inprogress.");
        }
    }
}
