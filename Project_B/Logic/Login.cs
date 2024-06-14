// Made by Orestis

public class Log_in
{
    public static object Option()
    {
        string prompt = "Login or Create account?";
        string[] options = { "Login", "Create Account" };
        int index = ConsoleGUI.OptionGUI(prompt, options, 1);

        bool flag = true;
        while (flag)
        {
            if (index == 0)
            {
                int attemptsleft = 3;
                bool NextStep = false;

                Program.ConsoleClear();
                while (NextStep == false && attemptsleft > 0)
                {
                    // ask for the username and password
                    Console.WriteLine("Enter username:");
                    string username = Console.ReadLine();

                    Console.WriteLine($"\nEnter password ({attemptsleft} attempts remaining):");
                    string password = Console.ReadLine();

                    attemptsleft--;
                    Tuple<string,string> data = new Tuple<string, string>(username,password);
                    if (Login(data) != null)
                    {
                        // check if username and password are correct, and return the correct menu
                        Account loggedInAccount = Login(data);
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
            else
            {
                // ask for all the necessary data
                Program.ConsoleClear();
                Console.WriteLine("Enter an e-mail:");
                string email = Console.ReadLine();
                if (!email.Contains("@")){Console.WriteLine("Invalid email"); return null;}

                Console.WriteLine("\nEnter a username:");
                string username = Console.ReadLine();

                Console.WriteLine("\nEnter a password:");
                string password = Console.ReadLine();

                Program.ConsoleClear();

                // create a new account
                if (Customer.MakeAccount(username, password, email)) {return new Customer(username, password, email);}
                else {Console.WriteLine("Press enter to continue..."); Console.ReadLine();}
            }
        }
        Program.ConsoleClear();
        Console.WriteLine("unknown error in option method\nPress enter to continue..."); Console.ReadLine(); Program.ConsoleClear(); return null;
    }

    private static Account Login(Tuple<string, string> login)
    {
        string username = login.Item1;
        string password = login.Item2;

        foreach (Account acc in CSV.ReadFromCSV(false))
        {
            // if there is an account with the given info
            if (acc.UserName == username && PasswordEncoding.DecodeString(acc.PassWord) == password){return acc;}
        }
        return null;
    }

    public static void CheckAdmin()
    {
        // check if the logged in account is from the admin
        List<Account> info = CSV.ReadFromCSV(false);
        bool adminExists = false;
        foreach (Account profile in info)
        {
            if (profile.UserName == "Admin" && profile.PassWord == "SvEAF" && profile.Email == "admin@admin.com")
            {
                adminExists = true;
                break;
            }
        }

        // create an admin account if it doesn't exist
        if (!adminExists)
        {
            CSV.WriteToCSV(new Admin("Admin", PasswordEncoding.EncodeString("Admin"), "admin@admin.com"), false);
        }
    }
}