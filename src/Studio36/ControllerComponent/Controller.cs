using Studio36.DTOs;
using Studio36.ModelComponent;
using Studio36.ViewComponent;

namespace Studio36.ControllerComponent
{
    public class Controller
    {
        readonly Model model;
        readonly View view;
        readonly ModelLog modelLog;

        public Controller()
        {
            model = new Model();
            view = new View();
            modelLog = new ModelLog();

            view.UserAttemptLogin += ProcessLogin;
            view.UserAttemptSignUp += ProcessSignUp;

            view.UserRequestsProjectTasks += ProcessProjectTasksRequest;

            model.SendLoginState += OnLoginStateReceived;
            model.SendSignUpState += OnSignUpStateReceived;

        }

        private void OnLoginStateReceived(LoginResultData result)
        {
            view.ShowLoginResult(result.IsSuccessful, result.Message);
        }

        private void OnSignUpStateReceived(SignUpResultData result)
        {
            view.ShowSignUpResult(result.Message);
        }

        private void ProcessProjectTasksRequest(int idProjeto)
        {
            ListarTarefas(idProjeto);
        }

        public void StartProgram()
        {
            view.Run();
        }

        public void ProcessLogin(LoginRequestData request)
        {
            try
            {
                model.AreCredentialsValid(request);
            }
            catch (InvalidLoginInputException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while processing login.");
            }
        }

        public void ProcessSignUp(SignUpRequestData request)
        {
            try
            {
                model.RegisterUser(request);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while processing sign up.");
            }
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
            try
            {
                List<string> tarefas = model.GetTasksByProject(idProjeto);
                view.ShowTaskList(tarefas);
                return tarefas;
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.RegistarLog(ex, idProjeto);

                List<string> listaProjetos = model.GetProjects();

                view.ShowErrorMessage(ex.Message);
                view.RefreshProjectList(listaProjetos);

                return new List<string>();
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Erro inesperado ao listar tarefas do projeto.");
                return new List<string>();
            }
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