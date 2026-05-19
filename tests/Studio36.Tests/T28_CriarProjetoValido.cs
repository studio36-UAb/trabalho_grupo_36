namespace Studio36.Tests;

public static class T28_CriarProjetoValido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication(
            "1\nadmin\nadmin123\n\n1\nProjeto Teste\nDescricao do projeto\n2026-05-04\n2026-05-10\n\n5\n2\n\n10\n3");

        TestHelper.AssertContains(output, "\nLogin successful.");
        TestHelper.AssertContains(output, "New Project");
        TestHelper.AssertContains(output, "Project created successfully with ID 2.");
        TestHelper.AssertContains(output, "Project ID: ");
        TestHelper.AssertContains(output, "There are no tasks associated with this project.");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
