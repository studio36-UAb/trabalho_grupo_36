using System;
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

        public event Action<bool, string>? SendLoginState;
        public event Action<bool, string>? SendSignUpState;

        public Model()
        {
            authenticationService = new AuthenticationService(@"UsersDatabase/UsersAccounts.json");
        }

        public void AreCredentialsValid(string username, string password)
        {
            ValidateLoginData(username, password);

            (LoginResult result, string message) = authenticationService.ValidateCredentials(username, password);

            IsLoggedIn = result == LoginResult.Success;
            SendLoginState?.Invoke(IsLoggedIn, message);
        }

        public void RegisterUser(string username, string password)
        {
            (SignUpResult result, string message) = authenticationService.RegisterUser(username, password);

            bool success = result == SignUpResult.Success;
            SendSignUpState?.Invoke(success, message);
        }

        private void ValidateLoginData(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidLoginDataException("The username cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidLoginDataException("The password cannot be empty.");
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