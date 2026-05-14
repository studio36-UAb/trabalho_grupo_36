using Studio36.DTOs;
using Studio36.ModelComponent;
using Studio36.ModelComponent.Interfaces;
using Studio36.ReportComponent.Interfaces;
using Studio36.ViewComponent.Interfaces;

namespace Studio36.ControllerComponent
{
    public class Controller
    {
        readonly IModel model;
        readonly IView view;
        readonly IReportGenerator reportGenerator;
        readonly ModelLog modelLog;

        public Controller(IModel model, IView view, IReportGenerator reportGenerator)
        {
            this.model = model;
            this.view = view;
            this.reportGenerator = reportGenerator;
            modelLog = new ModelLog();

            view.UserAttemptLogin += ProcessLogin;
            view.UserAttemptSignUp += ProcessSignUp;

            view.UserRequestsProjectTasks += ProcessProjectTasksRequest;
            view.UserRequestsProjectCreation += ProcessProjectCreationRequest;
            view.UserRequestsProjectEdition += ProcessProjectEditionRequest;
            view.UserRequestsProjectDeletion += ProcessProjectDeletionRequest;
            view.UserRequestsProjectReport += ProcessProjectReportRequest;
            view.UserRequestsProjectList += ProcessProjectListRequest;

            model.SendLoginState += OnLoginStateReceived;
            model.SendSignUpState += OnSignUpStateReceived;
            model.SendProjectCreationState += OnProjectCreationStateReceived;
            model.SendProjectEditionState += OnProjectEditionStateReceived;
            model.SendProjectDeletionState += OnProjectDeletionStateReceived;

        }

        private void OnLoginStateReceived(LoginResultData result)
        {
            view.ShowLoginResult(result.IsSuccessful, result.Message);
        }

        private void OnSignUpStateReceived(SignUpResultData result)
        {
            view.ShowSignUpResult(result.Message);
        }

        private void OnProjectCreationStateReceived(CreateProjectResultData result)
        {
            view.ShowProjectCreationResult(result.Message);
        }

        private void OnProjectEditionStateReceived(EditProjectResultData result)
        {
            view.ShowProjectEditionResult(result.Message);
        }

        private void OnProjectDeletionStateReceived(DeleteProjectResultData result)
        {
            view.ShowProjectDeletionResult(result.Message);
        }

        private void ProcessProjectCreationRequest(CreateProjectRequestData request)
        {
            CriarProjeto(request.Nome, request.Descricao, request.DataInicio, request.DataFim);
        }

        private void ProcessProjectEditionRequest(EditProjectRequestData request)
        {
            EditarProjeto(request.IdProjeto, request.Nome, request.Descricao, request.DataInicio, request.DataFim);
        }

        private void ProcessProjectDeletionRequest(int idProjeto)
        {
            EliminarProjeto(idProjeto);
        }

        private void ProcessProjectReportRequest(int idProjeto)
        {
            GerarRelatorio(idProjeto);
        }

        private void ProcessProjectTasksRequest(int idProjeto)
        {
            ListarTarefas(idProjeto);
        }

        private void ProcessProjectListRequest()
        {
            ListarProjetos();
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
            try
            {
                model.CreateProject(new CreateProjectRequestData(nome, descricao, dataInicio, dataFim));
            }
            catch (ArgumentException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Erro inesperado ao criar projeto.");
            }
        }

        public List<string> ListarProjetos()
        {
            try
            {
                List<string> projetos = model.GetProjects();
                view.ShowProjectList(projetos);
                return projetos;
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Erro inesperado ao listar projetos.");
                return new List<string>();
            }
        }

        public void EditarProjeto(int idProjeto, string nome, string descricao)
        {
            EditarProjeto(idProjeto, nome, descricao, DateTime.Today, DateTime.Today);
        }

        public void EditarProjeto(int idProjeto, string nome, string descricao, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                model.EditProject(new EditProjectRequestData(idProjeto, nome, descricao, dataInicio, dataFim));
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.RegistarLog(ex, idProjeto);
                view.ShowErrorMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Erro inesperado ao editar projeto.");
            }
        }

        public void EliminarProjeto(int idProjeto)
        {
            try
            {
                model.DeleteProject(idProjeto);
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.RegistarLog(ex, idProjeto);
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Erro inesperado ao eliminar projeto.");
            }
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
            try
            {
                ProjectReportData reportData = model.GetProjectReportData(idProjeto);
                ReportResultData result = reportGenerator.GenerateProjectReport(reportData);

                view.ShowReportResult(result.Message);
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.RegistarLog(ex, idProjeto);
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Erro inesperado ao gerar relatório.");
            }
        }
    }
}
