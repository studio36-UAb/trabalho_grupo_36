using Studio36.Utils;

namespace Studio36.Utils
{
    class FileUtils
    {
        public static void CopyRuntimeDBtoSourceDB()
        {
            // Copy runtime database back to source
            CopyFileToSource("UsersAccounts.json", "UsersDatabase");
            CopyFileToSource("Projects.json", "ProjectsAndTasksDatabase");
            CopyFileToSource("Tasks.json", "ProjectsAndTasksDatabase");
        }

        private static void CopyFileToSource(string fileName, string targetSubDir)
        {
            try
            {
                string runtimeFile = Path.Combine(AppContext.BaseDirectory, targetSubDir, fileName);
                if (!File.Exists(runtimeFile))
                {
                    // Fallback for flat structure or if it's directly in base dir
                    runtimeFile = Path.Combine(AppContext.BaseDirectory, fileName);
                }

                string sourceFile = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", targetSubDir, fileName);
                string normalizedSourceFile = Path.GetFullPath(sourceFile);

                if (File.Exists(runtimeFile) && Directory.Exists(Path.GetDirectoryName(normalizedSourceFile)))
                {
                    File.Copy(runtimeFile, normalizedSourceFile, overwrite: true);
                    Logger.Info($"{fileName} copied back to source: {normalizedSourceFile}");
                }
            }
            catch (Exception ex)
            {
                Logger.Warning($"Failed to copy {fileName} back to source: {ex.Message}");
            }
        }
    }
}
