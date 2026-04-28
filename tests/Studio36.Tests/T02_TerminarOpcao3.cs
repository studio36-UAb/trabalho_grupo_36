namespace Studio36.Tests;

public static class T02_TerminarOpcao3
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("3");

        TestHelper.AssertContains(output, "Goodbye!");
    }
}
