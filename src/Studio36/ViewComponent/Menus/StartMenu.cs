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
            Console.Clear();
            Console.WriteLine("Welcome to Studio36!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Sign up");
            Console.WriteLine("3. Exit\n");
        }

        public string GetUserInput()
        {
            Console.Write("Select an option: ");
            return Console.ReadLine() ?? "";
        }

        public (string username, string password) GetLoginData()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            return (username, password);
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
    }
}