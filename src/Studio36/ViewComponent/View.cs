using System;
using Studio36.ControllerComponent;

namespace Studio36.ViewComponent
{
    public class View
    {
        private bool isRunning = true;
        private readonly StartMenu startMenu;
        private readonly MainMenu mainMenu;

        public MenuState CurrentState { get; set; } = MenuState.StartMenu;

        public event Action<string, string>? UserAttemptLogin;
        public event Action<string, string>? UserAttemptSignUp;
        public event Action<int>? UserRequestsProjectTasks;

        public View()
        {
            startMenu = new StartMenu();
            mainMenu = new MainMenu();
        }

        public void Run()
        {
            while (isRunning)
            {
                switch (CurrentState)
                {
                    case MenuState.StartMenu:
                        RunStartMenu();
                        break;

                    case MenuState.MainMenu:
                        RunMainMenu();
                        break;

                    case MenuState.Exit:
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;
                }
            }
        }

        private void RunStartMenu()
        {
            startMenu.DisplayMenu();
            string userInput = (startMenu.GetUserInput() ?? "").Trim();
            StartMenuOption selectedOption = startMenu.GetMenuOption(userInput);

            switch (selectedOption)
            {
                case StartMenuOption.Login:
                    var loginCredentials = startMenu.GetLoginData();
                    UserAttemptLogin?.Invoke(loginCredentials.email, loginCredentials.password);
                    break;

                case StartMenuOption.SignUp:
                    var signUpCredentials = startMenu.GetSignUpData();
                    UserAttemptSignUp?.Invoke(signUpCredentials.email, signUpCredentials.password);
                    break;

                case StartMenuOption.Exit:
                    CurrentState = MenuState.Exit;
                    break;

                default:
                    Console.WriteLine("Invalid option, try again.");
                    Pause();
                    break;
            }
        }

        private void RunMainMenu()
        {
            mainMenu.DisplayMenu();
            string userInput = (mainMenu.GetUserInput() ?? "").Trim();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("New Project not implemented yet.");
                    Pause();
                    break;

                case "2":
                    Console.Write("Please enter the project ID: ");
                    string projectIdInput = Console.ReadLine() ?? "";

                    if (int.TryParse(projectIdInput, out int idProjeto))
                    {
                        UserRequestsProjectTasks?.Invoke(idProjeto);
                    }
                    else
                    {
                        ShowErrorMessage("The project ID must be an integer.");
                    }

                    break;

                case "3":
                    CurrentState = MenuState.StartMenu;
                    break;

                default:
                    Console.WriteLine("Invalid option, try again.");
                    Pause();
                    break;
            }
        }

        public void ShowLoginResult(bool isLoggedIn, string message)
        {
            Console.WriteLine(message);

            if (isLoggedIn)
            {
                CurrentState = MenuState.MainMenu;
            }

            Pause();
        }

        public void ShowSignUpResult(bool success, string message)
        {
            Console.WriteLine(message);
            Pause();
        }

        public void ShowErrorMessage(string message)
        {
            Console.WriteLine($"Input error: {message}");
            Console.WriteLine("Please correct the data and try again.");
            Pause();
        }

        public void RefreshProjectList(List<string> listaProjetos)
        {
            Console.WriteLine("Updated project list:");

            if (listaProjetos.Count == 0)
            {
                Console.WriteLine("There are no projects available.");
            }
            else
            {
                foreach (string projeto in listaProjetos)
                {
                    Console.WriteLine(projeto);
                }
            }

            Pause();
        }

        public void ShowTaskList(List<string> tarefas)
        {
            Console.WriteLine("Task list:");

            if (tarefas.Count == 0)
            {
                Console.WriteLine("There are no tasks associated with this project.");
            }
            else
            {
                foreach (string tarefa in tarefas)
                {
                    Console.WriteLine($"- {tarefa}");
                }
            }

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
