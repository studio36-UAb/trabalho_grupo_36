namespace Studio36.ViewComponent
{
    public class View
    {
        // Start Menu Event
        public event Action<StartMenuOption>? StartMenuOptionSent;

        public View()
        {
        }

        public void RunStartMenu()
        {
            StartMenu startMenu = new StartMenu();
            startMenu.DisplayMenu();
            StartMenuOption menuOption = startMenu.GetMenuOption(startMenu.GetUserInput());
            StartMenuOptionSent?.Invoke(menuOption);
        }

        public void RunMainMenu()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.DisplayMenu();
            // StartMenuOption menuOption = mainMenu.GetMenuOption(mainMenu.GetUserInput());
        }

        public static void ShowLoginSuccess(string message)
        {
            Console.WriteLine(message + "Redirecting to main menu...");
            System.Threading.Thread.Sleep(2000);
        }

        public static void ShowLoginFailure(string message)
        {
            Console.WriteLine(message + "Returning to start menu...");
            System.Threading.Thread.Sleep(2000);
        }

        public static void ShowSignUpSuccess(string message)
        {
            Console.WriteLine(message + "Returning to start menu...");
            System.Threading.Thread.Sleep(2000);
        }

        public static void ShowSignUpFailure(string message)
        {
            Console.WriteLine(message + "Please try again...");
            System.Threading.Thread.Sleep(2000);
        }
    }
}