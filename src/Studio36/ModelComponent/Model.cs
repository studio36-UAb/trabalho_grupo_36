using System;
using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent
{
    public class Model
    {
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
    }
}