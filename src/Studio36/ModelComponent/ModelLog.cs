namespace Studio36.ModelComponent
{
    public class ModelLog
    {
        public void RegistarLog(Exception excecao, int idProjeto)
        {
            // Regista a inconsistência detetada entre o pedido recebido
            // e o estado interno atual do Model.
            Console.WriteLine($"[LOG] Inconsistency while listing tasks for project {idProjeto}: {excecao.Message}");
        }
    }
}