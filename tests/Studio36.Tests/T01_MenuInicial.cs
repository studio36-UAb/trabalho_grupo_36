namespace Studio36.Tests;

public static class T01_MenuInicial
{
    public static void Run()
    {
        string output = TestHelper.RunApplication("3");

        TestHelper.AssertContains(output, "Welcome to Studio36!");
        TestHelper.AssertContains(output, "Please select an option:");
        TestHelper.AssertContains(output, "1. Log in");
        TestHelper.AssertContains(output, "2. Sign up");
        TestHelper.AssertContains(output, "3. Exit");
        TestHelper.AssertContains(output, "Selection:");
    }
}
