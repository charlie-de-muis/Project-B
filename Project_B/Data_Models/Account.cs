public class Account
{
    public string UserName;
    public string PassWord;
    public string Email;
    public bool IsAdmin;

    public Account(string username, string password, string email)
    {
        this.UserName = username;
        this.PassWord = password;
        this.Email = email;
    } 

    public static object Option()
    {
        Console.WriteLine("Login or Create account?");
        string choice = Console.ReadLine().ToLower();
        bool flag = true;
        while (flag)
        {
            if (choice == "login")
            {
                int attemptsleft = 3;
                bool NextStep = false;

                while (NextStep == false && attemptsleft > 0)
                {
                    Console.WriteLine("Enter username:");
                    string username = Console.ReadLine();
                    Console.WriteLine($"Enter password ({attemptsleft} attempts remaining):");
                    string password = Console.ReadLine();
                    NextStep = Login(username, password);
                    attemptsleft--;
                    if (NextStep == true)
                    {
                        Account loggedInAccount = GetAccountByUsername(username);
                        if (loggedInAccount != null && loggedInAccount.IsAdmin)
                        {
                            return new Admin(loggedInAccount.UserName, loggedInAccount.PassWord, loggedInAccount.Email);
                        }
                        else
                        {
                            return new Customer(loggedInAccount.UserName, loggedInAccount.PassWord, loggedInAccount.Email);
                        }
                    }
                    else {Console.WriteLine("Wrong username and/or password\n");}
                }

                return null;
            }
            else if (choice == "create account")
            {
                Console.WriteLine("Enter an e-mail:");
                string email = Console.ReadLine();
                if (!email.Contains("@")){return "Invalid email";}
                Console.WriteLine("Enter a username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter a password:");
                string password = Console.ReadLine();
                if (MakeAccount(username, password, email)) {return "account created\n";}
            }
            else 
            {
                return "Wrong Input (login/create account)\n";
            };
        }
        return "unknown error in option method\n";
    }

    private static Account GetAccountByUsername(string username)
    {
        foreach (Account acc in CSV.ReadFromCSV())
        {
            if (acc.UserName == username)
            {
                return acc;
            }
        }
        return null;
    }

    private static bool MakeAccount(string username, string password, string email)
    {
        bool test_chars = true;
        foreach (char c in password)
        {
            if (!PasswordEncoding.Chars.Contains(c))
            {
                Console.WriteLine($"Creating account failed. Invalid character {c}");
                test_chars = false;
                break;
            }
        }
        if (test_chars == true)
        {
            password = PasswordEncoding.EncodeString(password);
            CSV.WriteToCSV(new Account(username, password, email));
            return true;
        }
        return false;
    }

    private static bool Login(string username, string password)
    {
        //Console.WriteLine(ReadFromCSV().Count);
        foreach (Account acc in CSV.ReadFromCSV())
        {
            //Console.WriteLine(acc.UserName);
            //Console.WriteLine(acc.PassWord);
            if (acc.UserName == username && PasswordEncoding.DecodeString(acc.PassWord) == password){return true;}
        }
        return false;
    }

    public static void CheckAdmin()
    {
        List<Account> info = CSV.ReadFromCSV();
        bool adminExists = false;
        foreach (Account profile in info)
        {
            if (profile.UserName == "Admin" && profile.PassWord == "SvEAF" && profile.Email == "admin@admin.com")
            {
                adminExists = true;
                break;
            }
        }

        if (!adminExists)
        {
            MakeAccount("Admin", "Admin", "admin@admin.com");
        }
    }
}