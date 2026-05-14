using Studio36.ControllerComponent;
using Studio36.ModelComponent;
using Studio36.ModelComponent.Interfaces;
using Studio36.ReportComponent;
using Studio36.ReportComponent.Interfaces;
using Studio36.Utils;
using Studio36.ViewComponent;
using Studio36.ViewComponent.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            IModel model = new Model();
            IView view = new View();
            IReportGenerator reportGenerator = new PdfReportGenerator();

            Controller controller = new(model, view, reportGenerator);
            controller.StartProgram();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred during initialization: " + ex.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        finally
        {
            FileUtils.CopyRuntimeDBtoSourceDB();
            Logger.EndSession();
        }
    }
}
