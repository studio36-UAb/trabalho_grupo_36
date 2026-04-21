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
            Console.WriteLine("\nWelcome to Studio36!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Sign up");
            Console.WriteLine("3. Exit");
            Console.Write("Selection: ");
        }

        public string GetUserInput()
        {
            Console.Write("Select an option: ");
            return Console.ReadLine() ?? "";
        }

        public (string email, string password) GetLoginData()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            return (email, password);
        }

        public StartMenuOption GetMenuOption(string menuOption)
        {
            switch (menuOption)
            {
                case "1":
                    return StartMenuOption.Login;
                case "2":
                    return StartMenuOption.SignUp;
                case "3":
                    return StartMenuOption.Exit;
                default:
                    return StartMenuOption.NotValid;
            }
        }
    }
}