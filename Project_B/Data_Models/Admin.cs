public class Admin : Account
{
    public bool IsAdmin {get;} = true;
    public Admin(string UserName, string PassWord, string Email) : base(UserName, PassWord, Email)
    {

    }
}