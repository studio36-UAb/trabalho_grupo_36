namespace Studio36.Tests;

public static class T18_CriarProjetoSemNome
{
    public static void Run()
    {
        string output = TestHelper.RunApplication(
            "1\nadmin\nadmin123\n\n1\n\nDescricao valida\n2026-05-04\n2026-05-10\n\n2\n\n7\n3");

        TestHelper.AssertContains(output, "Login successful.");
        TestHelper.AssertContains(output, "New Project");
        TestHelper.AssertContains(output, "The project name cannot be empty.");
        TestHelper.AssertContains(output, "Please correct the data and try again.");
        TestHelper.AssertContains(output, "Project list:");
        TestHelper.AssertContains(output, "1 - Projeto de demonstração");

        if (output.Contains("2 -", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The project should not be created when the name is empty.");
        }
    }
}
