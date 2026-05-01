using System;

namespace Studio36.ViewComponent
{
    public enum StartMenuOption
    {
        Login,
        SignUp,
        Exit,
        NotValid
    }

    public class StartMenu
    {
        public void DisplayMenu()
        {
            ClearScreen();
            Console.WriteLine("Welcome to Studio36!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Sign up");
            Console.WriteLine("3. Exit\n");
        }

        public string GetUserInput()
        {
            Console.Write("Selection: ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                return "3";
            }

            return input.Trim();
        }

        public StartMenuOption GetMenuOption(string menuOption)
        {
            return menuOption switch
            {
                "1" => StartMenuOption.Login,
                "2" => StartMenuOption.SignUp,
                "3" => StartMenuOption.Exit,
                _ => StartMenuOption.NotValid
            };
        }

        public (string email, string password) GetLoginData()
        {
            Console.Write("Please enter your username: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Console.Write("Please enter your password: ");
            string password = (Console.ReadLine() ?? "").Trim();

            return (email, password);
        }

        public (string email, string password) GetSignUpData()
        {
            Console.Write("Please enter your username: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Console.Write("Please enter your password: ");
            string password = (Console.ReadLine() ?? "").Trim();

            return (email, password);
        }

        private static void ClearScreen()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
            }
        }
    }
}
