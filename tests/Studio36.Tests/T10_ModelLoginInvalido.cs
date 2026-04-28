using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T10_ModelLoginInvalido
{
    public static void Run()
    {
        Model model = new();
        bool? eventValue = null;

        model.SendLoginState += isLoggedIn => eventValue = isLoggedIn;

        model.AreCredentialsValid("wrong", "bad");

        TestHelper.AssertFalse(model.IsLoggedIn, "The model should not mark the user as logged in.");
        TestHelper.AssertTrue(eventValue == false, "The model should emit a failed login event.");
    }
}
