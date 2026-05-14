using Studio36.DTOs;

namespace Studio36.ReportComponent.Interfaces
{
    public interface IReportGenerator
    {
        ReportResultData GenerateProjectReport(ProjectReportData data);
    }
}
