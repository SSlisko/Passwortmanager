using PW_Manager;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text.Json;
using System.Text.RegularExpressions;

class Program //Variablen
{
    static List<User> users = new List<User>();
    static List<Account> accounts = new List<Account>();
    static int mainMenuSelectedOption = 0;
    static int subMenuSelectedOption = 0;
    static int passwordSubMenuSelectedOption = 0;
    static bool exitProgram = false;

    static void PW_Manager() //UI-Titel
    {
        Console.Clear();
        Console.WriteLine("   Passwortmanager");
        Console.WriteLine("------------------------");
        Console.WriteLine("");
    }
    static void Main(string[] args)
    {
        string[] mainMenuOptions = { "Registrieren", "Anmelden", "Beenden" };

        while (!exitProgram)
        {
            PW_Manager();
            //Menü mit Pfeiltasten hoch und runter
            for (int i = 0; i < mainMenuOptions.Length; i++)
            {
                if (i == mainMenuSelectedOption)
                {
                    Console.WriteLine($"->  {mainMenuOptions[i]}");
                }
                else
                {
                    Console.WriteLine($"   {mainMenuOptions[i]}");
                }
            }

            ConsoleKeyInfo mainMenuKeyInfo = Console.ReadKey();

            if (mainMenuKeyInfo.Key == ConsoleKey.UpArrow && mainMenuSelectedOption > 0)
            {
                mainMenuSelectedOption--;
            }
            else if (mainMenuKeyInfo.Key == ConsoleKey.DownArrow && mainMenuSelectedOption < mainMenuOptions.Length - 1)
            {
                mainMenuSelectedOption++;
            }
            else if (mainMenuKeyInfo.Key == ConsoleKey.Enter)
            {
                MainMenu(mainMenuSelectedOption);
            }
        }
    }

    static void MainMenu(int selectedOption)
    {
        if (selectedOption == 0)
        {
            PW_Manager(); //Registrieren
            Console.Write("Benutzername, E-Mail: ");
            string user = Console.ReadLine();
            Console.WriteLine("");
            Console.Write("Passwort: ");
            string pw = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Hallo {0}!", user);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            users.Add(new User(user, pw));
            Console.ReadKey();
            subMenuSelectedOption = 0; // Zurück zum Hauptmenü
        }
        else if (selectedOption == 2)
        {
            exitProgram = true;
        }

        {
            if (selectedOption == 1)
            {
                PW_Manager(); //Anmelden
                Console.Write("Benutzername, E-Mail: ");
                string user = Console.ReadLine();
                Console.WriteLine("");
                Console.Write("Passwort: ");
                string pw = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("Willkommen zurück {0}!", user);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.ReadKey();

                subMenuSelectedOption = 0; // Zurück zum Hauptmenü
                SubMenu();
            }
            else if (selectedOption == 2)
            {
                exitProgram = true;
            }
        }
    }



    static void SubMenu() //Untermenü
    {
        string[] subMenuOptions = { "Account hinzufügen", "Account löschen", "Accounts Anzeigen", "Zurück" };

        while (true)
        {
            PW_Manager();

            for (int i = 0; i < subMenuOptions.Length; i++)
            {
                if (i == subMenuSelectedOption)
                {
                    Console.WriteLine($"->  {subMenuOptions[i]}");
                }
                else
                {
                    Console.WriteLine($"   {subMenuOptions[i]}");
                }
            }

            ConsoleKeyInfo subMenuKeyInfo = Console.ReadKey();

            if (subMenuKeyInfo.Key == ConsoleKey.UpArrow && subMenuSelectedOption > 0)
            {
                subMenuSelectedOption--;
            }
            else if (subMenuKeyInfo.Key == ConsoleKey.DownArrow && subMenuSelectedOption < subMenuOptions.Length - 1)
            {
                subMenuSelectedOption++;
            }
            else if (subMenuKeyInfo.Key == ConsoleKey.Enter)
            {
                if (subMenuSelectedOption == 0)
                {
                    passwordSubMenuSelectedOption = 0; // Zurücksetzen auf Passwort Untermenü
                    PasswordSubMenu();
                }
                else if (subMenuSelectedOption == 1) //Accounts löschen
                {
                    PW_Manager();
                    Console.WriteLine("URL des zu löschenden Accounts eingeben: ");
                    string urlToDelete = Console.ReadLine();

                    AccountsManager accountsManager = new AccountsManager();
                    List<Account> accounts = accountsManager.GetAccounts();

                    bool accountDeleted = false;

                    for (int i = 0; i < accounts.Count; i++)
                    {
                        if (accounts[i].description == urlToDelete)
                        {
                            accounts.RemoveAt(i);
                            Console.WriteLine($"Account mit der URL '{urlToDelete}' wurde gelöscht.");
                            accountDeleted = true;
                            break;
                        }
                    }

                    if (!accountDeleted)
                    {
                        Console.WriteLine($"Kein Account mit der URL '{urlToDelete}' gefunden.");
                    }

                    accountsManager.SaveAccounts();

                    Console.ReadKey();
                }

                else if (subMenuSelectedOption == 2) //Accounts anzeigen
                {
                    PW_Manager();
                    AccountsManager accountsManager = new AccountsManager();
                    accountsManager.DisplayAccounts();
                    Console.ReadKey();
                }

                else if (subMenuSelectedOption == 3)
                {
                    // Zurück zum Hauptmenü
                    return;
                }
            }
        }
    }

