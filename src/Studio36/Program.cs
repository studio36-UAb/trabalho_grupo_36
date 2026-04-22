using Studio36.ControllerComponent;
using Studio36.Utils;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Controller controller = new();
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