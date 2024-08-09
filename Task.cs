// Task.cs


namespace TaskTrackerCLI
{
    public enum TaskStatus
    {
        InProgress,
        NotStarted,
        Done
    }
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
        public TaskStatus Status { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Title} - {(IsCompleted ? "Completed" : "Pending")}";
        }
    }
}