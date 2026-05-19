namespace Studio36.Tests;

public static class T01_MenuInicial
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("3");

        TestHelper.AssertContains(output, "Welcome to Studio36");
        TestHelper.AssertContains(output, "Log in to your account");
        TestHelper.AssertContains(output, "Create a new account");
        TestHelper.AssertContains(output, "Exit application");
        TestHelper.AssertContains(output, "Selection > ");
    }
}
