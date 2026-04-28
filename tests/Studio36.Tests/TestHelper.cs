using Studio36.ControllerComponent;

namespace Studio36.Tests;

public static class TestHelper
{
    public static string RunApplication(string input)
    {
        TextReader originalInput = Console.In;
        TextWriter originalOutput = Console.Out;

        try
        {
            using StringReader testInput = new(input);
            using StringWriter testOutput = new();

            Console.SetIn(testInput);
            Console.SetOut(testOutput);

            Controller controller = new();
            controller.StartProgram();

            return testOutput.ToString();
        }
        finally
        {
            Console.SetIn(originalInput);
            Console.SetOut(originalOutput);
        }
    }

    public static void AssertContains(string text, string expected)
    {
        if (!text.Contains(expected, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Expected to find '{expected}' in the output, but it was not found.");
        }
    }

    public static void AssertTrue(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    public static void AssertFalse(bool condition, string message)
    {
        AssertTrue(!condition, message);
    }
}
