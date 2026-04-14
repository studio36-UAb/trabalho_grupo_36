using Studio36.ModelComponent;
using Studio36.ViewComponent.Menus;

namespace Studio36.ViewComponent
{
    public class View
    {
        private readonly Model model;
        private bool isRunning = true;


        // Events
        public event Action<string, string>? UserAttemptLogin;

        public View(Model model)
        {
            this.model = model;
            model.SendLoginState += OnLoginChanged;
        }

        public void Run()
        {
            while (isRunning)
            {
                Menu.ShowPublicMenu();
                string userInput = Console.ReadLine() ?? "";

                switch (userInput)
                {
                    case "1":
                        var credentials = GetLoginData();
                        UserAttemptLogin?.Invoke(credentials.email, credentials.password);
                        break;
                    case "2":
                        Console.WriteLine("Sign up not implemented yet.");
                        break;
                    case "3":
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
            Console.WriteLine("Please enter your email:");
            string email = Console.ReadLine() ?? "";

            Console.WriteLine("Please enter your password:");
            string password = Console.ReadLine() ?? "";

            return (email, password);
        }
        private void OnLoginChanged()
        {
            if (model.IsLoggedIn)
            {
                Console.WriteLine("The user is logged in. Updating UI...");
            }
            else
            {
                Console.WriteLine("Login failed.");
            }
        }



    }
}
