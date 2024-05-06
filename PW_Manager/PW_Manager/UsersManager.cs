using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PW_Manager
{
    public class UsersManager
    {
        private List<User> users;
        private string filePath = "users.json";

        public UsersManager()
        {
            users = LoadUsers();
        }

        public void AddUser(string username, string password)
        {
            User user = new (username, password);
            users.Add(user);
            SaveUsers();
        }

        public bool VerifyPassword(string username, string password)
        {
            User user = users.Find(u => u.username == username);

            if (user != null)
            {
                string hashedPassword = HashPassword(password);
                return user.password == hashedPassword;
            }

            return false;
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public List<User> LoadUsers()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<User>>(json);
            }
            return new List<User>();
        }

        private void SaveUsers()
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        internal static User FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
