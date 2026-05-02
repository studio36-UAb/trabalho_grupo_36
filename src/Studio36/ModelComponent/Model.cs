using System;
using Studio36.DTOs;
using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent
{
    public class Model
    {
        private readonly Dictionary<int, string> projetos = new()
        {
            { 1, "Projeto de demonstração" }
        };

        private readonly Dictionary<int, List<string>> tarefasPorProjeto = new()
        {
            { 1, new List<string> { "Definir arquitetura MVC", "Validar tratamento de erros" } }
        };
        private readonly AuthenticationService authenticationService;

        public bool IsLoggedIn { get; set; } = false;

        public event Action<LoginResultData>? SendLoginState;
        public event Action<SignUpResultData>? SendSignUpState;

        public Model()
        {
            authenticationService = new AuthenticationService(@"UsersDatabase/UsersAccounts.json");
        }

        public void AreCredentialsValid(LoginRequestData request)
        {
            ValidateLoginInput(request.Email, request.Password);

            (LoginResult result, string message) = authenticationService.VerifyCredentials(request.Email, request.Password);

            SendLoginState?.Invoke(new LoginResultData(result == LoginResult.Success, message));
        }

        public void RegisterUser(SignUpRequestData request)
        {
            (SignUpResult result, string message) = authenticationService.RegisterUser(request.Email, request.Password);

            SendSignUpState?.Invoke(new SignUpResultData(result == SignUpResult.Success, message));
        }

        private void ValidateLoginInput(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidLoginInputException("The username cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidLoginInputException("The password cannot be empty.");
            }
        }

        public List<string> GetTasksByProject(int idProjeto)
        {
            // Verifica se o projeto existe no estado atual do Model.
            if (!projetos.ContainsKey(idProjeto))
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
                .Select(projeto => $"{projeto.Key} - {projeto.Value}")
                .ToList();
        }
    }
}