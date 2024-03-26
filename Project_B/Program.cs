class Program
{
    public void MakeAccountOrLogin()
    {
        // Implementation for making account or login
    }

    public void ViewMenu()
    {
        // Implementation for viewing the menu
    }

    public void MakeReservation()
    {
        // Implementation for making a reservation
    }

    public void ViewPastReservations()
    {
        // Implementation for viewing past reservations
    }

    public void AdminOptions()
    {
        // Implementation for admin options
    }

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Welcome to Restaurant Booking System!");
            Console.WriteLine("1. Make Account / Login");
            // Console.WriteLine("2. View Menu");
            // Console.WriteLine("3. Make Reservation");
            // Console.WriteLine("4. View Past Reservations");
            // Console.WriteLine("5. Admin Options");
            Console.WriteLine("Enter your choice:");
            int choice = int.Parse(Console.ReadLine());
            if (choice != 1){Console.WriteLine("Please enter a valid choice");}

            switch (choice)
            {
                case 1:
                    Console.WriteLine(Account.Option());
                    break;
                case 2:
                    Console.WriteLine("Menu");
                    // restaurantSystem.ViewMenu();
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
                    Console.WriteLine("Admin account");
                    // restaurantSystem.AdminOptions();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
