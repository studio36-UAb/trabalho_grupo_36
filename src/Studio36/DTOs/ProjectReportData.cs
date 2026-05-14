namespace Studio36.DTOs
{
    public record ProjectReportData(
        int IdProjeto,
        string Nome,
        string Descricao,
        DateTime DataInicio,
        DateTime DataFim,
        List<string> Tarefas);
}
