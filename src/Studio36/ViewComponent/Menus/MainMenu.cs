using Studio36.Utils;

namespace Studio36.ViewComponent.Menus
{
    public class MainMenu
    {
        public void DisplayMenu()
        {
            ClearScreen();

            UIUtils.WriteLineColor("  STUDIO36 DASHBOARD", ConsoleColor.Cyan);
            UIUtils.WriteLineColor(new string('=', 20), ConsoleColor.DarkCyan);
            Console.WriteLine();

            UIUtils.WriteColor("  [1] ", ConsoleColor.Cyan); Console.WriteLine("Create New Project");
            UIUtils.WriteColor("  [2] ", ConsoleColor.Cyan); Console.WriteLine("View All Projects");
            UIUtils.WriteColor("  [3] ", ConsoleColor.Cyan); Console.WriteLine("Edit Project Details");
            UIUtils.WriteColor("  [4] ", ConsoleColor.Cyan); Console.WriteLine("Delete a Project");
            Console.WriteLine();

            UIUtils.WriteColor("  [5] ", ConsoleColor.Cyan); Console.WriteLine("List Tasks by Project");
            UIUtils.WriteColor("  [6] ", ConsoleColor.Cyan); Console.WriteLine("Add Task to Project");
            UIUtils.WriteColor("  [7] ", ConsoleColor.Cyan); Console.WriteLine("Edit Task Description");
            UIUtils.WriteColor("  [8] ", ConsoleColor.Cyan); Console.WriteLine("Remove Task from Project");
            Console.WriteLine();

            UIUtils.WriteColor("  [9] ", ConsoleColor.Cyan); Console.WriteLine("Export PDF Report");
            Console.WriteLine();

            UIUtils.WriteLineColor(new string('-', 30), ConsoleColor.DarkGray);
            UIUtils.WriteColor(" [10] ", ConsoleColor.Cyan); UIUtils.WriteLineColor("Logout & Back to Start", ConsoleColor.DarkGray);
            Console.WriteLine();
        }

        public string GetUserInput()
        {
            UIUtils.WriteColor("  Action > ", ConsoleColor.Cyan);
            string? input = Console.ReadLine();

            if (input == null)
            {
                return "10";
            }

            return input.Trim();
        }

        private void ClearScreen()
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
