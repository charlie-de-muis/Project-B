class Main_Menu
{
    public static void MainMenu()
    {
        while (true)
        {
            Account.CheckAdmin();
            Console.WriteLine("Welcome to Restaurant Booking System!");
            Console.WriteLine("1. Make Account / Login");
            Console.WriteLine("2. View Menu");
            // Console.WriteLine("3. Make Reservation");
            // Console.WriteLine("4. View Past Reservations");
            Console.WriteLine("5. About Restaurant");
            Console.WriteLine("6. Admin Options");
            Console.WriteLine("Enter your choice:");
            int choice = 0;
            try {choice = int.Parse(Console.ReadLine());}
            catch {Console.WriteLine("Invalid entry");}

            switch (choice)
            {
                case 1:
                    Console.WriteLine(Account.Option());
                    break;
                case 2:
                    Console.WriteLine("Menu");
                    MenuManager.ViewMenu();
                    break;
                case 3:
                    Console.WriteLine("Reservation");
                    // restaurantSystem.MakeReservation();
                    break;
                case 4:
                    Console.WriteLine("Past reservations");
                    // restaurantSystem.ViewPastReservations();
                    break;
                case 5:
                    Console.WriteLine("About restaurant");
                    PrintAboutText();
                    break;
                case 6:
                    Console.WriteLine("Admin account");
                    // restaurantSystem.AdminOptions();
                    Admin_Menu.AdminMenu();
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