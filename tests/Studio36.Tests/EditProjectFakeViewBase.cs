using Studio36.DTOs;
using Studio36.ViewComponent.Interfaces;

namespace Studio36.Tests;

public class EditProjectFakeViewBase : IView
{
    private Action<EditProjectRequestData>? userRequestsProjectEdition;

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
        add => userRequestsProjectEdition += value;
        remove => userRequestsProjectEdition -= value;
    }

    public event Action<int>? UserRequestsProjectDeletion
    {
        add { }
        remove { }
    }

    public event Action<int>? UserRequestsProjectReport
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

    public string? LastProjectEditionMessage { get; private set; }
    public string? LastErrorMessage { get; private set; }

    public void SubmitProjectEdition(EditProjectRequestData request)
    {
        userRequestsProjectEdition?.Invoke(request);
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
        LastProjectEditionMessage = message;
    }

    public void ShowProjectDeletionResult(string message)
    {
    }

    public void ShowReportResult(string message)
    {
    }

    public void ShowErrorMessage(string message)
    {
        LastErrorMessage = message;
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
