namespace ChatBot { 
    using System.Drawing;
    using System.Media;

    public partial class Form1 : Form
    {

        //Voice greeting
        private void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Voice 0228.wav");
                player.Load();
                player.Play();
            }
            //Voice exception handling
            catch (Exception ex)
            {
                MessageBox.Show("Audio could not be played: " + ex.Message);
            }
        }


        private CyberBot bot = new CyberBot();

        //Collection to hold taskItem Objects or task storage
        private List<TaskItem> tasks = new List<TaskItem>();
        // Quiz data
        private string currentAnswer = "";
        // Track score
        private int quizScore = 0;

        public Form1()
        {
            InitializeComponent();

            //Calling greeting
            PlayGreeting();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            string userMessage = txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            // Display user message
            AppendUserMessage(userMessage);

            // Get bot response
            string response = bot.GetResponse(userMessage);
            // Handle commands returned by the chatbot
            if (response == "COMMAND_SHOW_TASKS")
            {
                btnShowTasks.PerformClick();
            }
            else if (response == "COMMAND_ACTIVITY_LOG")
            {
                btnViewLog.PerformClick();
            }
            else if (response == "COMMAND_START_QUIZ")
            {
                btnStartQuiz.PerformClick();
            }
            else
            {
                // Display bot response
                AppendBotMessage(response);
            }
            // Clear input box
            txtUserInput.Clear();

        }

        //Appending text messages to the left and right of the chat
        private void AppendUserMessage(string message)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            richTextBox1.AppendText(message + Environment.NewLine + Environment.NewLine);
        }

        private void AppendBotMessage(string message)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox1.AppendText("BOT: " + message + Environment.NewLine + Environment.NewLine);
        }


        //Exit Button
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        //Clear Button
        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {

            // Checking if the user entered a task
            if (string.IsNullOrWhiteSpace(txtTask.Text))
            {
                MessageBox.Show("Please enter a task.");
                return;
            }

            // Creating a new Task Item 
            TaskItem task = new TaskItem
            {
                Title = txtTask.Text,

                // Default description
                Description = "Cybersecurity Task",

                // Date chosen by user
                ReminderDate = dtpReminder.Value,

                IsCompleted = false
            };


            //Storing task in List<TaskItem>
            tasks.Add(task);

            //Displaying task in ListBox
            lstTasks.Items.Add($"{task.Title} (Due: {task.ReminderDate.ToShortDateString()})"
);

            // Record activity in the Activity Logger
            ActivityLogger.Log("Task Added: " + task.Title);

            // Clear textbox for next task
            txtTask.Clear();

            //Confirmation that task was saved
            MessageBox.Show("Task added successfully!");



        }

        private void btnCompleteTask_Click(object sender, EventArgs e)
        {

            // Make sure a task is selected
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task.");
                return;
            }

            // Find the selected task
            TaskItem selectedTask = tasks[lstTasks.SelectedIndex];

            // Mark task as completed
            selectedTask.IsCompleted = true;

            // Update ListBox display
            lstTasks.Items[lstTasks.SelectedIndex] =
                "[Completed]" + selectedTask.Title;

            // Log activity
            ActivityLogger.Log(
                "Task Completed: " + selectedTask.Title
            );

            MessageBox.Show("Task marked as completed.");



        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {

            // Make sure user selected a task
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task.");
                return;
            }

            // Save title before deleting
            string deletedTask =
                tasks[lstTasks.SelectedIndex].Title;

            // Remove task from List
            tasks.RemoveAt(lstTasks.SelectedIndex);

            // Remove task from ListBox
            lstTasks.Items.RemoveAt(lstTasks.SelectedIndex);

            // Log activity
            ActivityLogger.Log(
                "Task Deleted: " + deletedTask
            );

            MessageBox.Show("Task deleted.");




        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {

            // Retrieve all activities
            List<string> logs = ActivityLogger.GetActivities();

            // If no activities exist
            if (logs.Count == 0)
            {
                MessageBox.Show("No activities recorded.");
                return;
            }

            // Build activity report
            string report = string.Join(
                Environment.NewLine,
                logs
            );

            // Display report
            MessageBox.Show(
                report,
                "Activity Log"
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string userAnswer =
        txtQuizAnswer.Text.Trim().ToUpper();

            if (userAnswer == currentAnswer)
            {
                quizScore++;

                MessageBox.Show(
                    "Correct! Score: " + quizScore
                );

                ActivityLogger.Log(
                    "Quiz Question Answered Correctly"
                );
            }
            else
            {
                MessageBox.Show(
                    "Incorrect. The correct answer was "
                    + currentAnswer
                );

                ActivityLogger.Log(
                    "Quiz Question Answered Incorrectly"
                );
            }

            txtQuizAnswer.Clear();

        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            // Start the quiz by displaying the first question
            lblQuestion.Text =
                "What should you do if you receive a suspicious email?\n\n" +
                "A) Click the link immediately\n" +
                "B) Verify the sender first\n" +
                "C) Forward it to everyone";

            currentAnswer = "B";

            ActivityLogger.Log("Quiz Started");
        }

        private void btnShowTasks_Click(object sender, EventArgs e)
        {
            if (tasks.Count == 0)
            {
                AppendBotMessage("You currently have no tasks.");
                return;
            }

            AppendBotMessage("Here are your current tasks:");

            foreach (TaskItem task in tasks)
            {
                AppendBotMessage(
                    $"- {task.Title} | Due: {task.ReminderDate.ToShortDateString()} | Completed: {task.Completed}"
                );
            }
        }
    }
}
