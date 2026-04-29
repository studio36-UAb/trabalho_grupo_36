using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T09_ModelLoginValido
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

        model.AreCredentialsValid("admin", "admin123");

        TestHelper.AssertTrue(model.IsLoggedIn, "The model should mark the user as logged in.");
        TestHelper.AssertTrue(eventValue == true, "The model should emit a successful login event.");
        TestHelper.AssertTrue(eventMessage == "Login successful.", "The model should emit the correct success message.");
    }
}