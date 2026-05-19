using Studio36.ControllerComponent;
using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T39_FluxoGerarRelatorioMvc
{
    public static void Run()
    {
        var mockService = new MockProjectService();
        Model model = new(new MockAccountService(), new MockAccountService(), mockService);
        ReportFakeViewBase view = new();
        ReportGeneratorStub reportGenerator = new();

        _ = new Controller(model, view, reportGenerator);

        view.SubmitProjectReportRequest(1);

        var lastReportData = reportGenerator.LastReportData;

        if (lastReportData == null)
        {
            throw new InvalidOperationException("The Controller should ask the report generator to generate the project report.");
        }

        TestHelper.AssertTrue(
            lastReportData.Name == "Projeto de demonstração",
            "The report generator should receive the project data returned by the Model.");

        TestHelper.AssertTrue(
            view.LastReportMessage == "Report generated successfully: stub-project-1.pdf",
            "The View should receive the report generation success message through the Controller.");
    }
}
