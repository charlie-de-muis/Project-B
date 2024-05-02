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
            if (!PasswordEncoding.Chars.Contains(c))
            {
                Console.WriteLine($"Creating account failed. Invalid character {c}");
                test_chars = false;
                break;
            }
        }
        if (EmailExists(email))
        {
            Console.WriteLine($"Creating account failed. Email {email} already exists.");
            return false;
        }
        if (test_chars == true)
        {
            password = PasswordEncoding.EncodeString(password);
            CSV.WriteToCSV(new Customer(username, password, email));
            return true;
        }
        return false;
    }

    private static bool EmailExists(string email)
    {
        List<Account> accounts = CSV.ReadFromCSV();
        if (accounts != null)
        {
            return accounts.Any(acc => acc.Email == email);
        }
        return false;
    }
}