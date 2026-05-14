using Studio36.ControllerComponent;
using Studio36.DTOs;
using Studio36.ModelComponent;
using Studio36.ViewComponent.Interfaces;

namespace Studio36.Tests;

public static class T34_FluxoListagemProjetosMvc
{
    public static void Run()
    {
        Model model = new();
        FakeView view = new();

        _ = new Controller(model, view);

        view.SubmitProjectListRequest();

        List<string>? lastProjectList = view.LastProjectList;

        if (lastProjectList == null)
        {
            throw new InvalidOperationException("The View should receive the project list through the Controller.");
        }

        TestHelper.AssertTrue(
            lastProjectList.Contains("1 - Projeto de demonstração"),
            "The View should receive the projects returned by the Model.");
    }

    private sealed class FakeView : IView
    {
        private Action? userRequestsProjectList;

        public event Action<LoginRequestData>? UserAttemptLogin
        {
            add { }
            remove { }
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
            add => userRequestsProjectList += value;
            remove => userRequestsProjectList -= value;
        }

        public event Action<int>? UserRequestsProjectTasks
        {
            add { }
            remove { }
        }

        public List<string>? LastProjectList { get; private set; }

        public void SubmitProjectListRequest()
        {
            userRequestsProjectList?.Invoke();
        }

        public void Run()
        {
        }

        public void ShowLoginResult(bool isLoggedIn, string message)
        {
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
            LastProjectList = listaProjetos;
        }

        public void ShowTaskList(List<string> tarefas)
        {
        }
    }
}
