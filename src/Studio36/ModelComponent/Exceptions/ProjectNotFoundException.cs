namespace Studio36.ModelComponent
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(int idProjeto)
            : base($"O projeto com o ID {idProjeto} não existe no estado atual do Model.")
        {
        }
    }
}