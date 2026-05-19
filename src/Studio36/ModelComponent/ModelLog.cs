using Studio36.Utils;

namespace Studio36.ModelComponent
{
    public class ModelLog
    {
        public void LogRegistry(Exception exception, int projectId)
        {
            // Records the inconsistency detected between the request received
            // and the current internal state of the Model.
            Logger.Log(LogLevel.Error, $"Inconsistency while listing tasks for project {projectId}: {exception.Message}");
        }
    }
}