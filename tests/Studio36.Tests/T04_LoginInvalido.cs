namespace Studio36.Tests;

public static class T04_LoginInvalido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nteste@studio36.com\nbad\n\n3");

        TestHelper.AssertContains(output, "Please enter your email:");
        TestHelper.AssertContains(output, "Please enter your password:");
        TestHelper.AssertContains(output, "Login failed.");
    }
}