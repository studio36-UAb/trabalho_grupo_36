namespace Studio36.Tests;

public static class T07_InputNaoNumerico
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("abc\n3");

        TestHelper.AssertContains(output, "Invalid option, try again.");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
