using Newtonsoft.Json;
using Studio36.Utils;

namespace Studio36.ModelComponent.Services
{
    public class AuthenticationService
    {
        private readonly string _jsonFilePath;
        public List<UserData> _users { get; set; } = [];

        public AuthenticationService(string jsonFilePath)
        {
            _jsonFilePath = Path.Combine(AppContext.BaseDirectory, jsonFilePath);
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                if (File.Exists(_jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(_jsonFilePath);

                    _users = JsonConvert.DeserializeObject<List<UserData>>(jsonContent)!;

                    if (_users == null)
                    {
                        Logger.Warning("User database was empty or null after deserialization.");
                        throw new Exception("Database empty.");
                    }
                }
                else
                {
                    Logger.Warning($"User database file not found: {_jsonFilePath}");
                    throw new Exception($"User database file not found: {_jsonFilePath}");
                }
            }
            catch (JsonException jsonEx)
            {
                Logger.Error("JSON parsing failed. Check if JSON format matches UserData structure.", jsonEx);
                throw new Exception("Invalid database format.");
            }
            catch (Exception ex)
            {
                Logger.Error("Unexpected error loading users. Specific cause: " + ex.Message, ex);
                throw new Exception("Database is temporarily unavailable. Try again later, please.");
            }
        }

        public (LoginResult, string) ValidateCredentials(string username, string password)
        {
            UserData? user = _users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                Logger.Info($"Login attempt failed: User '{username}' not found.");
                return (LoginResult.InvalidCredentials, "User not found.");
            }

            if (user.Password == password)
            {
                Logger.Info($"User '{username}' logged in successfully.");
                return (LoginResult.Success, "Login successful.");
            }

            Logger.Warning($"Login attempt failed: Invalid password for user '{username}'.");
            return (LoginResult.InvalidCredentials, "Invalid password.");
        }

        public (SignUpResult, string) RegisterUser(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    Logger.Warning("Sign up attempt failed: Empty username or password.");
                    return (SignUpResult.InvalidInput, "Username or password cannot be empty.");
                }

                if (password.Length < 4)
                {
                    Logger.Warning($"Sign up attempt failed: Password too short for user '{username}'.");
                    return (SignUpResult.InvalidInput, "Password must be at least 4 characters long.");
                }

                UserData? existingUser = _users.FirstOrDefault(u => u.Username == username);
                if (existingUser != null)
                {
                    Logger.Info($"Sign up attempt failed: User '{username}' already exists.");
                    return (SignUpResult.UserAlreadyExists, "Username already taken. Please choose another.");
                }

                _users.Add(new UserData
                {
                    Username = username,
                    Password = password
                });

                SaveUsers();

                Logger.Info($"User '{username}' registered successfully.");
                return (SignUpResult.Success, "Registration successful! You can now log in.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error during user registration: {ex.Message}", ex);
                return (SignUpResult.DatabaseError, "An error occurred during registration. Please try again later.");
            }
        }

        private void SaveUsers()
        {
            try
            {
                string? directory = Path.GetDirectoryName(_jsonFilePath);

                if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string jsonContent = JsonConvert.SerializeObject(_users, Formatting.Indented);
                File.WriteAllText(_jsonFilePath, jsonContent);
                Logger.Info($"User database saved successfully to: {_jsonFilePath}");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to save user database.", ex);
                throw new Exception("Failed to save user data.");
            }
        }
    }
}