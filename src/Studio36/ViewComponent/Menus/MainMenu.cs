using System;

namespace Studio36.ViewComponent
{
    public class MainMenu
    {
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("\nMain Menu!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. New Project");
            Console.WriteLine("2. Back");
            Console.Write("Selection: ");
        }

        public string GetUserInput()
        {
            Console.Write("Selection: ");
            return (Console.ReadLine() ?? "").Trim();
        }
    }
}