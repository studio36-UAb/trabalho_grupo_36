using Studio36.ModelComponent.Services;

namespace Studio36.ModelComponent.Interfaces
{
    public interface IRegistrationService
    {
        (SignUpResult, string) RegisterUser(string email, string password);
    }
}
