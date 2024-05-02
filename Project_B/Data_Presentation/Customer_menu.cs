public class Customer_Menu
{
    public static void CustomerMenu(Customer c)
    {
        while (true)
            {
                Console.WriteLine($"Welcome to the Customer menu {c.UserName}!");
                Console.WriteLine("1. View menu");
                Console.WriteLine("2. Make a reservation");
                Console.WriteLine("3. View Past Reservations");
                Console.WriteLine("4. Back to the main menu");

                Console.WriteLine("Enter your choice:");
                int choice = 0;
                try {choice = int.Parse(Console.ReadLine());}
                catch {Console.WriteLine("Invalid entry");}

                switch (choice)
                {
                    case 1:
                        MenuManager.ViewMenu();
                        break;
                    case 2:
                        ReservationSystem.ReservationMenu(c);
                        break;
                    case 3:
                        PreviousReservation.PreRes(c);
                        break;
                    case 4:
                        break; 
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                break;
            }
    }
}