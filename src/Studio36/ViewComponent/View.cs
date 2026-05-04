using System;
using System.Globalization;
using Studio36.ControllerComponent;
using Studio36.DTOs;

namespace Studio36.ViewComponent
{
    public class View
    {
        private bool isRunning = true;
        private readonly StartMenu startMenu;
        private readonly MainMenu mainMenu;

        public MenuState CurrentState { get; set; } = MenuState.StartMenu;

        public event Action<LoginRequestData>? UserAttemptLogin;
        public event Action<SignUpRequestData>? UserAttemptSignUp;
        public event Action<CreateProjectRequestData>? UserRequestsProjectCreation;
        public event Action<EditProjectRequestData>? UserRequestsProjectEdition;
        public event Action? UserRequestsProjectList;
        public event Action<int>? UserRequestsProjectTasks;

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
                        Console.WriteLine("Goodbye!");
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
                    Console.WriteLine("Invalid option, try again.");
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
                    Console.Write("Please enter the project ID: ");
                    string projectIdInput = Console.ReadLine() ?? "";

                    if (int.TryParse(projectIdInput, out int idProjeto))
                    {
                        UserRequestsProjectTasks?.Invoke(idProjeto);
                    }
                    else
                    {
                        ShowErrorMessage("The project ID must be an integer.");
                    }

                    break;

                case "5":
                    CurrentState = MenuState.StartMenu;
                    break;

                default:
                    Console.WriteLine("Invalid option, try again.");
                    Pause();
                    break;
            }
        }

        public void ShowLoginResult(bool isLoggedIn, string message)
        {
            Console.WriteLine(message);

            if (isLoggedIn)
            {
                CurrentState = MenuState.MainMenu;
            }

            Pause();
        }

        public void ShowSignUpResult(string message)
        {
            Console.WriteLine(message);
            Pause();
        }

        public void ShowProjectCreationResult(string message)
        {
            Console.WriteLine(message);
            Pause();
        }

        public void ShowProjectEditionResult(string message)
        {
            Console.WriteLine(message);
            Pause();
        }

        public void ShowErrorMessage(string message)
        {
            Console.WriteLine($"Input error: {message}");
            Console.WriteLine("Please correct the data and try again.");
            Pause();
        }

        public void RefreshProjectList(List<string> listaProjetos)
        {
            Console.WriteLine("Updated project list:");

            if (listaProjetos.Count == 0)
            {
                Console.WriteLine("There are no projects available.");
            }
            else
            {
                foreach (string projeto in listaProjetos)
                {
                    Console.WriteLine(projeto);
                }
            }

            Pause();
        }

        public void ShowProjectList(List<string> listaProjetos)
        {
            Console.WriteLine("Project list:");

            if (listaProjetos.Count == 0)
            {
                Console.WriteLine("There are no projects available.");
            }
            else
            {
                foreach (string projeto in listaProjetos)
                {
                    Console.WriteLine(projeto);
                }
            }

            Pause();
        }

        public void ShowTaskList(List<string> tarefas)
        {
            Console.WriteLine("Task list:");

            if (tarefas.Count == 0)
            {
                Console.WriteLine("There are no tasks associated with this project.");
            }
            else
            {
                foreach (string tarefa in tarefas)
                {
                    Console.WriteLine($"- {tarefa}");
                }
            }

            Pause();
        }

        private CreateProjectRequestData? GetProjectCreationData()
        {
            Console.WriteLine("New Project");
            Console.Write("Project name: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Project description: ");
            string descricao = Console.ReadLine() ?? "";

            Console.Write("Start date (yyyy-MM-dd): ");
            string dataInicioInput = Console.ReadLine() ?? "";

            Console.Write("End date (yyyy-MM-dd): ");
            string dataFimInput = Console.ReadLine() ?? "";

            if (!TryParseProjectDate(dataInicioInput, out DateTime dataInicio) ||
                !TryParseProjectDate(dataFimInput, out DateTime dataFim))
            {
                ShowErrorMessage("Project dates must use the format yyyy-MM-dd.");
                return null;
            }

            // Envia os dados já convertidos para o Controller, mantendo a lógica de negócio no Model.
            return new CreateProjectRequestData(nome, descricao, dataInicio, dataFim);
        }

        private EditProjectRequestData? GetProjectEditionData()
        {
            Console.WriteLine("Edit Project");
            Console.Write("Project ID: ");
            string idProjetoInput = Console.ReadLine() ?? "";

            if (!int.TryParse(idProjetoInput, out int idProjeto))
            {
                ShowErrorMessage("The project ID must be an integer.");
                return null;
            }

            Console.Write("Project name: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Project description: ");
            string descricao = Console.ReadLine() ?? "";

            Console.Write("Start date (yyyy-MM-dd): ");
            string dataInicioInput = Console.ReadLine() ?? "";

            Console.Write("End date (yyyy-MM-dd): ");
            string dataFimInput = Console.ReadLine() ?? "";

            if (!TryParseProjectDate(dataInicioInput, out DateTime dataInicio) ||
                !TryParseProjectDate(dataFimInput, out DateTime dataFim))
            {
                ShowErrorMessage("Project dates must use the format yyyy-MM-dd.");
                return null;
            }

            // Mantém a View limitada à recolha/conversão de dados, sem aplicar regras de negócio.
            return new EditProjectRequestData(idProjeto, nome, descricao, dataInicio, dataFim);
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
