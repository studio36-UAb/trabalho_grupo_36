using Studio36.ControllerComponent;
using Studio36.DTOs;
using Studio36.ModelComponent;
using Studio36.ViewComponent.Interfaces;

namespace Studio36.Tests;

public static class T33_FluxoCriacaoProjetoMvc
{
    public static void Run()
    {
        Model model = new();
        FakeView view = new();

        _ = new Controller(model, view);

        CreateProjectRequestData request = new(
            "Projeto MVC",
            "Validar fluxo completo entre View, Controller e Model",
            new DateTime(2026, 5, 14),
            new DateTime(2026, 5, 20));

        view.SubmitProjectCreation(request);

        List<string> projects = model.GetProjects();

        TestHelper.AssertTrue(
            projects.Contains("2 - Projeto MVC"),
            "The Model should store the project requested by the View through the Controller.");

        TestHelper.AssertTrue(
            view.LastProjectCreationMessage == "Project created successfully with ID 2.",
            "The View should receive the project creation result message through the Controller.");
    }

    private sealed class FakeView : IView
    {
        private Action<CreateProjectRequestData>? userRequestsProjectCreation;

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
            add => userRequestsProjectCreation += value;
            remove => userRequestsProjectCreation -= value;
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

        public string? LastProjectCreationMessage { get; private set; }

        public void SubmitProjectCreation(CreateProjectRequestData request)
        {
            userRequestsProjectCreation?.Invoke(request);
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
            LastProjectCreationMessage = message;
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
