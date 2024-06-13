public class Customer : Account
{
    public Customer(string UserName, string PassWord, string Email) : base(UserName, PassWord, Email)
    {
        
    }

    public static bool MakeAccount(string username, string password, string email)
    {
        bool test_chars = true;
        foreach (char c in password)
        {
            // check if all the characters in the file are valid
            if (!PasswordEncoding.Chars.Contains(c))
            {
                Console.WriteLine($"Creating account failed. Invalid character {c}");
                test_chars = false;
                break;
            }
        }
        // check for duplicate accounts
        if (EmailandNameExists(email, username))
        {
            Console.WriteLine($"Creating account failed. Email {email} and / or Name {username} already exists.");
            return false;
        }
        if (test_chars == true)
        {
            // endcode the given password and upload the new account to the file
            password = PasswordEncoding.EncodeString(password);
            CSV.WriteToCSV(new Customer(username, password, email), false);
            return true;
        }
        return false;
    }

    // check for duplicate accounts
    private static bool EmailandNameExists(string email, string userName)
    {
        List<Account> accounts = CSV.ReadFromCSV(false);
        if (accounts != null)
        {
            return accounts.Any(acc => acc.Email == email || acc.UserName == userName);
        }
        return false;
    }
}