using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T09_ModelLoginValido
{
    public static void Run()
    {
        Model model = new();
        bool? eventValue = null;

        model.SendLoginState += isLoggedIn => eventValue = isLoggedIn;

        model.AreCredentialsValid("Hey", "Hey2");

        TestHelper.AssertTrue(model.IsLoggedIn, "The model should mark the user as logged in.");
        TestHelper.AssertTrue(eventValue == true, "The model should emit a successful login event.");
    }
}
