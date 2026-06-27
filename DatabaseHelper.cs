using MySql.Data.MySqlClient;

namespace ChatBot
{
    // Handles communication between the chatbot and the MySQL database
    public class DatabaseHelper
    {
        // Connection string used to connect to MySQL
        private readonly string connectionString =
     "server=127.0.0.1;port=3306;database=cybersecuritybotdb;uid=root;pwd=thabangkola01@S;";
        // Constructor
        public DatabaseHelper()
        {
        }

        // Opens a new MySQL connection
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Saves a new task into the database
        public void AddTask(TaskItem task)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();

                MessageBox.Show("Connected to: " + connection.Database);

                string sql = @"INSERT INTO tasks
                       (Title, Description, ReminderDate, Completed)
                       VALUES
                       (@title,@description,@date,@completed)";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@title", task.Title);
                    cmd.Parameters.AddWithValue("@description", task.Description);
                    cmd.Parameters.AddWithValue("@date", task.ReminderDate);
                    cmd.Parameters.AddWithValue("@completed", task.Completed);

                    int rows = cmd.ExecuteNonQuery();
                    MySqlCommand verify = new MySqlCommand(
    "SELECT Id, Title, Description FROM tasks ORDER BY Id DESC LIMIT 1",
    connection);

                    using (MySqlDataReader reader = verify.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show(
                                "Newest Task\n\n" +
                                "ID: " + reader["Id"] + "\n" +
                                "Title: " + reader["Title"] + "\n" +
                                "Description: " + reader["Description"]);
                        }
                    }

                    MessageBox.Show("ExecuteNonQuery returned: " + rows);
                }
            }
        }
    }
}