    static void PasswordSubMenu()
    {
        string secretKey = "secretKey";
        string[] passwordSubMenuOptions = {"Passwort generieren", "Eigenes Passwort"};

        while (true)
        {
            PW_Manager();
            {
                string description = "";
                string username = "";
                string ownPassword = "";
                string randomPassword = "";
                string group = "";
                string defPassword = "";

                    //Accounts speichern
                
                Console.Write("URL: ");
                description = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("Beliebige Taste drücken um fortzufahren...");
                Console.ReadKey();
                PW_Manager();
                Console.Write("Gruppe eingeben: ");
                group = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("Beliebige Taste drücken um fortzufahren...");
                Console.ReadKey();
                PW_Manager();
                Console.Write("Benutzername: ");
                username = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("Beliebige Taste drücken um fortzufahren...");
                Console.ReadKey();
                PW_Manager();

                for (int i = 0; i < passwordSubMenuOptions.Length; i++)
                {
                    if (i == passwordSubMenuSelectedOption)
                    {
                        Console.WriteLine($"->  {passwordSubMenuOptions[i]}");
                    }
                    else
                    {
                        Console.WriteLine($"   {passwordSubMenuOptions[i]}");
                    }
                }
                
                ConsoleKeyInfo passwordSubMenuKeyInfo = Console.ReadKey();

                if (passwordSubMenuKeyInfo.Key == ConsoleKey.UpArrow && passwordSubMenuSelectedOption > 0)
                {
                    passwordSubMenuSelectedOption--;
                }
                else if (passwordSubMenuKeyInfo.Key == ConsoleKey.DownArrow && passwordSubMenuSelectedOption < passwordSubMenuOptions.Length - 1)
                {
                    passwordSubMenuSelectedOption++;
                }
                else if (passwordSubMenuKeyInfo.Key == ConsoleKey.Enter)
                {
                
                }

                if (passwordSubMenuSelectedOption == 0)
                {
                    PW_Manager();
                    int passwordLength = 12;

                    randomPassword = PW_Entry.RandomPassword(passwordLength);
                    Console.WriteLine($"Generiertes Passwort: {randomPassword}");
                    Console.WriteLine("");
                    Console.WriteLine("Beliebige Taste drücken um fortzufahren...");
                    Console.ReadKey();
                }
                else if (passwordSubMenuSelectedOption == 1)
                {
                    PW_Manager();
                    ownPassword = "";

                    while (ownPassword.Length < 12)
                    {
                        PW_Manager();
                        Console.Write("Eigenes Passwort eingeben: ");
                        ownPassword = Console.ReadLine();
                        if (ownPassword.Length < 12)
                        {
                            Console.WriteLine("Passwort zu kurz [min. 12]");
                            Console.WriteLine("");
                            Console.WriteLine("Beliebige Taste drücken zum wiederholen...");
                            Console.ReadKey();
                        }
                        else if (ownPassword.Length >= 12)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Beliebige Taste drücken um fortzufahren...");
                            Console.ReadKey();
                        }

                        PW_Manager();
                    }
                }

                if (randomPassword.Length == 0)
                {
                    defPassword = PW_Entry.Encrypt(ownPassword, secretKey);

                }
                else if (ownPassword.Length == 0)
                {
                    defPassword = PW_Entry.Encrypt(randomPassword, secretKey);
                }

                AccountsManager accountsManager = new AccountsManager();
                accountsManager.AddAccounts(description, username, defPassword, group);
                PW_Manager();

                Console.WriteLine();
                Console.ReadKey();
                PW_Manager();
                Console.WriteLine("Account wurde gespeichert");
                Console.WriteLine("");
                Console.WriteLine("Beliebige Taste drücken um zurückzukehren...");
                Console.ReadKey();
                // Zurück zum vorherigen Menü
                return;

            }
        }
    }
}
