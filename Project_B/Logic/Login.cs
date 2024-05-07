public class Log_in
{
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

                Program.ConsoleClear();
                while (NextStep == false && attemptsleft > 0)
                {
                    Console.WriteLine("Enter username:");
                    string username = Console.ReadLine();

                    Console.WriteLine($"\nEnter password ({attemptsleft} attempts remaining):");
                    string password = Console.ReadLine();

                    attemptsleft--;
                    if (Login(username, password) != null)
                    {
                        Account loggedInAccount = Login(username, password);
                        if (loggedInAccount != null && loggedInAccount.UserName == "Admin")
                        {
                            return new Admin(loggedInAccount.UserName, loggedInAccount.PassWord, loggedInAccount.Email);
                        }
                        else
                        {
                            return new Customer(loggedInAccount.UserName, loggedInAccount.PassWord, loggedInAccount.Email);
                        }
                    }
                    else {Program.ConsoleClear(); Console.WriteLine("Wrong username and/or password\n");}
                }

                return null;
            }
            else if (choice == "create account")
            {
                Program.ConsoleClear();
                Console.WriteLine("Enter an e-mail:");
                string email = Console.ReadLine();
                if (!email.Contains("@")){Console.WriteLine("Invalid email"); return null;}

                Console.WriteLine("\nEnter a username:");
                string username = Console.ReadLine();

                Console.WriteLine("\nEnter a password:");
                string password = Console.ReadLine();

                Program.ConsoleClear();
                if (Customer.MakeAccount(username, password, email)) {return new Customer(username, password, email);}
                else {Console.WriteLine("Press enter to continue..."); Console.ReadLine();}
            }
            else 
            {
                Program.ConsoleClear();
                Console.WriteLine("Wrong Input (login/create account)\nPress enter to continue..."); Console.ReadLine(); Program.ConsoleClear(); return null;
            };
        }
        Program.ConsoleClear();
        Console.WriteLine("unknown error in option method\nPress enter to continue..."); Console.ReadLine(); Program.ConsoleClear(); return null;
    }

    // private static Account GetAccountByUsername(string username)
    // {
    //     foreach (Account acc in CSV.ReadFromCSV())
    //     {
    //         if (acc.UserName == username)
    //         {
    //             return acc;
    //         }
    //     }
    //     return null;
    // }

    private static Account Login(string username, string password)
    {
        //Console.WriteLine(ReadFromCSV().Count);
        foreach (Account acc in CSV.ReadFromCSV())
        {
            //Console.WriteLine(acc.UserName);
            //Console.WriteLine(acc.PassWord);
            if (acc.UserName == username && PasswordEncoding.DecodeString(acc.PassWord) == password){return acc;}
        }
        return null;
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
            CSV.WriteToCSV(new Admin("Admin", PasswordEncoding.EncodeString("Admin"), "admin@admin.com"));
            //MakeAccount("Admin", "Admin", "admin@admin.com");
        }
    }
}
