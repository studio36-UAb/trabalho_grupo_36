using Studio36.DTOs;
using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T10_ModelLoginInvalido
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

        model.AreCredentialsValid(new LoginRequestData("admin", "wrong"));

        TestHelper.AssertTrue(eventValue == false, "The model should emit a failed login event.");
        TestHelper.AssertTrue(eventMessage == "Invalid password.\n", "The model should emit the correct failure message.");
    }
}
