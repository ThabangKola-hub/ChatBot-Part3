namespace ChatBot
{
    public static class ActivityLogger
    {
        private static List<string> activities = new List<string>();

        public static void Log(string activity)
        {
            activities.Add(
                $"{DateTime.Now:G} - {activity}"
            );
        }

        public static List<string> GetActivities()
        {
            return activities;
        }
    }
}