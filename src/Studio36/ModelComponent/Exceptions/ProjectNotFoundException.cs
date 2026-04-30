namespace Studio36.ModelComponent
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(int idProjeto)
            : base($"The project with ID {idProjeto} does not exist in the current Model state.")
        {
        }
    }
}