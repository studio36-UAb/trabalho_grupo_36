using System;

namespace Studio36.ViewComponent
{
    public class MainMenu
    {
        public void DisplayMenu()
        {
            ClearScreen();
            Console.WriteLine("\nMain Menu!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. New Project");
            Console.WriteLine("2. List projects");
            Console.WriteLine("3. Edit project");
            Console.WriteLine("4. List tasks by project");
            Console.WriteLine("5. Delete project");
            Console.WriteLine("6. Generate report");
            Console.WriteLine("7. Back");
            Console.Write("Selection: ");
        }

        public string GetUserInput()
        {
            Console.Write("Selection: ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                return "7";
            }

            return input.Trim();
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
