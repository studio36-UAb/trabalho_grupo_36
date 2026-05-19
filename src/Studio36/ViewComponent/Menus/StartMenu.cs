using Studio36.Utils;

using Studio36.ViewComponent.Menus.Enums;

namespace Studio36.ViewComponent.Menus
{
    public class StartMenu
    {
        public void DisplayMenu()
        {
            ClearScreen();
            
            UIUtils.WriteLineColor(@"
  ____  _             _ _       _____  __   __
 / ___|| |_ _   _  __| (_) ___ |___ / / /_  \ \
 \___ \| __| | | |/ _` | |/ _ \  |_ \| '_ \  | |
  ___) | |_| |_| | (_| | | (_) |___) | (_) | | |
 |____/ \__|\__,_|\__,_|_|\___/|____/ \___/  |_|", ConsoleColor.Cyan);

            UIUtils.WriteLineColor("  Project Management", ConsoleColor.DarkCyan);
            Console.WriteLine();

            UIUtils.PrintHeader("Welcome to Studio36");
            
            Console.WriteLine();
            UIUtils.WriteColor("  [1] ", ConsoleColor.Cyan); Console.WriteLine("Log in to your account");
            UIUtils.WriteColor("  [2] ", ConsoleColor.Cyan); Console.WriteLine("Create a new account");
            UIUtils.WriteColor("  [3] ", ConsoleColor.Cyan); UIUtils.WriteLineColor("Exit application", ConsoleColor.Red);
            Console.WriteLine();
            
            UIUtils.WriteLineColor(new string('-', 30), ConsoleColor.DarkGray);
        }

        public string GetUserInput()
        {
            UIUtils.WriteColor("  Selection > ", ConsoleColor.Cyan);
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
            Console.WriteLine();
            UIUtils.Info("--- LOGIN ---");

            UIUtils.WriteColor("  Username: ", ConsoleColor.White);
            string email = (Console.ReadLine() ?? "").Trim();

            UIUtils.WriteColor("  Password: ", ConsoleColor.White);
            string password = (Console.ReadLine() ?? "").Trim();

            return (email, password);
        }

        public (string email, string password) GetSignUpData()
        {
            Console.WriteLine();
            UIUtils.Info("--- SIGN UP ---");

            UIUtils.WriteColor("  New Username: ", ConsoleColor.White);
            string email = (Console.ReadLine() ?? "").Trim();

            UIUtils.WriteColor("  New Password: ", ConsoleColor.White);
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
                // Ignore
            }
        }
    }
}
