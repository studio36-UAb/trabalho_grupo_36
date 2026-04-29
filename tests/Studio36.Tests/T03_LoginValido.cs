namespace Studio36.Tests;

public static class T03_LoginValido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n2\n3");

        TestHelper.AssertContains(output, "Please enter your username:");
        TestHelper.AssertContains(output, "Please enter your password:");
        TestHelper.AssertContains(output, "Login successful.");
        TestHelper.AssertContains(output, "Main Menu!");
    }
}