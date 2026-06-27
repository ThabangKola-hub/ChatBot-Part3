namespace ChatBot
{
    // Keeps a history of actions performed
    public static class ActivityLogger
    {
        // List that stores activities
        private static List<string> activities = new List<string>();

        // Add activity
        public static void Log(string message)
        {
            activities.Add($"{DateTime.Now:G} - {message}");
        }

        // Return all activities
        public static List<string> GetActivities()
        {
            return activities;
        }
    }
}