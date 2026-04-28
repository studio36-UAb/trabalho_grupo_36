namespace Studio36.ModelComponent
{
    public class Model
    {
        public bool IsLoggedIn { get; set; } = false;

        public event Action<bool>? SendLoginState;

        public Model()
        {
        }

        public void AreCredentialsValid(string username, string password)
        {
            ValidateLoginData(username, password);

            IsLoggedIn = username == "teste@studio36.com" && password == "pass123";
            SendLoginState?.Invoke(IsLoggedIn);
        }

        private void ValidateLoginData(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidLoginDataException("The email cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidLoginDataException("The password cannot be empty.");
            }

            if (!username.Contains("@"))
            {
                throw new InvalidLoginDataException("The email format is invalid.");
            }
        }
    }
}