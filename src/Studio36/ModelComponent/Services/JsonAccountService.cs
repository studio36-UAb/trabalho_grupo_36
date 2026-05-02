using Newtonsoft.Json;
using Studio36.ModelComponent.Interfaces;
using Studio36.Utils;

namespace Studio36.ModelComponent.Services
{
    public class JsonAccountService : IAuthenticationService, IRegistrationService
    {
        private readonly string _jsonFilePath;
        public List<AccountCredentials> _users { get; set; } = [];

        public JsonAccountService(string jsonFilePath)
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

                    _users = JsonConvert.DeserializeObject<List<AccountCredentials>>(jsonContent)!;

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
                Logger.Error("JSON parsing failed. Check if JSON format matches AccountCredentials structure.", jsonEx);
                throw new Exception("Invalid database format.");
            }
            catch (Exception ex)
            {
                Logger.Error("Unexpected error loading users. Specific cause: " + ex.Message, ex);
                throw new Exception("Database is temporarily unavailable. Try again later, please.");
            }
        }

        public (LoginResult, string) VerifyCredentials(string email, string password)
        {
            AccountCredentials? user = _users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                Logger.Info($"Login attempt failed: User '{email}' not found.");
                return (LoginResult.InvalidCredentials, "User not found.\n");
            }

            if (user.Password == password)
            {
                Logger.Info($"User '{email}' logged in successfully.");
                return (LoginResult.Success, "Login successful.\n");
            }

            Logger.Warning($"Login attempt failed: Invalid password for user '{email}'.");
            return (LoginResult.InvalidCredentials, "Invalid password.\n");
        }

        public (SignUpResult, string) RegisterUser(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    Logger.Warning("Sign up attempt failed: Empty email or password.");
                    return (SignUpResult.InvalidInput, "Email or password cannot be empty.");
                }

                if (password.Length < 4)
                {
                    Logger.Warning($"Sign up attempt failed: Password too short for user '{email}'.");
                    return (SignUpResult.InvalidInput, "Password must be at least 4 characters long.");
                }

                AccountCredentials? existingUser = _users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    Logger.Info($"Sign up attempt failed: User '{email}' already exists.");
                    return (SignUpResult.UserAlreadyExists, "Email already taken. Please choose another.");
                }

                _users.Add(new AccountCredentials
                {
                    Email = email,
                    Password = password
                });

                SaveUsersInJson();

                Logger.Info($"User '{email}' registered successfully.");
                return (SignUpResult.Success, "Registration successful! You can now log in.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error during user registration: {ex.Message}", ex);
                return (SignUpResult.DatabaseError, "An error occurred during registration. Please try again later.");
            }
        }

        private void SaveUsersInJson()
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