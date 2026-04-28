namespace Studio36.Tests;

public static class T03_LoginValido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nHey\nHey2\n3");

        TestHelper.AssertContains(output, "Please enter your email:");
        TestHelper.AssertContains(output, "Please enter your password:");
        TestHelper.AssertContains(output, "The user is logged in. Updating UI...");
    }
}
