using Studio36.ControllerComponent;

using Studio36.DTOs;
using Studio36.Interfaces;
using Studio36.Utils;

using Studio36.ViewComponent.Menus;
using Studio36.ViewComponent.Menus.Enums;

using System.Globalization;

namespace Studio36.ViewComponent
{
    public class View : IView
    {
        private bool isRunning = true;

        private readonly StartMenu startMenu;
        private readonly MainMenu mainMenu;

        public MenuState CurrentState { get; set; } = MenuState.StartMenu;

        public event Action<LoginRequestData>? UserAttemptLogin;
        public event Action<SignUpRequestData>? UserAttemptSignUp;

        public event Action<CreateProjectRequestData>? UserRequestsProjectCreation;
        public event Action? UserRequestsProjectList;
        public event Action<EditProjectRequestData>? UserRequestsProjectEdition;
        public event Action<int>? UserRequestsProjectTasks;
        public event Action<int>? UserRequestsProjectDeletion;
        public event Action<int>? UserRequestsProjectReport;
        public event Action<int, string>? UserRequestsTaskAddition;
        public event Action<int, int, string>? UserRequestsTaskEdition;
        public event Action<int, int>? UserRequestsTaskDeletion;

        public View()
        {
            startMenu = new StartMenu();
            mainMenu = new MainMenu();
        }

        public void Run()
        {
            while (isRunning)
            {
                switch (CurrentState)
                {
                    case MenuState.StartMenu:
                        RunStartMenu();
                        break;

                    case MenuState.MainMenu:
                        RunMainMenu();
                        break;

                    case MenuState.Exit:
                        isRunning = false;
                        UIUtils.Info("Goodbye!");
                        break;
                }
            }
        }

        private void RunStartMenu()
        {
            startMenu.DisplayMenu();
            string userInput = (startMenu.GetUserInput() ?? "").Trim();
            StartMenuOption selectedOption = startMenu.GetMenuOption(userInput);

            switch (selectedOption)
            {
                case StartMenuOption.Login:
                    var loginCredentials = startMenu.GetLoginData();
                    UserAttemptLogin?.Invoke(new LoginRequestData(loginCredentials.email, loginCredentials.password));
                    break;

                case StartMenuOption.SignUp:
                    var signUpCredentials = startMenu.GetSignUpData();
                    UserAttemptSignUp?.Invoke(new SignUpRequestData(signUpCredentials.email, signUpCredentials.password));
                    break;

                case StartMenuOption.Exit:
                    CurrentState = MenuState.Exit;
                    break;

                default:
                    UIUtils.Warning("\nInvalid option, try again.");
                    Pause();
                    break;
            }
        }

        private void RunMainMenu()
        {
            mainMenu.DisplayMenu();
            string userInput = (mainMenu.GetUserInput() ?? "").Trim();

            switch (userInput)
            {
                case "1":
                    CreateProjectRequestData? createProjectRequest = GetProjectCreationData();

                    if (createProjectRequest != null)
                    {
                        UserRequestsProjectCreation?.Invoke(createProjectRequest);
                    }

                    break;

                case "2":
                    UserRequestsProjectList?.Invoke();
                    break;

                case "3":
                    EditProjectRequestData? editProjectRequest = GetProjectEditionData();

                    if (editProjectRequest != null)
                    {
                        UserRequestsProjectEdition?.Invoke(editProjectRequest);
                    }

                    break;

                case "4":
                    int? projectIdToDelete = GetProjectDeletionData();

                    if (projectIdToDelete.HasValue)
                    {
                        UserRequestsProjectDeletion?.Invoke(projectIdToDelete.Value);
                    }

                    break;

                case "5":
                    UIUtils.PrintHeader("Project Tasks");
                    Console.Write("Project ID: ");
                    if (int.TryParse(Console.ReadLine() ?? "", out int projectId))
                    {
                        UserRequestsProjectTasks?.Invoke(projectId);
                    }
                    else
                    {
                        ShowErrorMessage("The project ID must be an integer.");
                    }

                    break;

                case "6":
                    UIUtils.PrintHeader("Add Task");
                    Console.Write("Project ID: ");
                    if (int.TryParse(Console.ReadLine() ?? "", out int addPid))
                    {
                        Console.Write("Task Description: ");
                        string desc = Console.ReadLine() ?? "";
                        UserRequestsTaskAddition?.Invoke(addPid, desc);
                    }
                    else
                    {
                        ShowErrorMessage("The project ID must be an integer.");
                    }
                    break;

                case "7":
                    UIUtils.PrintHeader("Edit Task");
                    Console.Write("Project ID: ");
                    if (int.TryParse(Console.ReadLine() ?? "", out int editPid))
                    {
                        Console.Write("Task ID: ");
                        if (int.TryParse(Console.ReadLine() ?? "", out int taskId))
                        {
                            Console.Write("New Description: ");
                            string newDesc = Console.ReadLine() ?? "";
                            UserRequestsTaskEdition?.Invoke(editPid, taskId, newDesc);
                        }
                        else
                        {
                            ShowErrorMessage("The task ID must be an integer.");
                        }
                    }
                    else
                    {
                        ShowErrorMessage("The project ID must be an integer.");
                    }
                    break;

                case "8":
                    UIUtils.PrintHeader("Remove Task");
                    Console.Write("Project ID: ");
                    if (int.TryParse(Console.ReadLine() ?? "", out int remPid))
                    {
                        Console.Write("Task ID: ");
                        if (int.TryParse(Console.ReadLine() ?? "", out int remTaskId))
                        {
                            UserRequestsTaskDeletion?.Invoke(remPid, remTaskId);
                        }
                        else
                        {
                            ShowErrorMessage("The task ID must be an integer.");
                        }
                    }
                    else
                    {
                        ShowErrorMessage("The project ID must be an integer.");
                    }
                    break;

                case "9":
                    int? projectIdToReport = GetProjectReportData();

                    if (projectIdToReport.HasValue)
                    {
                        UserRequestsProjectReport?.Invoke(projectIdToReport.Value);
                    }

                    break;

                case "10":
                    CurrentState = MenuState.StartMenu;
                    break;

                default:
                    UIUtils.Warning("Invalid option, try again.");
                    Pause();
                    break;
            }
        }

