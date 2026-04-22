using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent
{
    public class Model
    {
        private readonly AuthenticationService _authenticationService;

        public event Action<LoginResult, string>? LoginEvaluated;
        public event Action<SignUpResult, string>? SignUpEvaluated;

        public Model()
        {
            _authenticationService = new AuthenticationService("UsersAccounts.json");
        }

        public void AreCredentialsValid(string username, string password)
        {
            var (loginResult, message) = _authenticationService.ValidateCredentials(username, password);
            LoginEvaluated?.Invoke(loginResult, message);
        }

        public void RegisterUser(string username, string password)
        {
            var (signUpResult, message) = _authenticationService.RegisterUser(username, password);
            SignUpEvaluated?.Invoke(signUpResult, message);
        }
    }
}
