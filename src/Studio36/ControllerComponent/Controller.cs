using Studio36.ModelComponent;
using Studio36.ViewComponent;

namespace Studio36.ControllerComponent
{
    public class Controller
    {
        readonly Model model;
        readonly View view;

        public Controller()
        {
            model = new Model();
            view = new View(model);

            view.UserAttemptLogin += ProcessLogin;
        }

        public void StartProgram()
        {
            view.Run();
        }

        public void ProcessLogin(string username, string password)
        {
            model.AreCredentialsValid(username, password);
        }

        // AUTENTICAÇÃO
        public bool Login(string email, string password)
        {
            return false;
        }

        public bool Registar(string email, string password)
        {
            return false;
        }

        // PROJETOS
        public void CriarProjeto(string nome, string descricao, DateTime dataInicio, DateTime dataFim)
        {
        }

        public List<string> ListarProjetos()
        {
            return new List<string>();
        }

        public void EditarProjeto(int idProjeto, string nome, string descricao)
        {
        }

        public void EliminarProjeto(int idProjeto)
        {
        }

        // TAREFAS
        public void CriarTarefa(int idProjeto, string nome, string descricao, DateTime prazo, string prioridade)
        {
        }

        public List<string> ListarTarefas(int idProjeto)
        {
            return new List<string>();
        }

        public void EditarTarefa(int idTarefa, string nome, string descricao)
        {
        }

        public void EliminarTarefa(int idTarefa)
        {
        }

        // MEMBROS
        public void AdicionarMembro(int idProjeto, string nome, string funcao)
        {
        }

        public List<string> ListarMembros(int idProjeto)
        {
            return new List<string>();
        }

        // RELATÓRIOS
        public void GerarRelatorio(int idProjeto)
        {
        }
    }
}