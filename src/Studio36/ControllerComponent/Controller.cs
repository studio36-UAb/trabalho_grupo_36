using Studio36.ModelComponent;
using Studio36.ViewComponent;

namespace Studio36.ControllerComponent
{
    public class Controller
    {
        readonly Model model;
        readonly View view;

        public Controller()
        {
            model = new Model();
            view = new View(model);

            view.UserAttemptLogin += ProcessLogin;
        }

        public void StartProgram()
        {
            view.Run();
        }

        public void ProcessLogin(string username, string password)
        {
            model.AreCredentialsValid(username, password);
        }



    }
}
