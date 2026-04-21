namespace Studio36.ViewComponent
{
    /*public enum StartMenuOption
    {
        Login,
        SignUp,
        Exit,
        NotValid
    }*/
    public class MainMenu
    {
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\nMain Menu!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. New Project");
            Console.Write("Selection: ");
        }

        public string GetUserInput()
        {
            Console.Write("Select an option: ");
            return Console.ReadLine() ?? "";
        }
    }
}