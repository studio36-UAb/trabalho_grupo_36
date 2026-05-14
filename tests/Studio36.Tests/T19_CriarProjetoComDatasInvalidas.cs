namespace Studio36.Tests;

public static class T19_CriarProjetoComDatasInvalidas
{
    public static void Run()
    {
        string output = TestHelper.RunApplication(
            "1\nadmin\nadmin123\n\n1\nProjeto datas invalidas\nDescricao valida\n2026-05-10\n2026-05-04\n\n2\n\n7\n3");

        TestHelper.AssertContains(output, "Login successful.");
        TestHelper.AssertContains(output, "New Project");
        TestHelper.AssertContains(output, "The project end date cannot be earlier than the start date.");
        TestHelper.AssertContains(output, "Please correct the data and try again.");
        TestHelper.AssertContains(output, "Project list:");
        TestHelper.AssertContains(output, "1 - Projeto de demonstração");

        if (output.Contains("2 - Projeto datas invalidas", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The project should not be created when the end date is earlier than the start date.");
        }
    }
}