        public void ShowLoginResult(bool isLoggedIn, string message)
        {
            if (isLoggedIn)
            {
                UIUtils.Success(message);
                CurrentState = MenuState.MainMenu;
            }
            else
            {
                UIUtils.Error(message);
            }

            Pause();
        }

        public void ShowSignUpResult(string message)
        {
            UIUtils.Success(message);
            Pause();
        }

        public void ShowProjectCreationResult(string message)
        {
            UIUtils.Success(message);
            Pause();
        }

        public void ShowProjectEditionResult(string message)
        {
            UIUtils.Success(message);
            Pause();
        }

        public void ShowProjectDeletionResult(string message)
        {
            UIUtils.Success(message);
            Pause();
        }

        public void ShowReportResult(string message)
        {
            UIUtils.Success(message);
            Pause();
        }

        public void ShowTaskOperationResult(TaskOperationResultData result)
        {
            if (result.IsSuccessful)
            {
                UIUtils.Success(result.Message);
            }
            else
            {
                UIUtils.Error(result.Message);
            }
            Pause();
        }

        public void ShowErrorMessage(string message)
        {
            UIUtils.Error($"Input error: {message}");
            UIUtils.Info("Please correct the data and try again.");
            Pause();
        }

        public void RefreshProjectList(List<string> projectList)
        {
            UIUtils.Highlight("Updated project list:");

            if (projectList.Count == 0)
            {
                UIUtils.Warning("There are no projects available.");
            }
            else
            {
                foreach (string project in projectList)
                {
                    UIUtils.Info(project);
                }
            }

            Pause();
        }

        public void ShowProjectList(List<string> projectList)
        {
            UIUtils.Highlight("Project list:");

            if (projectList.Count == 0)
            {
                UIUtils.Warning("There are no projects available.");
            }
            else
            {
                foreach (string project in projectList)
                {
                    UIUtils.Info(project);
                }
            }

            Pause();
        }

        public void ShowTaskList(List<string> tasks)
        {
            UIUtils.Highlight("Task list:");

            if (tasks.Count == 0)
            {
                UIUtils.Warning("There are no tasks associated with this project.");
            }
            else
            {
                foreach (string task in tasks)
                {
                    UIUtils.Info($"- {task}");
                }
            }

            Pause();
        }

        private CreateProjectRequestData? GetProjectCreationData()
        {
            UIUtils.PrintHeader("New Project");
            Console.Write("Project name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Project description: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Start date (yyyy-MM-dd): ");
            string startDateInput = Console.ReadLine() ?? "";

            Console.Write("End date (yyyy-MM-dd): ");
            string endDateInput = Console.ReadLine() ?? "";

            if (!TryParseProjectDate(startDateInput, out DateTime startDate) ||
                !TryParseProjectDate(endDateInput, out DateTime endDate))
            {
                ShowErrorMessage("Project dates must use the format yyyy-MM-dd.");
                return null;
            }

            // Send the converted data to the Controller, keeping the business logic in the Model.
            return new CreateProjectRequestData(name, description, startDate, endDate);
        }

        private EditProjectRequestData? GetProjectEditionData()
        {
            UIUtils.PrintHeader("Edit Project");
            Console.Write("Project ID: ");
            string projectIdInput = Console.ReadLine() ?? "";

            if (!int.TryParse(projectIdInput, out int projectId))
            {
                ShowErrorMessage("The project ID must be an integer.");
                return null;
            }

            Console.Write("Project name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Project description: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Start date (yyyy-MM-dd): ");
            string startDateInput = Console.ReadLine() ?? "";

            Console.Write("End date (yyyy-MM-dd): ");
            string endDateInput = Console.ReadLine() ?? "";

            if (!TryParseProjectDate(startDateInput, out DateTime startDate) ||
                !TryParseProjectDate(endDateInput, out DateTime endDate))
            {
                ShowErrorMessage("Project dates must use the format yyyy-MM-dd.");
                return null;
            }

            // Keep the View limited to data collection/conversion, without applying business rules.
            return new EditProjectRequestData(projectId, name, description, startDate, endDate);
        }

        private int? GetProjectDeletionData()
        {
            UIUtils.PrintHeader("Delete Project");
            Console.Write("Project ID: ");
            string projectIdInput = Console.ReadLine() ?? "";

            if (!int.TryParse(projectIdInput, out int projectId))
            {
                ShowErrorMessage("The project ID must be an integer.");
                return null;
            }

            return projectId;
        }

        private int? GetProjectReportData()
        {
            UIUtils.PrintHeader("Generate Report");
            Console.Write("Project ID: ");
            string projectIdInput = Console.ReadLine() ?? "";

            if (!int.TryParse(projectIdInput, out int projectId))
            {
                ShowErrorMessage("The project ID must be an integer.");
                return null;
            }

            return projectId;
        }

        private static bool TryParseProjectDate(string value, out DateTime date)
        {
            return DateTime.TryParseExact(
                value,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);
        }

        private void Pause()
        {
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
