using System;

namespace Studio36.ViewComponent
{
    public class View
    {
        private bool isRunning = true;
        private readonly StartMenu startMenu;

        public event Action<string, string>? UserAttemptLogin;

        public View()
        {
            startMenu = new StartMenu();
        }

        public void Run()
        {
            while (isRunning)
            {
                startMenu.DisplayMenu();
                string userInput = (startMenu.GetUserInput() ?? "").Trim();
                StartMenuOption selectedOption = startMenu.GetMenuOption(userInput);

                switch (selectedOption)
                {
                    case StartMenuOption.Login:
                        var credentials = GetLoginData();
                        UserAttemptLogin?.Invoke(credentials.email, credentials.password);
                        break;

                    case StartMenuOption.SignUp:
                        Console.WriteLine("Sign up not implemented yet.");
                        break;

                    case StartMenuOption.Exit:
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        private (string email, string password) GetLoginData()
        {
            var credentials = startMenu.GetLoginData();

            string email = (credentials.email ?? "").Trim();
            string password = (credentials.password ?? "").Trim();

            return (email, password);
        }

        public void ShowLoginResult(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                Console.WriteLine("The user is logged in. Updating UI...");
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials and try again.");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        public void ShowErrorMessage(string message)
        {
            Console.WriteLine($"Input error: {message}");
            Console.WriteLine("Please correct the data and try again.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}