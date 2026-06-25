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
    }
}
