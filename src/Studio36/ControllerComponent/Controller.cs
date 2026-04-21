using Studio36.ModelComponent;
using Studio36.ViewComponent;

namespace Studio36.ControllerComponent
{
    public class Controller
    {
        readonly Model model;
        readonly View view;

        private MenuState _currentMenu = MenuState.StartMenu;

        public Controller()
        {
            model = new Model();
            view = new View();

            view.StartMenuOptionSent += ProcessStartMenuOption;
            model.LoginEvaluated += HandleLoginResult;
        }

        public void StartProgram()
        {
            RunStateMachine();
        }

        // State Machine to manage menu navigation. State changed by event handlers
        private void RunStateMachine()
        {
            while (_currentMenu != MenuState.Exit)
            {
                switch (_currentMenu)
                {
                    case MenuState.StartMenu:
                        view.RunStartMenu();
                        break;
                    case MenuState.MainMenu:
                        view.RunMainMenu();
                        _currentMenu = MenuState.StartMenu;
                        break;
                }
            }
        }

        void ProcessStartMenuOption(StartMenuOption menuOption)
        {
            StartMenu startMenu = new StartMenu();
            switch (menuOption)
            {
                case StartMenuOption.Login:
                    var (username, password) = startMenu.GetLoginData();
                    ProcessLogin(username, password);
                    break;
                case StartMenuOption.SignUp:
                    // Handle sign up logic here
                    break;
                case StartMenuOption.Exit:
                    _currentMenu = MenuState.Exit;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    _currentMenu = MenuState.StartMenu;
                    break;
            }
        }

        public void ProcessLogin(string username, string password)
        {
            model.AreCredentialsValid(username, password);
        }

        private void HandleLoginResult(LoginResult loginResult)
        {
            if (loginResult == LoginResult.Success)
            {
                view.ShowLoginSuccess();
                _currentMenu = MenuState.MainMenu;  // Transition to main menu
            }
            else
            {
                // printLoginResult(loginResult);
                _currentMenu = MenuState.StartMenu;  // Stay in start menu
            }
        }


        /*// AUTENTICAÇÃO
        public bool Login(string email, string password)
        {
            return false;
        }

        public bool Registar(string email, string password)
        {
            return false;
        }

        // PROJETOS
        public void CriarProjeto(string nome, string descricao, DateTime dataInicio, DateTime dataFim)
        {
        }

        public List<string> ListarProjetos()
        {
            return new List<string>();
        }

        public void EditarProjeto(int idProjeto, string nome, string descricao)
        {
        }

        public void EliminarProjeto(int idProjeto)
        {
        }

        // TAREFAS
        public void CriarTarefa(int idProjeto, string nome, string descricao, DateTime prazo, string prioridade)
        {
        }

        public List<string> ListarTarefas(int idProjeto)
        {
            return new List<string>();
        }

        public void EditarTarefa(int idTarefa, string nome, string descricao)
        {
        }

        public void EliminarTarefa(int idTarefa)
        {
        }

        // MEMBROS
        public void AdicionarMembro(int idProjeto, string nome, string funcao)
        {
        }

        public List<string> ListarMembros(int idProjeto)
        {
            return new List<string>();
        }

        // RELATÓRIOS
        public void GerarRelatorio(int idProjeto)
        {
        }
        
        public void printLoginResult(LoginResult result)
        {
            switch (result)
            {
                case LoginResult.Success:
                    Console.WriteLine("\n[SUCCESS] Welcome back! Redirecting to dashboard...");
                    break;
                case LoginResult.InvalidCredentials:
                    Console.WriteLine("\n[ERROR] Invalid username or password. Please try again.");
                    break;
                case LoginResult.DatabaseError:
                    Console.WriteLine("\n[ERROR] A database error occurred. Please contact support.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
         */
    }
}