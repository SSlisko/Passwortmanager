using System.Text.Json.Serialization;

public class Account
{
    public string description { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string group { get; set; }

    [JsonConstructor]
    public Account(string description, string username, string password, string group)
    {
        this.description = description;
        this.username = username;
        this.password = password;
        this.group = group;
    }
}