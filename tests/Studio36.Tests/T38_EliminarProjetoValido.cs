namespace Studio36.Tests;

public static class T38_EliminarProjetoValido
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n5\n1\n\n2\n\n7\n3");

        TestHelper.AssertContains(output, "Login successful.");
        TestHelper.AssertContains(output, "Delete Project");
        TestHelper.AssertContains(output, "Project 1 deleted successfully.");
        TestHelper.AssertContains(output, "Project list:");
        TestHelper.AssertContains(output, "There are no projects available.");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
