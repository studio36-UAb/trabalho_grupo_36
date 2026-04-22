using Studio36.Utils;

namespace Studio36.Utils
{
    class FileUtils
    {
        public static void CopyRuntimeDBtoSourceDB()
        {
            // Copy runtime database back to source
            try
            {
                string runtimeDb = Path.Combine(AppContext.BaseDirectory, "UsersAccounts.json");
                string sourceDb = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "UsersDatabase", "UsersAccounts.json");
                string normalizedSourceDb = Path.GetFullPath(sourceDb);

                if (File.Exists(runtimeDb) && Directory.Exists(Path.GetDirectoryName(normalizedSourceDb)))
                {
                    File.Copy(runtimeDb, normalizedSourceDb, overwrite: true);
                    Logger.Info($"Database copied back to source: {normalizedSourceDb}");
                }
            }
            catch (Exception ex)
            {
                Logger.Warning($"Failed to copy database back to source: {ex.Message}");
            }
        }
    }
}
