using Studio36.ControllerComponent;
using Studio36.DTOs;
using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T37_FluxoEditarProjetoNomeVazioMvc
{
    public static void Run()
    {
        Model model = new();
        EditProjectFakeViewBase view = new();

        _ = new Controller(model, view, new ReportGeneratorStub());

        view.SubmitProjectEdition(new EditProjectRequestData(
            1,
            "",
            "Descricao valida",
            new DateTime(2026, 5, 14),
            new DateTime(2026, 5, 30)));

        List<string> projects = model.GetProjects();

        TestHelper.AssertTrue(
            view.LastErrorMessage == "The project name cannot be empty.",
            "The View should receive a validation error when the project name is empty.");

        TestHelper.AssertTrue(
            projects.Contains("1 - Projeto de demonstração"),
            "The Model should keep the original project name when edition validation fails.");
    }
}
