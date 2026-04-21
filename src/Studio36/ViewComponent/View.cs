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
            // To Do: StartMenuOption menuOption = mainMenu.GetMenuOption(mainMenu.GetUserInput(), ref shouldRun);
        }

        public void ShowLoginSuccess()
        {
            Console.WriteLine("Login successful! Redirecting to main menu...");
            System.Threading.Thread.Sleep(2000);
        }
    }
}