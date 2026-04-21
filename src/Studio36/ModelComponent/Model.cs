namespace Studio36.ModelComponent
{
    public class Model
    {
        public event Action<LoginResult>? LoginEvaluated;

        public Model()
        {
        }

        public void AreCredentialsValid(string username, string password)
        {
            Console.WriteLine("Checking Login Credentials...");
            if (username == "Manel" && password == "AmorDeMae") // Should consult a database or an API
            {
                LoginEvaluated?.Invoke(LoginResult.Success);
            }
            else if (username == null && password == null)
            {
                LoginEvaluated?.Invoke(LoginResult.InvalidCredentials);
            }
            else
            {
                LoginEvaluated?.Invoke(LoginResult.DatabaseError);
            }
        }
    }
}
