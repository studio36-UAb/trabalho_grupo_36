namespace Studio36.Utils
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public static class Logger
    {
        private static readonly string _logFilePath;
        private static readonly object _lockObject = new();
        private static bool _isInitialized = false;

        static Logger()
        {
            try
            {
                // Navigate from bin/Debug/net10.0 to solution root
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo? dir = new DirectoryInfo(baseDir);

                // Find solution root (contains "src" folder)
                while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "src")))
                {
                    dir = dir.Parent;
                }

                string solutionRoot = dir?.FullName ?? baseDir;

                // Create Logs directory at solution root
                string logDirectory = Path.Combine(solutionRoot, "Logs");
                Directory.CreateDirectory(logDirectory);

                // Generate unique filename with timestamp
                string logFileName = $"Studio36_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
                _logFilePath = Path.Combine(logDirectory, logFileName);

                // Write session header
                WriteSessionHeader();
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                // Fallback: log to a file in the current directory if initialization fails
                _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Studio36_fallback.log");

                try
                {
                    File.WriteAllText(_logFilePath,
                        $"[FATAL] Logger initialization failed: {ex.Message}{Environment.NewLine}");
                }
                catch
                {
                    // If even fallback fails, we can't do anything
                }
            }
        }

        private static void WriteSessionHeader()
        {
            try
            {
                string separator = new string('=', 80);
                string sessionHeader =
                    $"{separator}{Environment.NewLine}" +
                    $"SESSION STARTED: {DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}" +
                    $"Application: Studio36{Environment.NewLine}" +
                    $"Log File: {_logFilePath}{Environment.NewLine}" +
                    $"{separator}{Environment.NewLine}{Environment.NewLine}";

                File.WriteAllText(_logFilePath, sessionHeader);
            }
            catch
            {
                // Silently fail - don't crash the app during initialization
            }
        }

        public static void Log(LogLevel level, string message, Exception? exception = null)
        {
            if (!_isInitialized)
            {
                return;
            }

            lock (_lockObject)
            {
                try
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string logEntry = $"[{timestamp}] [{level,-7}] {message}";

                    if (exception != null)
                    {
                        logEntry += BuildExceptionDetails(exception);
                    }

                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
                }
                catch
                {
                    // Silently fail - don't crash the app if logging fails
                }
            }
        }

        private static string BuildExceptionDetails(Exception exception)
        {
            var details = new System.Text.StringBuilder();
            details.AppendLine();
            details.AppendLine($"    Exception Type: {exception.GetType().FullName}");
            details.AppendLine($"    Message: {exception.Message}");

            if (!string.IsNullOrEmpty(exception.StackTrace))
            {
                details.AppendLine("    Stack Trace:");
                var stackLines = exception.StackTrace.Split(Environment.NewLine);
                foreach (var line in stackLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        details.AppendLine($"      {line.Trim()}");
                    }
                }
            }

            // Log inner exceptions recursively
            if (exception.InnerException != null)
            {
                details.AppendLine("    Inner Exception:");
                details.Append(BuildExceptionDetails(exception.InnerException));
            }

            return details.ToString();
        }

        public static void Info(string message) => Log(LogLevel.Info, message);
        public static void Warning(string message) => Log(LogLevel.Warning, message);
        public static void Error(string message, Exception? exception = null)
            => Log(LogLevel.Error, message, exception);
        public static string GetLogFilePath() => _logFilePath;
        public static void EndSession()
        {
            if (!_isInitialized)
            {
                return;
            }

            lock (_lockObject)
            {
                try
                {
                    string separator = new string('=', 80);
                    string sessionFooter =
                        $"{Environment.NewLine}{separator}{Environment.NewLine}" +
                        $"SESSION ENDED: {DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}" +
                        $"{separator}{Environment.NewLine}";

                    File.AppendAllText(_logFilePath, sessionFooter);
                }
                catch
                {
                    // Silently fail
                }
            }
        }
    }
}