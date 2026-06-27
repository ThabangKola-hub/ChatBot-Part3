namespace ChatBot
{
    // Represents one cybersecurity task
    public class TaskItem
    {
        // Task title
        public string Title { get; set; }

        // Description
        public string Description { get; set; }

        // Reminder date
        public DateTime ReminderDate { get; set; }

        // Completed?
        public bool Completed { get; set; }

        // Constructor
        public TaskItem(string title, string description, DateTime reminderDate)
        {
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
            Completed = false;
        }

        // Display inside ListBox
        public override string ToString()
        {
            return $"{Title} ({ReminderDate.ToShortDateString()})";
        }
    }
}