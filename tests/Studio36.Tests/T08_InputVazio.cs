namespace Studio36.Tests;

public static class T08_InputVazio
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("\n\n3");

        TestHelper.AssertContains(output, "Invalid option, try again.");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}