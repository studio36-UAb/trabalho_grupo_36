using Studio36.DTOs;

namespace Studio36.Interfaces
{
    public interface IModel
    {
        event Action<LoginResultData>? SendLoginState;
        event Action<SignUpResultData>? SendSignUpState;
        event Action<CreateProjectResultData>? SendProjectCreationState;
        event Action<EditProjectResultData>? SendProjectEditionState;
        event Action<DeleteProjectResultData>? SendProjectDeletionState;
        event Action<TaskOperationResultData>? SendTaskOperationState;

        void AreCredentialsValid(LoginRequestData request);
        void RegisterUser(SignUpRequestData request);
        void CreateProject(CreateProjectRequestData request);
        void EditProject(EditProjectRequestData request);
        void DeleteProject(int projectId);

        void AddTask(int projectId, string taskDescription);
        void EditTask(int projectId, int taskIndex, string newDescription);
        void DeleteTask(int projectId, int taskIndex);

        ProjectReportData GetProjectReportData(int projectId);

        List<string> GetProjects();
        List<string> GetTasksByProject(int projectId);
    }
}
