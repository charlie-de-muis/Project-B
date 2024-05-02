public class DisplayCorrectMenu
{
    public static void DisplayMenu(object Acc)
    {
        if (Acc is string){Console.WriteLine(Acc);}
        else if (Acc is Admin){Admin_Menu.AdminMenu();}
        else if (Acc is Customer){Customer_Menu.CustomerMenu((Customer)Acc);}
        else {Console.WriteLine("Unknown error");}
    }
}