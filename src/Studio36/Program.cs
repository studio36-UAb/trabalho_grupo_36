using Studio36.ControllerComponent;

using Studio36.Interfaces;

using Studio36.ModelComponent;
using Studio36.ModelComponent.Services;

using Studio36.ReportComponent;
using Studio36.ReportComponent.Interfaces;

using Studio36.Utils;

using Studio36.ViewComponent;

class Program
{
    /**
     * Application Entry Point.
     * 
     * This method is responsible for the system initialization phase, orchestrating the 
     * instantiation and wiring of core components through Dependency Injection:
     * 1. Initializes Persistence Services (JSON-based).
     * 2. Bootstraps the Model with injected services.
     * 3. Configures the View and Report components.
     * 4. Injects dependencies into the Controller to begin execution.
     */
    static void Main()
    {
        Console.Clear();
        UIUtils.PrintHeader("Studio36", "System Initialization...");
        Console.WriteLine();

        try
        {
            UIUtils.Info("> Connecting to persistence layer...");
            var accountService = new JsonAccountService(@"UsersDatabase/UsersAccounts.json");
            var projectService = new JsonProjectService(@"ProjectsAndTasksDatabase/Projects.json", @"ProjectsAndTasksDatabase/Tasks.json");

            UIUtils.Info("> Bootstrapping core model...");
            IModel model = new Model(accountService, accountService, projectService);
            
            UIUtils.Info("> Initializing user interface...");
            IView view = new View();

            UIUtils.Info("> Configuring report engines...");
            IReportGenerator reportGenerator = new PdfReportGenerator();

            UIUtils.Info("> Wiring controller...");
            Controller controller = new(model, view, reportGenerator);

            UIUtils.Success("> System ready.");
            Thread.Sleep(3000); // Brief pause to show the initialization status

            controller.StartProgram();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            UIUtils.Error("An unexpected error occurred during initialization:");
            UIUtils.Error($"Details: {ex.Message}");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        finally
        {
            FileUtils.CopyRuntimeDBtoSourceDB(); // The database in this case is a JSON file, so we copy the runtime version back to the source directory to persist changes.
            Logger.EndSession();
        }
    }
}
