namespace Studio36.Tests;

public static class T05_AcessoRegisto
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("2\n3");

        TestHelper.AssertContains(output, "Sign up not implemented yet.");
    }
}
