using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T10_ModelLoginInvalido
{
    public static void Run()
    {
        Model model = new();
        bool? eventValue = null;
        string? eventMessage = null;

        model.SendLoginState += (isLoggedIn, message) =>
        {
            eventValue = isLoggedIn;
            eventMessage = message;
        };

        model.AreCredentialsValid("admin", "wrong");

        TestHelper.AssertFalse(model.IsLoggedIn, "The model should not mark the user as logged in.");
        TestHelper.AssertTrue(eventValue == false, "The model should emit a failed login event.");
        TestHelper.AssertTrue(eventMessage == "Invalid password.", "The model should emit the correct failure message.");
    }
}