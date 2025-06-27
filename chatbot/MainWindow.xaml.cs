using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;



// Code Attributions:
// - W3Schools (https://www.w3schools.com) - General guidance on XAML layout and C# event handling.
// - Microsoft Docs (https://docs.microsoft.com) - References for WPF controls, threading, and exception handling.
// - Stack Overflow (https://stackoverflow.com) - Solutions for regex pattern matching and list manipulation.
// - Pygame Documentation (https://www.pygame.org/docs) - Inspiration for audio handling with SoundPlayer.
// - GitHub Repositories (https://github.com) - Examples of task management UI design in WPF applications.
// - Mozilla Developer Network (https://developer.mozilla.org) - Insights on UI responsiveness and styling.


namespace chatbot
{
    public partial class MainWindow : Window
    {
        enum ChatStage { Greeting, AskName, AskMood, Ready }
        ChatStage stage = ChatStage.Greeting; // Defines the conversation stages: starts at Greeting to welcome users.

        private List<string> activityLog = new List<string>(); // Stores up to 10 recent actions for the activity log feature.
        private List<(string title, string description, DateTime reminderDate, bool hasReminder, bool completed)> tasks = new List<(string, string, DateTime, bool, bool)>(); // Holds task data with title, description, reminder date, and completion status.
        private string lastKeyword = ""; // Tracks the last cybersecurity topic discussed for context.
        private string userInterest = ""; // Records the user's preferred cybersecurity topic.
        private bool quizActive = false; // Tracks whether a quiz is in progress.
        private int quizIndex = 0, quizScore = 0; // Manages quiz question index and user score.
        private string userAlias = "Cyber Cadet"; // Default username, updated when user provides their name.

        private DispatcherTimer reminderTimer = new DispatcherTimer(); // Timer to check for overdue tasks every 5 seconds.

        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>
        {
            {
                "password", new List<string>
                {
                    "🔐 Create strong passwords with at least 12 characters, including uppercase, lowercase, numbers, and symbols (e.g., P@ssw0rd!2025). Avoid common words or personal info like birthdays.",
                    "🚫 Never reuse passwords across different websites or accounts. If one site is compromised, hackers can use the same password to access other accounts.",
                    "🔒 Enable two-factor authentication (2FA) whenever available. This adds a second verification step, like a code sent to your phone, making it harder for attackers to gain access.",
                    "🔄 Change passwords every 6-12 months, or immediately if a service reports a data breach. Use a password manager like LastPass or Bitwarden to securely store and generate unique passwords.",
                    "⚠️ Avoid sharing passwords via email, text, or unsecured apps. If you must share, use a secure method like an encrypted messaging app or a one-time password-sharing tool."
                }
            },
            {
                "scam", new List<string>
                {
                    "📧 Always verify the sender’s email address. Scammers often use slightly altered domains (e.g., support@paypa1.com instead of support@paypal.com) to trick you into trusting fake messages.",
                    "🚨 Be cautious of unsolicited emails, texts, or calls with urgent demands, like 'Your account is locked!' These are common phishing tactics to steal your credentials or money.",
                    "🔍 Don’t click on links or download attachments from unknown sources. Hover over links to check the URL, and scan files with antivirus software before opening.",
                    "🛡️ Use spam filters in your email client and report suspicious messages as phishing. Services like Gmail and Outlook have built-in tools to flag and block scam attempts.",
                    "🧠 Stay informed about common scams, like fake tech support calls, lottery frauds, or romance scams. If an offer seems too good to be true, it probably is—verify before acting."
                }
            },
            {
                "privacy", new List<string>
                {
                    "🌐 Use a Virtual Private Network (VPN) on public Wi-Fi to encrypt your data. Free Wi-Fi at cafes or airports can be targeted by hackers to intercept sensitive information.",
                    "🔐 Regularly review privacy settings on social media platforms like X or Facebook. Limit who can see your posts, avoid sharing sensitive details like your address, and disable location tagging.",
                    "🚫 Block unnecessary app permissions on your phone or computer. For example, a flashlight app doesn’t need access to your contacts or location—check and revoke permissions in settings.",
                    "🗑️ Clear browser cookies and cache monthly to reduce tracking by advertisers and websites. Use browsers like Firefox with enhanced tracking protection for better privacy.",
                    "🔍 Be mindful of oversharing online. Avoid posting sensitive information like travel plans or financial details, as scammers can use this to target you or impersonate you."
                }
            }
        };

