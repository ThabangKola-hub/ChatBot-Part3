using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatBot
{
    public class CyberBot
    {


        //Conversation State
        private bool waitingForTaskTitle = false;
        private bool waitingForTaskDescription = false;
        private bool waitingForReminder = false;

        //Temporary storage
        private string currentTaskTitle = "";
        // Chat-based task creation
        private bool waitingForTaskConfirmation = false;

        // Commands for Form1
        public string PendingTaskTitle => currentTaskTitle;
       



        private Random random = new Random();

        //Memory
        private string lastTopic = "";
        public string UserName { get; set; }


        //Memory for Tips
        private string[] passwordTips =
        {
            "Use strong passwords with symbols and numbers.",
            "Avoid using your birthdate in passwords.",
            "Use different passwords for every account."
        };

        private string[] phishingTips =
        {
            "Do not click suspicious email links.",
            "Always verify the sender's email address.",
            "Phishing emails often create urgency."
        };

        private string[] safeBrowsingTips =
        {
            "Only visit secure websites using HTTPS.",
            "Avoid downloading files from unknown websites.",
            "Keep your browser updated regularly."
        };

        // Cybersecurity Knowledge Base
        private Dictionary<string, string[]> knowledgeBase =
            new Dictionary<string, string[]>()
        {
    {
        "password",
        new string[]
        {
            "password",
            "passwords",
            "strong password",
            "weak password",
            "forgot password",
            "password security"
        }
    },

    {
        "phishing",
        new string[]
        {
            "phishing",
            "fake email",
            "scam email",
            "email scam",
            "suspicious email"
        }
    },

    {
        "safe browsing",
        new string[]
        {
            "safe browsing",
            "secure browsing",
            "https",
            "unsafe website",
            "web browsing"
        }
    }
        };

        //Conversation overflow
        private string passwordExplanation =
    "Strong passwords help protect your accounts from hackers. A good password" +
            " should contain uppercase letters, lowercase letters, numbers, and symbols.";

        private string phishingExplanation =
            "Phishing attacks attempt to trick users into revealing sensitive " +
            "information like passwords or banking details through fake emails, websites, or messages.";

        private string browsingExplanation =
            "Safe browsing means avoiding suspicious websites, keeping " +
            "software updated, and not downloading unknown files from the internet.";

        // Detects the user's intended topic
        private string DetectTopic(string input)
        {
            foreach (var topic in knowledgeBase)
            {
                foreach (string keyword in topic.Value)
                {
                    if (input.Contains(keyword))
                    {
                        return topic.Key;
                    }
                }
            }

            return "";
        }

        //Emotion detection or sentiment detection
        private string DetectSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("scared"))
            {
                return "I understand cybersecurity threats can feel worrying. I'm here to help you stay informed and safe.";
            }
            else if (input.Contains("confused") || input.Contains("frustrated"))
            {
                return "Cybersecurity can seem complicated at first, but I'll explain things simply.";
            }

            return "";
        }


        public string GetResponse(string userInput)

        {
            userInput = userInput.ToLower();
            string detectedTopic = DetectTopic(userInput);

            // Help Menu
            if (userInput == "help")
            {
                return
            @"I can help you with:

Cybersecurity
--------------
• Password safety
• Phishing
• Safe browsing

Task Management
---------------
• Add task
• Show tasks
• Complete task
• Delete task

Learning
--------
• Quiz
• Activity log

Examples:
---------
password
phishing
safe browsing
add task
show tasks
quiz";
            }
        
            //Responding from help menu

            if (userInput == "show tasks")
            {
                return "COMMAND_SHOW_TASKS";
            }

            if (userInput == "activity log")
            {
                return "COMMAND_ACTIVITY_LOG";
            }

         if (userInput.Contains("quiz"))
{
    return "COMMAND_START_QUIZ";
}



            //Calling sentiment detection
            string sentimentResponse = DetectSentiment(userInput);

            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                return sentimentResponse;
            }


            // ===============================
            // CHAT TASK CREATION
            // ===============================

            if (userInput == "add task")
            {
                waitingForTaskTitle = true;

                return "Sure! What is the title of your task?";
            }

            if (waitingForTaskTitle)
            {
                currentTaskTitle = userInput;

                waitingForTaskTitle = false;
                waitingForTaskConfirmation = true;

                return "Great! Type 'save' to save the task or 'cancel' to cancel.";
            }

            if (waitingForTaskConfirmation)
            {
                waitingForTaskConfirmation = false;

                if (userInput == "save")
                {
                    return "COMMAND_SAVE_TASK";
                }

                currentTaskTitle = "";

                return "Task creation cancelled.";
            }



            //Conversation overflow detection
            if (userInput.Contains("tell me more") ||
                userInput.Contains("explain more") ||
                userInput.Contains("another tip") ||
                userInput.Contains("continue"))
            {
                if (lastTopic == "password")
                {
                    return passwordExplanation;
                }

                else if (lastTopic == "phishing")
                {
                    return phishingExplanation;
                }

                else if (lastTopic == "browsing")
                {
                    return browsingExplanation;
                }

                else
                {
                    return "Please ask about a cybersecurity topic first, such as passwords, phishing, or safe browsing.";
                }
            }

            //greeting
            if (userInput == "hello" ||
    userInput == "hi" ||
    userInput == "hey")
            {
                return "Hello! I'm your Cybersecurity Assistant. Type 'help' to see everything I can do.";
            }
            if (userInput.Contains("thank"))
            {
                return "You're welcome! Stay safe online.";
            }
            if (userInput == "bye" ||
    userInput == "goodbye")
            {
                return "Goodbye! Stay cyber safe.";
            }
            if (userInput == "date")
            {
                return DateTime.Now.ToLongDateString();
            }
            if (userInput == "time")
            {
                return DateTime.Now.ToShortTimeString();
            }
            if (userInput.Contains("who are you"))
            {
                return "I'm CyberBot, your cybersecurity awareness assistant.";
            }

            //Password section
            if (detectedTopic == "password")
            {
                    string response = passwordTips[random.Next(passwordTips.Length)];

                    if (lastTopic == "password")
                    {
                        response = "Earlier you asked about passwords. Remember: " + response;
                    }

                    lastTopic = "password";
                    return response;

            }

            //Phishing section
            if (detectedTopic == "phishing")
            {
                string response = phishingTips[random.Next(phishingTips.Length)];

                if (lastTopic == "phishing")
                {
                    response = "We previously discussed phishing. " + response;
                }

                lastTopic = "phishing";
                return response; 
            }


            //Safe browsing section
            // Safe browsing section
            else if (detectedTopic == "safe browsing" || detectedTopic == "browsing")
            {
                string response = safeBrowsingTips[random.Next(safeBrowsingTips.Length)];

                if (lastTopic == "browsing")
                {
                    response = "Earlier we talked about safe browsing. " + response;
                }

                lastTopic = "browsing";
                return response;
            }

            //Greeting section
            else if (userInput.Contains("how are you"))
            {
                return "I'm functioning good and ready to help you stay safe online!";
            }

                //Purpose response section
            else if (userInput.Contains("purpose"))
            {
                return "My purpose is to educate users about cybersecurity awareness.";
            }

                //Input error section
            else
            {
                return "I didn't quite understand that. Please try asking about passwords, phishing, or safe browsing.";
            }
        }
    }
}