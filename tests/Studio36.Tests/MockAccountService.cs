using Studio36.ModelComponent;
using Studio36.ModelComponent.Interfaces;

namespace Studio36.Tests;

public class MockAccountService : IAuthenticationService, IRegistrationService
{
    public (LoginResult, string) VerifyCredentials(string email, string password)
    {
        if (password == "admin123")
        {
            return (LoginResult.Success, "\nLogin successful.\n");
        }
        return (LoginResult.InvalidCredentials, "Invalid password.\n");
    }

    public (SignUpResult, string) RegisterUser(string email, string password) => (SignUpResult.Success, "Registration successful! You can now log in.");
}
