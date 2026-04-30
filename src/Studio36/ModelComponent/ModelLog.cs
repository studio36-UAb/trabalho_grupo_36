namespace Studio36.ModelComponent
{
    public class ModelLog
    {
        public void RegistarLog(Exception excecao, int idProjeto)
        {
            // Regista a inconsistência detetada entre o pedido recebido
            // e o estado interno atual do Model.
            Console.WriteLine($"[LOG] Inconsistência ao listar tarefas do projeto {idProjeto}: {excecao.Message}");
        }
    }
}