        private Dictionary<string, int> responseIndex = new Dictionary<string, int>(); // Keeps track of the current tip index for each keyword topic.

        private List<(string question, string[] options, int correctAnswerIndex, bool isTrueFalse, string explanation)> quizQuestions = new List<(string, string[], int, bool, string)>
        {
            ("What does HTTPS stand for?", new[] { "HyperText Transfer Protocol Secure", "Hyper Terminal Secure", "Host Transfer Secure" }, 0, false, "HTTPS indicates a secure, encrypted connection."),
            ("Should you reuse passwords?", new[] { "True", "False" }, 1, true, "Using unique passwords reduces the risk of multiple accounts being compromised."),
            ("Is public Wi-Fi safe for online banking?", new[] { "True", "False" }, 1, true, "Public Wi-Fi can be insecure; use a VPN for sensitive activities."),
            ("What is two-factor authentication?", new[] { "Two-Factor Authentication", "Two-Faced Algorithm", "Too Fast Access" }, 0, false, "Two-factor authentication adds an extra layer of security."),
            ("How do you spot a phishing scam?", new[] { "Grammar mistakes", "Urgency", "All of the above" }, 2, false, "Phishing scams often use urgency and errors to trick users."),
            ("What should a strong password include?", new[] { "Letters and numbers", "Symbols", "All of the above" }, 2, false, "Strong passwords include a mix of letters, numbers, and special characters."),
            ("What is a VPN used for?", new[] { "Encrypt traffic", "Speed up internet" }, 0, false, "A VPN encrypts your internet traffic for security."),
            ("Are browser updates important?", new[] { "True", "False" }, 0, true, "Browser updates fix security vulnerabilities."),
            ("What should you do if you receive an email asking for your password?", new[] { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" }, 2, false, "Reporting phishing emails helps prevent scams."),
            ("Social engineering involves manipulating people to gain confidential information.", new[] { "True", "False" }, 0, true, "Social engineering exploits human psychology to bypass security.")
        };

        public MainWindow()
        {
            InitializeComponent(); // Initializes the app by loading the user interface from the XAML file, setting up the window and controls.
            reminderTimer.Interval = TimeSpan.FromSeconds(5); // Configures the reminder timer to check for due tasks every 5 seconds for timely notifications.
            reminderTimer.Tick += ReminderTimer_Tick; // Attaches the ReminderTimer_Tick method to handle reminder checks when the timer ticks.
            reminderTimer.Start(); // Activates the timer to begin monitoring task reminders.

            ShowIntro(); // Triggers the display of a fun ASCII art logo and a welcoming message to engage the user right away.
            PlayStartupSound(); // Attempts to play a welcome sound file named "greeting.wav" from the Assets folder, adding an audio cue if available.
            stage = ChatStage.AskName; // Transitions the conversation state to ask the user for their name after the intro.
            AppendText("👋 What is your name?\n(Please type your name below)"); // Prompts the user to enter their name in the text box with a friendly emoji.
            UpdateTaskList(); // Initializes the task list display in the GUI to ensure it’s empty and ready for new tasks.
        } // End MainWindow constructor

        private async void SendButton_Click(object sender, RoutedEventArgs e) => await ProcessInput(); // Asynchronously triggers the ProcessInput method when the Send button is clicked, handling user input.
        private async void UserInputTextBox_KeyDown(object sender, KeyEventArgs e) { if (e.Key == Key.Enter) await ProcessInput(); } // Allows users to send input by pressing the Enter key, enhancing keyboard accessibility.
        private void AddTaskButton_Click(object sender, RoutedEventArgs e) => AddTaskFromGUI(); // Calls the AddTaskFromGUI method when the Add Task button is clicked in the GUI.
        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e) => CompleteTaskFromGUI(); // Invokes the CompleteTaskFromGUI method when the Complete button is pressed.
        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e) => DeleteTaskFromGUI(); // Executes the DeleteTaskFromGUI method when the Delete button is clicked.
        private void StartQuizButton_Click(object sender, RoutedEventArgs e) => StartQuiz(); // Launches the quiz feature when the Start Quiz button is activated.
        private void ShowLogButton_Click(object sender, RoutedEventArgs e) => ShowActivityLog(); // Displays the activity log when the Show Log button is selected.
        private void TaskTitleTextBox_GotFocus(object sender, RoutedEventArgs e) { if (TaskTitleTextBox.Text == "Task Title") TaskTitleTextBox.Text = ""; } // Clears the "Task Title" placeholder text when the user clicks into the field.
        private void TaskDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e) { if (TaskDescriptionTextBox.Text == "Task Description") TaskDescriptionTextBox.Text = ""; } // Removes the "Task Description" placeholder when the user focuses on that field.

