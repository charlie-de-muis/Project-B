public class Account
{
    public string UserName;
    public string PassWord;
    public string Email;


    public Account(string username, string password, string email)
    {
        this.UserName = username;
        this.PassWord = password;
        this.Email = email;
    } 
}

// customer menu als een klant is ingelogd zodat ze een reservering kunnen maken