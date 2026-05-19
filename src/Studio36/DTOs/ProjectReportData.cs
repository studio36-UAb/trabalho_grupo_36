namespace Studio36.DTOs
{
    public record ProjectReportData(
        int ProjectId,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        List<string> Tasks);
}