        private async Task ProcessInput()
        {
            string input = UserInputTextBox.Text.Trim(); // Captures the user’s input from the text box and removes leading/trailing spaces.
            if (string.IsNullOrWhiteSpace(input)) return; // Exits the method if the input is empty or just whitespace to avoid processing nothing.
            AppendText("You: " + input); // Displays the user’s input in the chat history with a "You:" prefix for clarity.
            UserInputTextBox.Clear(); // Empties the input box after processing to prepare for the next command.

            if (stage == ChatStage.AskName) // Manages the initial name input phase of the conversation.
            {
                userAlias = input; // Assigns the user’s input as their alias for personalized responses.
                AppendText("Welcome aboard, " + userAlias + "!"); // Greets the user by their chosen name with a warm message.
                stage = ChatStage.AskMood; // Advances the conversation to the mood-checking stage.
                AppendText("How are you doing today?"); // Asks the user about their mood to tailor the next response.
                return;
            }
            else if (stage == ChatStage.AskMood) // Handles the mood input phase with context-aware replies.
            {
                if (input.Contains("good") || input.Contains("great"))
                    AppendText("That's great to hear! Let's dive into some cybersecurity tips to keep you safe online!");
                else if (input.Contains("bad"))
                    AppendText("Sorry to hear that. Learning about cybersecurity can empower you to stay secure. Let’s start with some tips!");
                else if (input.Contains("worried"))
                    AppendText("😟 I can sense you're worried. Cybersecurity threats can feel overwhelming, but I’ll guide you through protecting yourself. Try typing 'password', 'scam', or 'privacy' to learn more, or use the quiz to test your knowledge!");
                else if (input.Contains("okay") || input.Contains("fine"))
                    AppendText("Glad you're doing okay! Let’s explore some cybersecurity topics to keep you informed and secure.");
                else
                    AppendText("Got it! Let's explore some cybersecurity topics to keep you informed and secure.");

                stage = ChatStage.Ready; // Moves to the ready state where full functionality is unlocked.
                ShowTopicOptions(); // Presents the user with topic choices and a guide to explore features.
                return;
            }

            if (quizActive) // Processes quiz answers if a quiz is active.
            {
                ProcessQuizAnswer(input);
                return;
            }

            // Enhanced NLP with flexible intent detection
            var intents = new Dictionary<string, List<string>>
            {
                { "add task", new List<string> { "create task", "new task", "set task", "add a task", "remind me", "remind me to", "schedule task" } },
                { "complete task", new List<string> { "finish task", "done task", "mark task complete", "complete it" } },
                { "delete task", new List<string> { "remove task", "delete task", "erase task", "trash task" } },
                { "show tasks", new List<string> { "view tasks", "list tasks", "display tasks", "show my tasks", "help me with tasks" } },
                { "activity log", new List<string> { "activity history", "what have you done", "show log", "view log", "check history" } },
                { "quiz", new List<string> { "start quiz", "take quiz", "test me", "begin quiz", "try quiz" } }
            };

            string intent = intents.Keys.FirstOrDefault(k => input.ToLower().Contains(k)) ??
                            intents.FirstOrDefault(kvp => kvp.Value.Any(syn => input.ToLower().Contains(syn))).Key;

            if (intent == "add task" || intents["add task"].Any(syn => input.ToLower().Contains(syn))) // Processes commands to add a new task.
            {
                string title = "", description = "No description.", reminderText = "";
                int days = 0;
                bool hasReminder = false;

                var words = input.ToLower().Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
                var wordDict = new Dictionary<string, string>
                {
                    { "with", "" }, { "description", "" }, { "in", "" }, { "days", "" }, { "day", "" }, { "tomorrow", "" }, { "next", "" }, { "week", "" }
                };

                for (int i = 0; i < words.Length; i++)
                {
                    if (string.IsNullOrEmpty(title) && !wordDict.ContainsKey(words[i]))
                    {
                        title = words[i];
                        continue;
                    }
                    if (words[i] == "with" && i + 1 < words.Length && words[i + 1] == "description")
                    {
                        i++;
                        var descBuilder = new System.Text.StringBuilder();
                        while (i + 1 < words.Length && words[i + 1] != "in" && words[i + 1] != "tomorrow" && words[i + 1] != "next")
                        {
                            descBuilder.Append(words[i + 1] + " ");
                            i++;
                        }
                        description = descBuilder.ToString().Trim();
                        continue;
                    }
                    if ((words[i] == "in" || words[i] == "tomorrow" || words[i] == "next") && i + 1 < words.Length)
                    {
                        i++;
                        if (int.TryParse(words[i], out days) && days > 0)
                        {
                            hasReminder = true;
                            reminderText = $"in {days} days";
                        }
                        else if (words[i] == "tomorrow")
                        {
                            hasReminder = true;
                            days = 1;
                            reminderText = "tomorrow";
                        }
                        else if (words[i] == "week")
                        {
                            hasReminder = true;
                            days = 7;
                            reminderText = "in 1 week";
                        }
                        continue;
                    }
                }

                if (string.IsNullOrEmpty(title))
                {
                    AppendText("❌ Error: Please provide a task title (e.g., 'add task submit report').");
                    return;
                }

                DateTime reminder = hasReminder ? DateTime.Now.AddDays(days) : default(DateTime);
                tasks.Add((title, description, reminder, hasReminder, false));
                Log($"Task added: {title} {(hasReminder ? $" (Reminder: {reminder.ToString("MMM dd")})" : "")}");
                AppendText($"📌 Task: {title}\nDesc: {description}\nReminder: {reminderText}\n");
                UpdateTaskList();
                return;
            }

            if (intent == "show tasks" || intents["show tasks"].Any(syn => input.ToLower().Contains(syn))) // Displays the current list of tasks.
            {
                ShowTasks();
                return;
            }

            if (intent == "complete task" || intents["complete task"].Any(syn => input.ToLower().Contains(syn))) // Marks a specified task as completed.
            {
                var match = Regex.Match(input, @"complete\s*task(?:\s*[-:]\s*)?(.+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string title = match.Groups[1].Value.Trim();
                    var index = tasks.FindIndex(t => t.title.Equals(title, StringComparison.OrdinalIgnoreCase));
                    if (index >= 0)
                    {
                        var task = tasks[index];
                        tasks[index] = (task.title, task.description, task.reminderDate, task.hasReminder, true);
                        AppendText("✅ Task '" + title + "' marked as complete.");
                        Log("Completed task: " + title);
                        UpdateTaskList();
                    }
                    else
                    {
                        AppendText("Task not found.");
                    }
                }
                else
                {
                    AppendText("❌ Please specify a task to complete (e.g., 'complete task submit report').");
                }
                return;
            }

            if (intent == "delete task" || intents["delete task"].Any(syn => input.ToLower().Contains(syn))) // Removes a task from the list.
            {
                var match = Regex.Match(input, @"delete\s*task(?:\s*[-:]\s*)?(.+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string title = match.Groups[1].Value.Trim();
                    var index = tasks.FindIndex(t => t.title.Equals(title, StringComparison.OrdinalIgnoreCase));
                    if (index >= 0)
                    {
                        tasks.RemoveAt(index);
                        AppendText("🗑️ Task '" + title + "' deleted.");
                        Log("Deleted task: " + title);
                        UpdateTaskList();
                    }
                    else
                    {
                        AppendText("Task not found.");
                    }
                }
                else
                {
                    AppendText("❌ Please specify a task to delete (e.g., 'delete task submit report').");
                }
                return;
            }

            if (intent == "activity log" || intents["activity log"].Any(syn => input.ToLower().Contains(syn))) // Retrieves and displays the activity log.
            {
                ShowActivityLog();
                return;
            }

            if (intent == "quiz" || intents["quiz"].Any(syn => input.ToLower().Contains(syn))) // Initiates the cybersecurity quiz feature.
            {
                StartQuiz();
                return;
            }

            if (input.ToLower().Contains("worried") || input.ToLower().Contains("anxious")) // Provides supportive response for a worried user.
            {
                AppendText("😟 I can sense you're worried. Cybersecurity can be daunting, but small steps make a big difference. For example, using strong passwords and enabling 2FA can protect your accounts. Try typing 'password' for tips or 'quiz' to test your knowledge!");
            }
            else if (input.ToLower().Contains("frustrated") || input.ToLower().Contains("angry")) // Offers help for a frustrated user.
            {
                AppendText("😣 Frustration is understandable—cyber threats are tricky! Let’s simplify things. You can learn about avoiding scams or securing your browsing. Type 'scam' or 'privacy' for detailed advice, or use the right panel to manage tasks and stay organized.");
            }
            else if (input.ToLower().Contains("curious") || input.ToLower().Contains("interested")) // Encourages a curious user to explore.
            {
                AppendText("🤔 Curiosity is a great start! Cybersecurity is all about staying one step ahead. Explore topics like phishing scams or VPNs by typing 'scam' or 'privacy', or try the quiz to dive deeper into key concepts!");
            }

            string keyword = keywordResponses.Keys.FirstOrDefault(k => input.ToLower().Contains(k) || input.ToLower().Replace("s", "").Contains(k)); // Identifies cybersecurity topics with flexible spelling.
            if (!string.IsNullOrEmpty(keyword))
            {
                lastKeyword = userInterest = keyword;
                if (!responseIndex.ContainsKey(keyword)) responseIndex[keyword] = 0;
                var responses = keywordResponses[keyword];
                var idx = responseIndex[keyword];
                AppendText(responses[idx]); // Displays the current tip for the selected topic, cycling through the list.
                responseIndex[keyword] = (idx + 1) % responses.Count;
                Log("Keyword response: " + keyword);
                return;
            }

            if (input == "1" || input.ToLower() == "password safety") keyword = "password";
            else if (input == "2" || input.ToLower() == "phishing scams") keyword = "scam";
            else if (input == "3" || input.ToLower() == "safe browsing") keyword = "privacy";
            if (!string.IsNullOrEmpty(keyword)) // Processes topic selection based on numbered options.
            {
                AppendText("📘 Topic selected: " + keyword);
                AppendText(keywordResponses[keyword][0]);
                return;
            }

            if (input.ToLower() == "exit" || input.ToLower() == "quit") // Handles the app exit command.
            {
                AppendText("👋 Logging off... Stay safe, " + userAlias + ".");
                Application.Current.Shutdown(); // Safely terminates the application, closing all windows.
                return;
            }

            AppendText("🤖 Try 'quiz', 'add task <title> [with description ...] [in X days/tomorrow/next week]', 'complete task <title>', 'delete task <title>', 'show tasks', 'activity log', or a keyword like 'password'.");
        } // End ProcessInput

