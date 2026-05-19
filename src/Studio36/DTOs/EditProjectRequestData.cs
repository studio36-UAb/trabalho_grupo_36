namespace Studio36.DTOs
{
    public record EditProjectRequestData(int ProjectId, string Name, string Description, DateTime StartDate, DateTime EndDate);
}
