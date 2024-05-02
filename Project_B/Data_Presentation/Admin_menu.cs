public class Admin_Menu
{
    public static void AdminMenu()
    {
        while (true)
            {
                Console.WriteLine("Welcome to Admin menu!");
                Console.WriteLine("1. Add menu items");
                Console.WriteLine("2. Delete menu items");
                // Console.WriteLine("3. View reservations");
                Console.WriteLine("4. Add restaurant info");
                Console.WriteLine("5. Back to the main menu");

                Console.WriteLine("Enter your choice:");
                int choice = 0;
                try {choice = int.Parse(Console.ReadLine());}
                catch {Console.WriteLine("Invalid entry");}

                switch (choice)
                {
                    case 1:
                        Add_Item.Add_Items();
                        break;
                    case 2:
                        Delete_Item.Delete_Items();
                        break;
                    case 3:
                        Console.WriteLine("View reservations");
                        // restaurantSystem.MakeReservation();
                        break;
                    case 4: 
                        Main_Menu.AboutTextAdmin();
                        break;
                    case 5:
                    break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                break;
            }
    }
}