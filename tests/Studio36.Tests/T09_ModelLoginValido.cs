using Studio36.DTOs;
using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T09_ModelLoginValido
{
    public static void Run()
    {
        Model model = new();
        bool? eventValue = null;
        string? eventMessage = null;

        model.SendLoginState += (result) =>
        {
            eventValue = result.IsSuccessful;
            eventMessage = result.Message;
        };

        model.AreCredentialsValid(new LoginRequestData("admin", "admin123"));

        TestHelper.AssertTrue(eventValue == true, "The model should emit a successful login event.");
        TestHelper.AssertTrue(eventMessage == "Login successful.\n", "The model should emit the correct success message.");
    }
}
