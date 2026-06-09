namespace Studio36.Tests;

public static class T03_LoginValido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n2\n3");

        TestHelper.AssertContains(output, "Username: ");
        TestHelper.AssertContains(output, "Password: ");
        TestHelper.AssertContains(output, "\nLogin successful.");
        TestHelper.AssertContains(output, "STUDIO36 DASHBOARD");
    }
}