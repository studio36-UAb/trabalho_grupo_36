using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent
{
    public class Model
    {
        private readonly AuthenticationService _authenticationService;

        public event Action<LoginResult, string>? LoginEvaluated;

        public Model()
        {
            _authenticationService = new AuthenticationService("UsersAccounts.json");
        }

        public void AreCredentialsValid(string username, string password)
        {
            var (loginResult, message) = _authenticationService.ValidateCredentials(username, password);
            LoginEvaluated?.Invoke(loginResult, message);
        }
    }
}
