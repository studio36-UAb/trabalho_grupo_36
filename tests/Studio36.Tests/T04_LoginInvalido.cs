namespace Studio36.Tests;

public static class T04_LoginInvalido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nbadpassword\n\n3");

        TestHelper.AssertContains(output, "Please enter your username:");
        TestHelper.AssertContains(output, "Please enter your password:");
        TestHelper.AssertContains(output, "Invalid password.");
    }
}