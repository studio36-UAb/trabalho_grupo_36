namespace Studio36.Tests;

public static class T29_ListarProjetos
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n2\n\n5\n3");

        TestHelper.AssertContains(output, "Login successful.");
        TestHelper.AssertContains(output, "Project list:");
        TestHelper.AssertContains(output, "1 - Projeto de demonstração");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
