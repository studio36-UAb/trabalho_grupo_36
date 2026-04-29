namespace Studio36.Tests;

public static class T05_AcessoRegisto
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("2\nadmin\npass123\n\n3");

        TestHelper.AssertContains(output, "Please enter your username:");
        TestHelper.AssertContains(output, "Please enter your password:");
        TestHelper.AssertContains(output, "Username already taken. Please choose another.");
    }
}