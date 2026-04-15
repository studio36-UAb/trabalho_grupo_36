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
            IsLoggedIn = username == "Hey" && password == "Hey2";
            SendLoginState?.Invoke(IsLoggedIn);
        }
    }
}
