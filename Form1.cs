namespace ChatBot { 
    using MySql.Data.MySqlClient;
    using System.Drawing;
    using System.Media;
    using System.Threading.Tasks;



   
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

        // Handles database operations
        private DatabaseHelper database = new DatabaseHelper();
     

        //Collection to hold taskItem Objects or task storage
        private List<TaskItem> tasks = new List<TaskItem>();
       

        // =============================
        // Quiz Variables
        // =============================
        private int quizQuestion = 0;
        private int quizScore = 0;
        private bool quizRunning = false;
        private string currentAnswer = "";

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

            // =============================
            // If the quiz is running,
            // every message becomes an answer
            // =============================
            if (quizRunning)
            {
                CheckQuizAnswer(userMessage);

                txtUserInput.Clear();
                return;
            }


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

            if (response == "COMMAND_SAVE_TASK")
            {
                TaskItem task = new TaskItem
                {
                    Title = bot.PendingTaskTitle,
                    ReminderDate = DateTime.Now,
                    Completed = false
                };

                tasks.Add(task);

                lstTasks.Items.Add(task);

                ActivityLogger.Log("Task Added: " + task.Title);

                AppendBotMessage("Task saved successfully!");

                txtUserInput.Clear();

                return;
            }


            if (response == "COMMAND_START_QUIZ")
            {
                StartQuiz();
            }
            else if (response == "COMMAND_SHOW_TASKS")
            {
                btnShowTasks.PerformClick();
            }
            else if (response == "COMMAND_ACTIVITY_LOG")
            {
                btnViewLog.PerformClick();
            }
            else if (response == "COMMAND_SAVE_TASK")
            {
                TaskItem task = new TaskItem
                {
                    Title = bot.PendingTaskTitle,
                    ReminderDate = DateTime.Now,
                    Completed = false
                };

                tasks.Add(task);

                lstTasks.Items.Add(task);

                ActivityLogger.Log("Task Added: " + task.Title);

                AppendBotMessage("Task saved successfully!");
            }
            else
            {
                AppendBotMessage(response);
            }
        }
        private void StartQuiz()
        {
            quizRunning = true;

            quizQuestion = 1;

            quizScore = 0;

            currentAnswer = "B";

            AppendBotMessage(

                "Welcome to CYBERSECURITY QUIZ\n\n" +

                "Question 1/5\n\n" +

                "What should you do if you receive a suspicious email?\n\n" +

                "A) Click every link\n" +

                "B) Verify the sender\n" +

                "C) Ignore your antivirus\n\n" +

                "Type A, B or C."

            );

            ActivityLogger.Log("Quiz Started");
        }

        private void CheckQuizAnswer(string answer)
        {
            answer = answer.Trim().ToUpper();

            if (answer == currentAnswer)
            {
                quizScore++;

                AppendBotMessage("Correct!");
            }
            else
            {
                AppendBotMessage("Incorrect ");
            }

            quizQuestion++;

            switch (quizQuestion)
            {

                case 2:

                    currentAnswer = "A";

                    AppendBotMessage(

                        "Question 2/5\n\n" +

                        "HTTPS websites are...\n\n" +

                        "A) More secure\n" +

                        "B) Dangerous\n" +

                        "C) Illegal"

                    );

                    break;

                case 3:

                    currentAnswer = "B";

                    AppendBotMessage(

                        "Question 3/5\n\n" +

                        "Should you reuse passwords?\n\n" +

                        "A) Yes\n" +

                        "B) No"

                    );

                    break;

                case 4:

                    currentAnswer = "A";

                    AppendBotMessage(

                        "Question 4/5\n\n" +

                        "Two-Factor Authentication improves security.\n\n" +

                        "A) True\n" +

                        "B) False"

                    );

                    break;

                case 5:

                    currentAnswer = "B";

                    AppendBotMessage(

                        "Question 5/5\n\n" +

                        "Phishing attacks try to...\n\n" +

                        "A) Speed up the internet\n" +

                        "B) Steal information\n" +

                        "C) Improve passwords"

                    );

                    break;

                default:

                    quizRunning = false;

                    AppendBotMessage(

                        "QUIZ COMPLETE!\n\n" +

                        "Your Score: "

                        + quizScore +

                        "/5\n\n" +

                        GetQuizFeedback()

                    );

                    ActivityLogger.Log(

                        "Quiz Finished. Score: "

                        + quizScore +

                        "/5"

                    );

                    break;

            }
        }

        private string GetQuizFeedback()
        {
            if (quizScore == 5)
                return "Outstanding! You have excellent cybersecurity knowledge.";

            if (quizScore >= 4)
                return "Great job! You know cybersecurity well.";

            if (quizScore >= 3)
                return "Good effort. Keep learning.";

            return "You should revise cybersecurity basics.";
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
        {// Check if textbox is empty

        if (string.IsNullOrWhiteSpace(txtTask.Text))
                {
                    MessageBox.Show("Please enter a task.");
                    return;
                }

            // Create a new task
            TaskItem task = new TaskItem
            {
                Title = txtTask.Text,
                Description = "Cybersecurity Task",
                ReminderDate = dtpReminder.Value,
                Completed = false
            };

            try
            {
                try
                {
                    database.AddTask(task);

                    MessageBox.Show("DatabaseHelper finished.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                // Show in ListBox
                lstTasks.Items.Add(task);

                // Log activity
                ActivityLogger.Log("Task Added: " + task.Title);

                txtTask.Clear();

                MessageBox.Show("Task added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            selectedTask.Completed = true;

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

      /*  private void button1_Click(object sender, EventArgs e)
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

                quizQuestion++;

                switch (quizQuestion)
                {
                    case 2:

                        lblQuestion.Text =
                        "Question 2\n\n" +
                        "HTTPS websites are...\n\n" +
                        "A) Safer\n" +
                        "B) Dangerous\n" +
                        "C) Illegal";

                        currentAnswer = "A";

                        break;

                    case 3:

                        lblQuestion.Text =
                        "Question 3\n\n" +
                        "Should you reuse passwords?\n\n" +
                        "A) Yes\n" +
                        "B) No";

                        currentAnswer = "B";

                        break;

                    case 4:

                        lblQuestion.Text =
                        "Question 4\n\n" +
                        "Two-factor authentication improves security.\n\n" +
                        "A) True\n" +
                        "B) False";

                        currentAnswer = "A";

                        break;

                    case 5:

                        lblQuestion.Text =
                        "Question 5\n\n" +
                        "Phishing attacks usually try to...\n\n" +
                        "A) Protect data\n" +
                        "B) Steal information\n" +
                        "C) Speed up internet";

                        currentAnswer = "B";

                        break;

                    default:

                        lblQuestion.Text =
                        "Quiz Finished!\n\nFinal Score: " +
                        quizScore +
                        "/5";

                        ActivityLogger.Log(
                        "Quiz Finished");

                        break;
                }
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

        }*/
/*  private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            // Start the quiz by displaying the first question
            quizQuestion = 1;

            lblQuestion.Text =
            "Question 1\n\n" +
            "What should you do if you receive a suspicious email?\n\n" +
            "A) Click it\n" +
            "B) Verify the sender\n" +
            "C) Ignore antivirus";

            currentAnswer = "B";

            ActivityLogger.Log("Quiz Started");
        }*/

        private void btnShowTasks_Click(object sender, EventArgs e)
        {
            if (tasks.Count == 0)
            {
                AppendBotMessage("You have no tasks.");
                return;
            }

            AppendBotMessage("YOUR TASKS");

            int count = 1;

            foreach (TaskItem task in tasks)
            {
                AppendBotMessage(
                    count + ". " +
                    task.Title +
                    " | " +
                    (task.Completed ? "Completed" : "Pending"));

                count++;
            }
        }
    }
}
