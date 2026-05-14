using Studio36.ControllerComponent;
using Studio36.DTOs;
using Studio36.ModelComponent;

namespace Studio36.Tests;

public static class T36_FluxoEditarProjetoInexistenteMvc
{
    public static void Run()
    {
        Model model = new();
        EditProjectFakeViewBase view = new();

        _ = new Controller(model, view, new ReportGeneratorStub());

        view.SubmitProjectEdition(new EditProjectRequestData(
            999,
            "Projeto inexistente",
            "Tentativa de editar projeto inexistente",
            new DateTime(2026, 5, 14),
            new DateTime(2026, 5, 30)));

        TestHelper.AssertTrue(
            view.LastErrorMessage == "The project with ID 999 does not exist in the current Model state.",
            "The View should receive an error message when the project ID does not exist.");
    }
}
