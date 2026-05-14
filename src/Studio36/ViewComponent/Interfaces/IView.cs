using Studio36.DTOs;

namespace Studio36.ViewComponent.Interfaces
{
    public interface IView
    {
        event Action<LoginRequestData>? UserAttemptLogin;
        event Action<SignUpRequestData>? UserAttemptSignUp;
        event Action<CreateProjectRequestData>? UserRequestsProjectCreation;
        event Action<EditProjectRequestData>? UserRequestsProjectEdition;
        event Action<int>? UserRequestsProjectDeletion;
        event Action<int>? UserRequestsProjectReport;
        event Action? UserRequestsProjectList;
        event Action<int>? UserRequestsProjectTasks;

        void Run();
        void ShowLoginResult(bool isLoggedIn, string message);
        void ShowSignUpResult(string message);
        void ShowProjectCreationResult(string message);
        void ShowProjectEditionResult(string message);
        void ShowProjectDeletionResult(string message);
        void ShowReportResult(string message);
        void ShowErrorMessage(string message);
        void RefreshProjectList(List<string> listaProjetos);
        void ShowProjectList(List<string> listaProjetos);
        void ShowTaskList(List<string> tarefas);
    }
}
