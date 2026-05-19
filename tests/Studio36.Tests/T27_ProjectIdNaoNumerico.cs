namespace Studio36.Tests;

public static class T27_ProjectIdNaoNumerico
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n5\nabc\n\n10\n3");

        TestHelper.AssertContains(output, "\nLogin successful.");
        TestHelper.AssertContains(output, "Project ID: ");
        TestHelper.AssertContains(output, "The project ID must be an integer.");
        TestHelper.AssertContains(output, "Please correct the data and try again.");
        TestHelper.AssertContains(output, "Goodbye!");
    }
}
