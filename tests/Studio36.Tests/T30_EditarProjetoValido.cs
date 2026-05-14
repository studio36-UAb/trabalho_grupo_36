namespace Studio36.Tests;

public static class T30_EditarProjetoValido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication(
            "1\nadmin\nadmin123\n\n3\n1\nProjeto editado\nDescricao editada\n2026-05-04\n2026-05-20\n\n2\n\n7\n3");

        TestHelper.AssertContains(output, "Login successful.");
        TestHelper.AssertContains(output, "Edit Project");
        TestHelper.AssertContains(output, "Project 1 updated successfully.");
        TestHelper.AssertContains(output, "Project list:");
        TestHelper.AssertContains(output, "1 - Projeto editado");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
