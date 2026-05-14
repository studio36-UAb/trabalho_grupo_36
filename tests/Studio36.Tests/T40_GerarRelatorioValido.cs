namespace Studio36.Tests;

public static class T40_GerarRelatorioValido
{
    public static void Run()
    {
        string expectedReportPath = Path.Combine("Reports", "project-1-report.pdf");

        if (File.Exists(expectedReportPath))
        {
            File.Delete(expectedReportPath);
        }

        string output = TestHelper.RunApplication("1\nadmin\nadmin123\n\n6\n1\n\n7\n3");

        TestHelper.AssertContains(output, "Login successful.");
        TestHelper.AssertContains(output, "Generate Report");
        TestHelper.AssertContains(output, "Report generated successfully:");
        TestHelper.AssertTrue(File.Exists(expectedReportPath), "The report PDF file should be created.");

        File.Delete(expectedReportPath);
    }
}
