Welcome to Grace - Cybersecurity Assistant, an interactive desktop application designed to educate users on cybersecurity best practices. This app provides tips on password safety, phishing prevention, and privacy protection, along with a task management system and a quiz feature to test your knowledge. Built using C# and WPF, Grace is a user-friendly tool to enhance your online security awareness.

Prerequisites
Operating System: Windows 10 or later (due to WPF framework requirements).
Development Environment: Visual Studio 2022 Community Edition or later (recommended for opening and running the project).
.NET Framework: Ensure the .NET Desktop Development workload is installed in Visual Studio, targeting .NET Framework 4.8 or higher.
Optional Audio File: Place a greeting.wav file in the Assets folder for a welcome sound (optional but enhances experience).
How to Open the App
Clone the Repository:
Open a terminal or Git client and run:
text

Collapse

Wrap

Copy
git clone https://github.com/yourusername/grace-cybersecurity-assistant.git
Replace yourusername with your GitHub username and the repository URL with the actual link.
Open in Visual Studio:
Launch Visual Studio 2022.
Go to File > Open > Project/Solution and navigate to the cloned repository folder.
Select the chatbot.csproj file (or the main project file) and click Open.
Restore any missing NuGet packages if prompted by clicking Restore in the Solution Explorer.
Build the Solution:
Click Build > Build Solution from the top menu to compile the project.
Ensure there are no build errors. If errors occur, check that all dependencies are installed and the Assets folder contains greeting.wav if used.
Run the App:
Press F5 or click Debug > Start Debugging to launch the app.
The app window will open, centered on your screen, ready for interaction.
What to Do When You Open the App
Upon launching Grace, you’ll see a sleek, dark-themed interface divided into two main sections:

Left Panel: Displays the chat history and input area for interacting with Grace.
Right Panel: Contains task management controls, including a task list, input fields, and buttons for additional features.
When the app starts:

A welcome message with an ASCII art logo will appear, accompanied by a sound (if greeting.wav is present).
Grace will prompt you to enter your name by displaying: "👋 What is your name? (Please type your name below)".
Enter your name and press Enter or click the Send button to proceed. This personalizes your experience and moves you to the next step.
How to Use the App
Grace is designed to be intuitive. Follow these steps to navigate and utilize its features:

1. Initial Setup
Enter Your Name: Type your name (e.g., "John") in the input box and press Enter or click Send. Grace will welcome you and ask how you’re feeling today (e.g., "How are you doing today?").
Share Your Mood: Respond with a feeling like "good", "bad", "worried", "okay", or similar. Grace will tailor its response based on your input, transitioning to the main interface.
2. Explore Cybersecurity Topics
After setting up, Grace will display topic options:
1. Password Safety
2. Phishing Scams
3. Safe Browsing Tips
Select a Topic: Type the number (e.g., "1" for Password Safety) or the topic name (e.g., "password") and press Enter. Grace will provide a series of tips, cycling through multiple pieces of advice.
Alternative Input: Type related keywords (e.g., "passwor" or "scam") to trigger the same content with flexible spelling recognition.
3. Manage Tasks
Using Text Commands:
Add a Task: Type add task <title> [with description ...] [in X days/tomorrow/next week] (e.g., "add task check VPN tomorrow" or "add task submit report with description email manager in 2 days").
View Tasks: Type show tasks to list all tasks in the chat.
Complete a Task: Type complete task <title> (e.g., "complete task check VPN").
Delete a Task: Type delete task <title> (e.g., "delete task submit report").
Using the GUI:
In the right panel, enter a task title in the "Task Title" field, a description in the "Task Description" field, and an optional reminder date using the date picker.
Click Add Task to save the task. It will appear in the task list below.
Select a task from the list and click Complete or Delete to update its status or remove it.
4. Take the Cybersecurity Quiz
Start the Quiz: Type quiz or click the Start Quiz button in the right panel.
Answer Questions: Grace will present 10 questions (multiple-choice or true/false) with options. Type the letter (e.g., "A", "B") or "T"/"F" and press Enter, or type skip to move to the next question.
Get Feedback: Receive instant feedback ("✅ Correct!" or "❌ Wrong!") with explanations. After all questions, see your score and tailored feedback (e.g., "🏆 Great job! You're a cybersecurity pro!" for 8+ correct answers).
5. View Activity Log
Type activity log or click Show Log in the right panel to display the last 10 actions (e.g., task additions, quiz starts) in the chat window.
6. Exit the App
Type exit or quit and press Enter to close Grace. It will display a farewell message and shut down.
Additional Notes
Customization: Add a greeting.wav file to the Assets folder for an audio welcome. Ensure it’s a valid WAV file to avoid errors.
Troubleshooting: If the app doesn’t run, verify Visual Studio settings, rebuild the solution, or check the console for error messages.
Contributions: Feel free to fork this repository, submit issues, or propose enhancements via pull requests.[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/Q0H1VfQX)
