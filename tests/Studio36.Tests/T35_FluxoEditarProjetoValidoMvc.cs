using Studio36.ControllerComponent;
using Studio36.DTOs;
using Studio36.ModelComponent;
using Studio36.ViewComponent.Interfaces;

namespace Studio36.Tests;

public static class T35_FluxoEditarProjetoValidoMvc
{
    public static void Run()
    {
        Model model = new();
        FakeView view = new();

        _ = new Controller(model, view, new ReportGeneratorStub());

        view.SubmitProjectEdition(new EditProjectRequestData(
            1,
            "Projeto editado MVC",
            "Descricao editada pelo fluxo MVC",
            new DateTime(2026, 5, 14),
            new DateTime(2026, 5, 30)));

        List<string> projects = model.GetProjects();

        TestHelper.AssertTrue(
            projects.Contains("1 - Projeto editado MVC"),
            "The Model should update the existing project requested by the View through the Controller.");

        TestHelper.AssertTrue(
            view.LastProjectEditionMessage == "Project 1 updated successfully.",
            "The View should receive the project edition success message through the Controller.");
    }

    private sealed class FakeView : EditProjectFakeViewBase
    {
    }
}
