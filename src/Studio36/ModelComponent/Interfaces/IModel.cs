using Studio36.DTOs;

namespace Studio36.ModelComponent.Interfaces
{
    public interface IModel
    {
        event Action<LoginResultData>? SendLoginState;
        event Action<SignUpResultData>? SendSignUpState;
        event Action<CreateProjectResultData>? SendProjectCreationState;
        event Action<EditProjectResultData>? SendProjectEditionState;
        event Action<DeleteProjectResultData>? SendProjectDeletionState;

        void AreCredentialsValid(LoginRequestData request);
        void RegisterUser(SignUpRequestData request);
        void CreateProject(CreateProjectRequestData request);
        void EditProject(EditProjectRequestData request);
        void DeleteProject(int idProjeto);
        ProjectReportData GetProjectReportData(int idProjeto);
        List<string> GetProjects();
        List<string> GetTasksByProject(int idProjeto);
    }
}
