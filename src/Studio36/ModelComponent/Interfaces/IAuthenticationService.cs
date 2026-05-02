using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent.Interfaces
{
    public interface IAuthenticationService
    {
        (LoginResult, string) VerifyCredentials(string email, string password);
    }
}
