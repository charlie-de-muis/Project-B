class Main_Menu
{
    public static void MainMenu()
    {
        while (true)
        {
            Log_in.CheckAdmin();
            Console.WriteLine("Welcome to Restaurant Booking System!");
            Console.WriteLine("1. Make Account / Login");
            Console.WriteLine("2. View Menu");
            Console.WriteLine("3. About Restaurant");
            Console.WriteLine("4. Quit");
            Console.WriteLine("Enter your choice:");
            int choice = 0;
            try {choice = int.Parse(Console.ReadLine()); if(choice == 4){break;}}
            catch {Console.WriteLine("Invalid entry");}

            switch (choice)
            {
                case 1:
                    //Console.WriteLine(Log_in.Option());
                    DisplayCorrectMenu.DisplayMenu(Log_in.Option());
                    break;
                case 2:
                    Console.WriteLine("Menu");
                    MenuManager.ViewMenu();
                    break;
                case 3:
                    Console.WriteLine("About restaurant");
                    PrintAboutText();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public static void PrintAboutText()
    {
        string text = TXT.ReadFromTXT();
        if (text != null)
        {
            Console.WriteLine(text);
        }
    }

    public static void AboutTextAdmin()
    {
        Console.WriteLine("Type 3 lines of information about your restaurant:");
        TXT.WritetoTXT();
        TXT.WritetoTXT();
        TXT.WritetoTXT();
    }
}