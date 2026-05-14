using Studio36.ControllerComponent;
using Studio36.DTOs;
using Studio36.ModelComponent.Interfaces;
using Studio36.ViewComponent.Interfaces;

namespace Studio36.Tests;

public static class T32_ControllerFuncionaComFakes
{
    public static void Run()
    {
        FakeModel model = new();
        FakeView view = new();

        _ = new Controller(model, view);

        view.SubmitLogin(new LoginRequestData("teste@studio36.pt", "1234"));

        LoginRequestData? lastLoginRequest = model.LastLoginRequest;

        if (lastLoginRequest == null)
        {
            throw new InvalidOperationException("The Controller should forward login requests to IModel.");
        }

        TestHelper.AssertTrue(lastLoginRequest.Email == "teste@studio36.pt", "The login email should be passed to IModel.");
        TestHelper.AssertTrue(lastLoginRequest.Password == "1234", "The login password should be passed to IModel.");

        model.EmitLoginResult(new LoginResultData(true, "Login via fake model."));

        TestHelper.AssertTrue(view.LastLoginResult == true, "The Controller should forward login result state to IView.");
        TestHelper.AssertTrue(view.LastLoginMessage == "Login via fake model.", "The Controller should forward login result message to IView.");
    }

    private sealed class FakeModel : IModel
    {
        private Action<LoginResultData>? sendLoginState;

        public event Action<LoginResultData>? SendLoginState
        {
            add => sendLoginState += value;
            remove => sendLoginState -= value;
        }

        public event Action<SignUpResultData>? SendSignUpState
        {
            add { }
            remove { }
        }

        public event Action<CreateProjectResultData>? SendProjectCreationState
        {
            add { }
            remove { }
        }

        public event Action<EditProjectResultData>? SendProjectEditionState
        {
            add { }
            remove { }
        }

        public LoginRequestData? LastLoginRequest { get; private set; }

        public void AreCredentialsValid(LoginRequestData request)
        {
            LastLoginRequest = request;
        }

        public void EmitLoginResult(LoginResultData result)
        {
            sendLoginState?.Invoke(result);
        }

        public void RegisterUser(SignUpRequestData request)
        {
        }

        public void CreateProject(CreateProjectRequestData request)
        {
        }

        public void EditProject(EditProjectRequestData request)
        {
        }

        public List<string> GetProjects()
        {
            return new List<string>();
        }

        public List<string> GetTasksByProject(int idProjeto)
        {
            return new List<string>();
        }
    }

    private sealed class FakeView : IView
    {
        private Action<LoginRequestData>? userAttemptLogin;

        public event Action<LoginRequestData>? UserAttemptLogin
        {
            add => userAttemptLogin += value;
            remove => userAttemptLogin -= value;
        }

        public event Action<SignUpRequestData>? UserAttemptSignUp
        {
            add { }
            remove { }
        }

        public event Action<CreateProjectRequestData>? UserRequestsProjectCreation
        {
            add { }
            remove { }
        }

        public event Action<EditProjectRequestData>? UserRequestsProjectEdition
        {
            add { }
            remove { }
        }

        public event Action? UserRequestsProjectList
        {
            add { }
            remove { }
        }

        public event Action<int>? UserRequestsProjectTasks
        {
            add { }
            remove { }
        }

        public bool? LastLoginResult { get; private set; }
        public string? LastLoginMessage { get; private set; }

        public void SubmitLogin(LoginRequestData request)
        {
            userAttemptLogin?.Invoke(request);
        }

        public void Run()
        {
        }

        public void ShowLoginResult(bool isLoggedIn, string message)
        {
            LastLoginResult = isLoggedIn;
            LastLoginMessage = message;
        }

        public void ShowSignUpResult(string message)
        {
        }

        public void ShowProjectCreationResult(string message)
        {
        }

        public void ShowProjectEditionResult(string message)
        {
        }

        public void ShowErrorMessage(string message)
        {
        }

        public void RefreshProjectList(List<string> listaProjetos)
        {
        }

        public void ShowProjectList(List<string> listaProjetos)
        {
        }

        public void ShowTaskList(List<string> tarefas)
        {
        }
    }
}
