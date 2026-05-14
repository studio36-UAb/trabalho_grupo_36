namespace Studio36.Tests;

public static class T41_GerarRelatorioProjetoInexistente
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n6\n999\n\n7\n3");

        TestHelper.AssertContains(output, "Generate Report");
        TestHelper.AssertContains(output, "The project with ID 999 does not exist in the current Model state.");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
