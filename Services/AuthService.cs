using AgriEnergyConnect.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AgriEnergyConnect.Services
{
    public class AuthService
    {
        private readonly ILogger<AuthService> _logger;

        public AuthService(ILogger<AuthService> logger)
        {
            _logger = logger;
        }

        // Async version to match controller call
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await Task.Run(() => Authenticate(username, password));
        }

        public User? Authenticate(string username, string password)
        {
            try
            {
                // Hardcoded test users - TEMPORARY DEVELOPMENT ONLY
                var testUsers = new List<User>
                {
                    new User
                    {
                        UserId = 1,
                        Username = "farmer1",
                        Password = "password123", // Not hashed for simplicity
                        Email = "farmer@test.com",
                        FirstName = "John",
                        LastName = "Doe",
                        RoleId = 1,
                        Role = new Role { RoleId = 1, RoleName = "Farmer" }
                    },
                    new User
                    {
                        UserId = 2,
                        Username = "employee1",
                        Password = "password123",
                        Email = "employee@test.com",
                        FirstName = "Jane",
                        LastName = "Smith",
                        RoleId = 2,
                        Role = new Role { RoleId = 2, RoleName = "Employee" }
                    }
                };

                _logger.LogInformation($"Login attempt for {username}");

                var user = testUsers.FirstOrDefault(u =>
                    string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase) &&
                    u.Password == password);

                if (user != null)
                {
                    _logger.LogInformation($"Successful login for {username}");
                    return user;
                }

                _logger.LogWarning($"Failed login attempt for {username}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authentication error");
                return null;
            }
        }

        public static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}