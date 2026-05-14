using System;
using Studio36.DTOs;
using Studio36.ModelComponent.Entities;
using Studio36.ModelComponent.Interfaces;
using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent
{
    public class Model : IModel
    {
        private readonly List<Project> projetos = new()
        {
            new Project(
                1,
                "Projeto de demonstração",
                "Projeto inicial usado para demonstrar a aplicação.",
                DateTime.Today,
                DateTime.Today.AddDays(30))
        };

        private readonly Dictionary<int, List<string>> tarefasPorProjeto = new()
        {
            { 1, new List<string> { "Definir arquitetura MVC", "Validar tratamento de erros" } }
        };
        private readonly IAuthenticationService authenticationService;
        private readonly IRegistrationService registrationService;

        public bool IsLoggedIn { get; set; } = false;

        public event Action<LoginResultData>? SendLoginState;
        public event Action<SignUpResultData>? SendSignUpState;
        public event Action<CreateProjectResultData>? SendProjectCreationState;
        public event Action<EditProjectResultData>? SendProjectEditionState;
        public event Action<DeleteProjectResultData>? SendProjectDeletionState;

        public Model()
        {
            var jsonAccountService = new JsonAccountService(@"UsersDatabase/UsersAccounts.json");
            authenticationService = jsonAccountService;
            registrationService = jsonAccountService;
        }

        public Model(IAuthenticationService authService, IRegistrationService regService)
        {
            authenticationService = authService;
            registrationService = regService;
        }

        public void AreCredentialsValid(LoginRequestData request)
        {
            ValidateLoginInput(request.Email, request.Password);

            (LoginResult result, string message) = authenticationService.VerifyCredentials(request.Email, request.Password);

            SendLoginState?.Invoke(new LoginResultData(result == LoginResult.Success, message));
        }

        public void RegisterUser(SignUpRequestData request)
        {
            (SignUpResult result, string message) = registrationService.RegisterUser(request.Email, request.Password);

            SendSignUpState?.Invoke(new SignUpResultData(result == SignUpResult.Success, message));
        }

        public void CreateProject(CreateProjectRequestData request)
        {
            ValidateProjectInput(request);

            int idProjeto = GetNextProjectId();

            // Guarda o projeto como entidade de domínio para suportar futuros fluxos de CRUD.
            projetos.Add(new Project(
                idProjeto,
                request.Nome.Trim(),
                request.Descricao.Trim(),
                request.DataInicio,
                request.DataFim));
            tarefasPorProjeto.Add(idProjeto, new List<string>());

            SendProjectCreationState?.Invoke(new CreateProjectResultData(
                true,
                idProjeto,
                $"Project created successfully with ID {idProjeto}."));
        }

        public void EditProject(EditProjectRequestData request)
        {
            ValidateProjectInput(request.Nome, request.Descricao, request.DataInicio, request.DataFim);

            Project projeto = GetProjectById(request.IdProjeto);

            // Atualiza a entidade existente sem criar um novo projeto nem alterar o seu ID.
            projeto.UpdateDetails(
                request.Nome.Trim(),
                request.Descricao.Trim(),
                request.DataInicio,
                request.DataFim);

            SendProjectEditionState?.Invoke(new EditProjectResultData(
                true,
                request.IdProjeto,
                $"Project {request.IdProjeto} updated successfully."));
        }

        public void DeleteProject(int idProjeto)
        {
            Project projeto = GetProjectById(idProjeto);

            projetos.Remove(projeto);
            tarefasPorProjeto.Remove(idProjeto);

            SendProjectDeletionState?.Invoke(new DeleteProjectResultData(
                true,
                idProjeto,
                $"Project {idProjeto} deleted successfully."));
        }

        public ProjectReportData GetProjectReportData(int idProjeto)
        {
            Project projeto = GetProjectById(idProjeto);

            List<string> tarefas = tarefasPorProjeto.ContainsKey(idProjeto)
                ? new List<string>(tarefasPorProjeto[idProjeto])
                : new List<string>();

            return new ProjectReportData(
                projeto.Id,
                projeto.Name,
                projeto.Description,
                projeto.StartDate,
                projeto.EndDate,
                tarefas);
        }

        private void ValidateLoginInput(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidLoginInputException("The email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidLoginInputException("The password cannot be empty.");
            }
        }

        private static void ValidateProjectInput(CreateProjectRequestData request)
        {
            ValidateProjectInput(request.Nome, request.Descricao, request.DataInicio, request.DataFim);
        }

        private static void ValidateProjectInput(string nome, string descricao, DateTime dataInicio, DateTime dataFim)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("The project name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentException("The project description cannot be empty.");
            }

            if (dataFim < dataInicio)
            {
                throw new ArgumentException("The project end date cannot be earlier than the start date.");
            }
        }

        private int GetNextProjectId()
        {
            // Calcula o próximo ID com base nos projetos já existentes no estado do Model.
            if (projetos.Count == 0)
            {
                return 1;
            }

            return projetos.Max(projeto => projeto.Id) + 1;
        }

        public List<string> GetTasksByProject(int idProjeto)
        {
            // Verifica se o projeto existe no estado atual do Model.
            if (!ProjectExists(idProjeto))
            {
                throw new ProjectNotFoundException(idProjeto);
            }

            // Se o projeto existe mas ainda não tem tarefas, devolve uma lista vazia.
            if (!tarefasPorProjeto.ContainsKey(idProjeto))
            {
                return new List<string>();
            }

            return tarefasPorProjeto[idProjeto];
        }

        public List<string> GetProjects()
        {
            // Devolve a lista atualizada de projetos existentes no Model.
            return projetos
                .OrderBy(projeto => projeto.Id)
                .Select(projeto => $"{projeto.Id} - {projeto.Name}")
                .ToList();
        }

        private bool ProjectExists(int idProjeto)
        {
            return projetos.Any(projeto => projeto.Id == idProjeto);
        }

        private Project GetProjectById(int idProjeto)
        {
            Project? projeto = projetos.FirstOrDefault(projeto => projeto.Id == idProjeto);

            if (projeto == null)
            {
                throw new ProjectNotFoundException(idProjeto);
            }

            return projeto;
        }
    }
}
