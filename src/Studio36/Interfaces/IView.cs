using Studio36.DTOs;

namespace Studio36.Interfaces
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
        event Action<int, string>? UserRequestsTaskAddition;
        event Action<int, int, string>? UserRequestsTaskEdition;
        event Action<int, int>? UserRequestsTaskDeletion;

        void Run();
        void ShowLoginResult(bool isLoggedIn, string message);
        void ShowSignUpResult(string message);
        void ShowProjectCreationResult(string message);
        void ShowProjectEditionResult(string message);
        void ShowProjectDeletionResult(string message);
        void ShowReportResult(string message);
        void ShowTaskOperationResult(TaskOperationResultData result);
        void ShowErrorMessage(string message);
        void RefreshProjectList(List<string> projectList);
        void ShowProjectList(List<string> projectList);
        void ShowTaskList(List<string> tasks);
    }
}
