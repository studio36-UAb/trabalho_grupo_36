namespace Studio36.Tests;

public static class T05_AcessoRegisto
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("2\nadmin\npass123\n\n3");

        TestHelper.AssertContains(output, "Username: ");
        TestHelper.AssertContains(output, "Password: ");
        TestHelper.AssertContains(output, "Registration successful! You can now log in.");
    }
}