using Studio36.DTOs;
using Studio36.ReportComponent.Interfaces;

namespace Studio36.Tests;

public sealed class ReportGeneratorStub : IReportGenerator
{
    public ProjectReportData? LastReportData { get; private set; }

    public ReportResultData GenerateProjectReport(ProjectReportData data)
    {
        LastReportData = data;

        return new ReportResultData(
            true,
            $"stub-project-{data.IdProjeto}.pdf",
            $"Report generated successfully: stub-project-{data.IdProjeto}.pdf");
    }
}
