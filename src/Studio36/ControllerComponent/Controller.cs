using Studio36.DTOs;
using Studio36.ModelComponent;
using Studio36.Interfaces;
using Studio36.ReportComponent.Interfaces;

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

            view.UserRequestsProjectCreation += ProcessProjectCreationRequest;
            view.UserRequestsProjectList += ProcessProjectListRequest;
            view.UserRequestsProjectTasks += ProcessProjectTasksRequest;
            view.UserRequestsProjectEdition += ProcessProjectEditionRequest;
            view.UserRequestsProjectDeletion += ProcessProjectDeletionRequest;
            view.UserRequestsProjectReport += ProcessProjectReportRequest;

            view.UserRequestsTaskAddition += ProcessTaskAdditionRequest;
            view.UserRequestsTaskEdition += ProcessTaskEditionRequest;
            view.UserRequestsTaskDeletion += ProcessTaskDeletionRequest;
            
            model.SendLoginState += OnLoginStateReceived;
            model.SendSignUpState += OnSignUpStateReceived;
            model.SendProjectCreationState += OnProjectCreationStateReceived;
            model.SendProjectEditionState += OnProjectEditionStateReceived;
            model.SendProjectDeletionState += OnProjectDeletionStateReceived;
            model.SendTaskOperationState += OnTaskOperationStateReceived;

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

        private void OnTaskOperationStateReceived(TaskOperationResultData result)
        {
            view.ShowTaskOperationResult(result);
        }

        private void ProcessProjectCreationRequest(CreateProjectRequestData request)
        {
            CreateProject(request.Name, request.Description, request.StartDate, request.EndDate);
        }

        private void ProcessProjectEditionRequest(EditProjectRequestData request)
        {
            EditProject(request.ProjectId, request.Name, request.Description, request.StartDate, request.EndDate);
        }

        private void ProcessProjectDeletionRequest(int projectId)
        {
            DeleteProject(projectId);
        }

        private void ProcessProjectReportRequest(int projectId)
        {
            GenerateReport(projectId);
        }

        private void ProcessTaskAdditionRequest(int projectId, string description)
        {
            AddTask(projectId, description);
        }

        private void ProcessTaskEditionRequest(int projectId, int taskId, string description)
        {
            EditTask(projectId, taskId, description);
        }

        private void ProcessTaskDeletionRequest(int projectId, int taskId)
        {
            DeleteTask(projectId, taskId);
        }

        private void ProcessProjectTasksRequest(int projectId)
        {
            ListTasks(projectId);
        }

        private void ProcessProjectListRequest()
        {
            ListProjects();
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

        // AUTHENTICATION
        public bool Login(string email, string password)
        {
            return false;
        }

        public bool Register(string email, string password)
        {
            return false;
        }

        // PROJECTS
        public void CreateProject(string name, string description, DateTime startDate, DateTime endDate)
        {
            try
            {
                model.CreateProject(new CreateProjectRequestData(name, description, startDate, endDate));
            }
            catch (ArgumentException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while creating project.");
            }
        }

        public List<string> ListProjects()
        {
            try
            {
                List<string> projects = model.GetProjects();
                view.ShowProjectList(projects);
                return projects;
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while listing projects.");
                return new List<string>();
            }
        }

        public void EditProject(int projectId, string name, string description)
        {
            EditProject(projectId, name, description, DateTime.Today, DateTime.Today);
        }

        public void EditProject(int projectId, string name, string description, DateTime startDate, DateTime endDate)
        {
            try
            {
                model.EditProject(new EditProjectRequestData(projectId, name, description, startDate, endDate));
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.LogRegistry(ex, projectId);
                view.ShowErrorMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while editing project.");
            }
        }

        public void DeleteProject(int projectId)
        {
            try
            {
                model.DeleteProject(projectId);
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.LogRegistry(ex, projectId);
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while deleting project.");
            }
        }

        // TASKS
        public void AddTask(int projectId, string description)
        {
            try
            {
                model.AddTask(projectId, description);
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.LogRegistry(ex, projectId);
                view.ShowErrorMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while adding task.");
            }
        }

        public List<string> ListTasks(int projectId)
        {
            try
            {
                List<string> tasks = model.GetTasksByProject(projectId);
                view.ShowTaskList(tasks);
                return tasks;
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.LogRegistry(ex, projectId);

                List<string> projectList = model.GetProjects();

                view.ShowErrorMessage(ex.Message);
                view.RefreshProjectList(projectList);

                return new List<string>();
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while listing project tasks.");
                return new List<string>();
            }
        }

        public void EditTask(int projectId, int taskId, string description)
        {
            try
            {
                model.EditTask(projectId, taskId, description);
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.LogRegistry(ex, projectId);
                view.ShowErrorMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while editing task.");
            }
        }

        public void DeleteTask(int projectId, int taskId)
        {
            try
            {
                model.DeleteTask(projectId, taskId);
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.LogRegistry(ex, projectId);
                view.ShowErrorMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while deleting task.");
            }
        }

        public void GenerateReport(int projectId)
        {
            try
            {
                ProjectReportData reportData = model.GetProjectReportData(projectId);
                ReportResultData result = reportGenerator.GenerateProjectReport(reportData);

                view.ShowReportResult(result.Message);
            }
            catch (ProjectNotFoundException ex)
            {
                modelLog.LogRegistry(ex, projectId);
                view.ShowErrorMessage(ex.Message);
            }
            catch (Exception)
            {
                view.ShowErrorMessage("Unexpected error while generating report.");
            }
        }
    }
}
