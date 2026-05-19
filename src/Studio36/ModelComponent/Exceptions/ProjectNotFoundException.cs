namespace Studio36.ModelComponent
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(int projectId)
            : base($"The project with ID {projectId} does not exist in the current Model state.")
        {
        }
    }
}