        private void AddTaskFromGUI() // Adds a new task using the graphical interface on the right panel.
        {
            string title = TaskTitleTextBox.Text.Trim();
            string desc = TaskDescriptionTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(title) || title == "Task Title")
            {
                AppendText("Please enter a valid task title.");
                return;
            }
            desc = string.IsNullOrEmpty(desc) || desc == "Task Description" ? "No description." : desc;
            bool hasReminder = ReminderDatePicker.SelectedDate.HasValue;
            DateTime reminder = hasReminder ? ReminderDatePicker.SelectedDate.Value : default(DateTime);
            tasks.Add((title, desc, reminder, hasReminder, false));
            Log("Task added: " + title + (hasReminder ? " (Reminder: " + reminder.ToString("MMM dd") + ")" : ""));
            AppendText("📌 Task: " + title + "\nDesc: " + desc + "\nReminder: " + (hasReminder ? reminder.ToShortDateString() : "None") + "\n");
            UpdateTaskList();
            TaskTitleTextBox.Text = "Task Title";
            TaskDescriptionTextBox.Text = "Task Description";
            ReminderDatePicker.SelectedDate = null;
        } // End AddTaskFromGUI

        private void CompleteTaskFromGUI() // Marks a selected task as completed through the GUI interface.
        {
            if (TaskListBox.SelectedIndex >= 0)
            {
                var task = tasks[TaskListBox.SelectedIndex];
                tasks[TaskListBox.SelectedIndex] = (task.title, task.description, task.reminderDate, task.hasReminder, true);
                AppendText("✅ Task '" + task.title + "' marked as complete.");
                Log("Completed task: " + task.title);
                UpdateTaskList();
            }
            else
            {
                AppendText("Please select a task to mark as complete.");
            }
        } // End CompleteTaskFromGUI

        private void DeleteTaskFromGUI() // Removes a selected task from the list using the GUI.
        {
            if (TaskListBox.SelectedIndex >= 0)
            {
                var task = tasks[TaskListBox.SelectedIndex];
                tasks.RemoveAt(TaskListBox.SelectedIndex);
                AppendText("🗑️ Task '" + task.title + "' deleted.");
                Log("Deleted task: " + task.title);
                UpdateTaskList();
            }
            else
            {
                AppendText("Please select a task to delete.");
            }
        } // End DeleteTaskFromGUI

        private void StartQuiz() // Initiates a 10-question cybersecurity quiz to test user knowledge.
        {
            quizActive = true; // Activates the quiz mode to process answers.
            quizIndex = 0; // Resets the question index to start from the beginning.
            quizScore = 0; // Clears the previous score for a new attempt.
            Log("Quiz started"); // Records the quiz start in the activity log.
            DisplayQuizQuestion(); // Shows the first question to the user.
        } // End StartQuiz

        private void DisplayQuizQuestion() // Presents the current quiz question with options to the user.
        {
            if (quizIndex < quizQuestions.Count)
            {
                var q = quizQuestions[quizIndex];
                string options = string.Join(", ", q.options.Select((opt, i) => (q.isTrueFalse ? (i == 0 ? "T" : "F") : ((char)('A' + i)).ToString()) + ": " + opt));
                AppendText("🧠 Q" + (quizIndex + 1) + ": " + q.question + "\nOptions: " + options);
                AppendText("Type the letter (A, B, C, D) or T/F to answer, or 'skip' to move on.");
            }
            else
            {
                quizActive = false; // Ends the quiz when all questions are answered.
                string feedback = quizScore >= 8 ? "🏆 Great job! You're a cybersecurity pro!" :
                                 quizScore >= 5 ? "📘 Good effort! Keep learning to stay safe online!" :
                                 "📚 You might want to review cybersecurity basics!";
                AppendText("🎉 Quiz Done! Score: " + quizScore + "/" + quizQuestions.Count + "\n" + feedback);
                Log("Quiz completed with score " + quizScore + "/" + quizQuestions.Count);
                quizIndex = 0; // Resets for future quizzes.
            }
        } // End DisplayQuizQuestion

        private void ProcessQuizAnswer(string input) // Evaluates the user’s answer to the current quiz question.
        {
            var q = quizQuestions[quizIndex];
            if (input.ToLower() == "skip")
            {
                AppendText("⏭️ Skipping to the next question...");
                quizIndex++;
                DisplayQuizQuestion();
                return;
            }
            bool isCorrect = false;
            if (q.isTrueFalse)
            {
                isCorrect = (input.ToUpper() == "T" && q.correctAnswerIndex == 0) || (input.ToUpper() == "F" && q.correctAnswerIndex == 1);
            }
            else
            {
                if (int.TryParse(input, out int answerIndex) && answerIndex >= 1 && answerIndex <= q.options.Length)
                {
                    isCorrect = (answerIndex - 1) == q.correctAnswerIndex;
                }
                else if (input.Length > 0 && char.IsLetter(input[0]) && char.ToUpper(input[0]) - 'A' >= 0 && char.ToUpper(input[0]) - 'A' < q.options.Length)
                {
                    isCorrect = (char.ToUpper(input[0]) - 'A') == q.correctAnswerIndex;
                }
            }

            if (isCorrect)
            {
                quizScore++;
                AppendText("✅ Correct! " + q.explanation);
            }
            else if (!string.IsNullOrWhiteSpace(input))
            {
                AppendText("❌ Wrong! " + q.explanation);
            }
            else
            {
                AppendText("❌ Please provide an answer (e.g., A, B, T, F, or 'skip').");
                return;
            }

            quizIndex++;
            DisplayQuizQuestion();
        } // End ProcessQuizAnswer

        private void ShowActivityLog() // Displays the last 10 recorded actions in the chat window.
        {
            AppendText("🧾 Activity Log:");
            if (activityLog.Count == 0)
            {
                AppendText("No recent activity.");
            }
            else
            {
                foreach (var log in activityLog.Take(10))
                {
                    AppendText(log);
                }
            }
        } // End ShowActivityLog

        private void ShowTasks() // Lists all current tasks with their details in the chat area.
        {
            if (!tasks.Any())
            {
                AppendText("No tasks yet.");
            }
            else
            {
                AppendText("📝 Tasks:");
                foreach (var t in tasks)
                {
                    AppendText("- " + t.title + " | " + t.description + " | " + (t.completed ? "✅ Done" : "❌ Pending") + " | " + (t.hasReminder ? "Reminder: " + t.reminderDate.ToShortDateString() : "No Reminder"));
                }
            }
        } // End ShowTasks

        private void ReminderTimer_Tick(object sender, EventArgs e) // Checks for tasks with reminders due today and notifies the user.
        {
            foreach (var t in tasks)
            {
                if (t.hasReminder && !t.completed && t.reminderDate.Date == DateTime.Now.Date)
                {
                    AppendText("🔔 Reminder: " + t.title + " - " + t.description);
                    Log("Reminder shown: " + t.title);
                }
            }
        } // End ReminderTimer_Tick

        private void AppendText(string text) => ChatHistoryTextBlock.Text += text + "\n\n"; // Appends text to the chat history with double line breaks for readability.

        private void Log(string entry) // Adds a timestamped entry to the activity log, limiting it to 10 items.
        {
            activityLog.Insert(0, DateTime.Now.ToString("T") + " - " + entry);
            if (activityLog.Count > 10) activityLog.RemoveAt(activityLog.Count - 1);
        } // End Log

        private void ShowIntro() // Presents a custom ASCII art logo followed by a welcome message to set the tone.
        {
            string intro = @"  _____)  _____    _____   )   ___      _____) 
 /        (, /   ) (, /  | (__/_____)  /        
/   ___     /__ /    /---|   /         )__      
/     / ) ) /   \_ ) /    |_ /        /          
(____ /   (_/      (_/       (______) (_____)     ";
            AppendText(intro);
            AppendText("Hi, I'm Grace, your cybersecurity assistant!");
        } // End ShowIntro

        private void PlayStartupSound() // Attempts to play a welcome audio file, handling errors if the file is missing.
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");
                SoundPlayer sp = new SoundPlayer(path); // Creates a sound player instance for the specified file.
                sp.Play(); // Plays the audio file synchronously.
            }
            catch (Exception ex) { AppendText("[Audio Error] " + ex.Message); } // Logs any errors, like a missing file, to the chat.
        } // End PlayStartupSound

        private void ShowTopicOptions() // Displays available cybersecurity topics and a detailed user guide for interaction.
        {
            AppendText("Select a topic to begin learning:");
            AppendText("1. Password Safety\n2. Phishing Scams\n3. Safe Browsing Tips");
            AppendText("Or explore these features:");
            AppendText("=== User Guide ===");
            AppendText("Here's what you can do with Grace:");
            AppendText("1. Learn Cybersecurity:");
            AppendText("- Type 'password', 'scam', or 'privacy' for tips.");
            AppendText("- Or select a topic: '1' (Password Safety), '2' (Phishing Scams), '3' (Safe Browsing).");
            AppendText("- Share feelings like 'worried', 'frustrated', or 'curious' for tailored responses.");
            AppendText("2. Manage Tasks:");
            AppendText("- Use the right panel:");
            AppendText("  - Enter a title and description, set an optional reminder date, and click 'Add Task'.");
            AppendText("  - Select a task in the list and click 'Complete' or 'Delete'.");
            AppendText("- Or use text commands:");
            AppendText("  - Add: 'add task submit report with description email manager in 2 days' or 'remind me submit report tomorrow'.");
            AppendText("  - View: 'show tasks'");
            AppendText("  - Complete: 'complete task submit report'");
            AppendText("  - Delete: 'delete task submit report'");
            AppendText("3. Take a Quiz:");
            AppendText("- Click 'Start Quiz' (right panel) or type 'quiz' for a 10-question cybersecurity quiz.");
            AppendText("- Answer with A, B, C, D, or T/F in the chat box, or 'skip' to move on.");
            AppendText("- Get instant feedback and a final score.");
            AppendText("4. View Activity Log:");
            AppendText("- Click 'Show Log' (right panel) or type 'activity log' to see the last 10 actions.");
            AppendText("5. Exit:");
            AppendText("- Type 'exit' or 'quit' to close the app.");
            AppendText("Try a command or use the right panel to get started!");
        } // End ShowTopicOptions

        private void UpdateTaskList() // Refreshes the task list display in the GUI to reflect current task status.
        {
            TaskListBox.Items.Clear(); // Clears the existing list to prevent duplicates.
            foreach (var t in tasks)
            {
                TaskListBox.Items.Add(t.title + " | " + t.description + " | " + (t.completed ? "✅ Done" : "❌ Pending") + " | " + (t.hasReminder ? "Reminder: " + t.reminderDate.ToShortDateString() : "No Reminder"));
            }
        } // End UpdateTaskList
    } // End MainWindow class
} // End chatbot namespace