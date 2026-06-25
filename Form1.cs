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

            // Display bot response
            AppendBotMessage(response);

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

            // Creating a new TaskItem object
            TaskItem task = new TaskItem
            {
                Title = txtTask.Text,
                Description = "Cybersecurity Task",
                ReminderDate = DateTime.Now,
                IsCompleted = false
            };

            //Storing task in List<TaskItem>
            tasks.Add(task);

            //Displaying task in ListBox
            lstTasks.Items.Add(task.Title);

            // Record activity in the Activity Logger
            ActivityLogger.Log("Task Added: " + task.Title);

            // Clear textbox for next task
            txtTask.Clear();

            //Confirmation that task was saved
            MessageBox.Show("Task added successfully!");



        }
    }
}
