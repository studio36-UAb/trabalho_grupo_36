namespace Studio36.Tests;

public static class T06_OpcaoInvalida
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("9\n\n3");

        TestHelper.AssertContains(output, "Invalid option, try again.");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}