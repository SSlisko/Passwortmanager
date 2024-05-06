using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Principal;

namespace PW_Manager
{
    public class AccountsManager
    {
        private List<Account> accounts;

        private string filePath = "accounts.json";

        //private string filePath = "/.accounts.json";
        //string content = File.ReadAllText(filePath);


        public AccountsManager()
        {
            accounts = LoadAccounts();
        }

        public void AddAccounts(string description, string username, string password, string group)
        {
            Account account = new Account(description, username, password, group);
            accounts.Add(account);
            SaveAccounts();
        }

        public List<Account> GetAccounts()
        {
            return accounts;
        }

        public List<Account> LoadAccounts()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Account>>(json);
            }
            return new List<Account>();
        }

        public void DisplayAccounts()
        {
            Console.Clear();
            AccountsManager accountsManager = new AccountsManager();
            var accounts = accountsManager.LoadAccounts();
            string secretKey = "secretKey";
            string password = "";
            Console.WriteLine("   Passwortmanager");
            Console.WriteLine("------------------------");

            foreach (var account in accounts)
            {
                if (account.password != null)
                {
                    password = PW_Entry.Decrypt(account.password, secretKey);

                    Console.WriteLine("");
                    Console.WriteLine($"URL: {account.description}");
                    Console.WriteLine($"Gruppe: {account.group}");
                    Console.WriteLine($"Benutzername: {account.username}");
                    Console.WriteLine($"Passwort: {password}");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("");

                }
                else
                {
                    Console.WriteLine($"Password für den Account '{account.username}' ist null.");
                }
            }

            Console.ReadKey();
        }


        public void SaveAccounts()
        {
            string json = JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

    }
}
