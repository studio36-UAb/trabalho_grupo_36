using System;

namespace Studio36.Utils
{
    public static class UIUtils
    {
        public static void WriteColor(string text, ConsoleColor color)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = original;
        }

        public static void WriteLineColor(string text, ConsoleColor color)
        {
            WriteColor(text + Environment.NewLine, color);
        }

        public static void Success(string message) => WriteLineColor(message, ConsoleColor.Green);
        public static void Warning(string message) => WriteLineColor(message, ConsoleColor.Yellow);
        public static void Error(string message) => WriteLineColor(message, ConsoleColor.Red);
        public static void Info(string message) => WriteLineColor(message, ConsoleColor.Cyan);
        public static void Highlight(string message) => WriteLineColor(message, ConsoleColor.Magenta);

        public static void PrintHeader(string title, string? subtitle = null)
        {
            string mainTitle = $" {title.ToUpper()} ";
            int width = Math.Max(mainTitle.Length, (subtitle?.Length ?? 0)) + 4;
            string line = new string('═', width);

            WriteLineColor($"╔{line}╗", ConsoleColor.DarkCyan);
            
            // Title Line
            WriteColor("║", ConsoleColor.DarkCyan);
            string centeredTitle = mainTitle.PadLeft((width + mainTitle.Length) / 2).PadRight(width);
            WriteColor(centeredTitle, ConsoleColor.White);
            WriteLineColor("║", ConsoleColor.DarkCyan);

            // Subtitle Line
            if (!string.IsNullOrEmpty(subtitle))
            {
                WriteColor("║", ConsoleColor.DarkCyan);
                string centeredSubtitle = subtitle.PadLeft((width + subtitle.Length) / 2).PadRight(width);
                WriteColor(centeredSubtitle, ConsoleColor.Gray);
                WriteLineColor("║", ConsoleColor.DarkCyan);
            }

            WriteLineColor($"╚{line}╝", ConsoleColor.DarkCyan);
        }
    }
}
