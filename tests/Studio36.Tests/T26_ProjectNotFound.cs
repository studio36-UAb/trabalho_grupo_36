namespace Studio36.Tests;

public static class T26_ProjectNotFound
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n5\n999\n\n10\n3");

        TestHelper.AssertContains(output, "\nLogin successful.");
        TestHelper.AssertContains(output, "Project ID: ");
        TestHelper.AssertContains(output, "The project with ID 999 does not exist in the current Model state.");
        TestHelper.AssertContains(output, "Updated project list:");
        TestHelper.AssertContains(output, "1 - Projeto de demonstração");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
