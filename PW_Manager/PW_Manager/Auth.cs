using System.Text.RegularExpressions;

public class AccountManager
{
    private List<Account> accounts;

    public AccountManager()
    {
        accounts = new List<Account>();
    }

    public void AddAccount(string description, string username, string password, string group)
    {
        Account newAccount = new Account(description,username, password, group);
      
        accounts.Add(newAccount);
        Console.WriteLine($"User {username} has been added to the {group} group.");
        Console.ReadKey();
    }

    public Account AuthenticateAccount(string username, string password, string group)
    {
        foreach (Account account in accounts)
        {
            if (account.username == username && account.password == password && account.group == group)
            {
                return account;
            }
        }
        return null;
    }

    public List<Account> GetAccounts()
    {
        return accounts;
    }
}

public class UserManager
{
    private List<User> users;

    public UserManager()
    {
        users = new List<User>();
    }

    public void AddUser(string username, string password)
    {
        User newUser = new User(username, password);

        users.Add(newUser);
        Console.WriteLine($"User {username} has been signed up");
        Console.ReadKey();
    }

    public User AuthenticateUser(string username, string password)
    {
        foreach (User user in users)
        {
            if (user.username == username && user.password == password)
            {
                return user;
            }
        }
        return null;
    }

    public List<User> GetUser()
    {
        return users;
    }
}
