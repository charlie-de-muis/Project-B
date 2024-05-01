using System.Security.Cryptography.X509Certificates;

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

            //Choices: 1, 2, 4, 5 'can be chosen by both 'Admin and Customer''
            //Choice: 6 'can only be chosen by 'Admin''
            //Choice: 3 'can only be chosen by 'Customer''
            object result = null;
            switch (choice)
            {
                case 1:
                result = Account.Option();
                if (result is string)
                {
                    Console.WriteLine(result);
                }
                break;
                case 2:
                    Console.WriteLine("Menu");
                    DisplayMenu();
                    break;
                case 3:
                Console.WriteLine("Reservation");
                // Check if the logged-in user is a customer, if yes then access is granted for the reservation system
                if (IsCustomer(result))
                {
                    // Code here for reservations!! IDK
                }
                else
                {
                    Console.WriteLine("Only customers can make reservations.");
                }
                break;
                case 4:
                Console.WriteLine("Past reservations");
                // Check if the logged-in user is a customer, if yes then access is granted for viewing reservations
                if (IsCustomer(result))
                {
                    // Put reservation code here
                }
                else
                {
                    Console.WriteLine("Only customers can view past reservations.");
                }
                break;
                case 5:
                    Console.WriteLine("About restaurant");
                    PrintAboutText();
                    break;
                case 6:
                Console.WriteLine("Admin account");
                // Check if the logged-in user is an admin, if yes then grant access to the admin control pannel
                if (result is Admin)
                {
                    Admin_Menu.AdminMenu();
                }
                else
                {
                    Console.WriteLine("Only admins can access this.");
                }
                break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    public static void DisplayMenu()
    {
        // Read menu items from JSON
        List<MenuItem> menuItems = JSON.ReadJSON();

        // Print menu items
        foreach (var item in menuItems)
        {
            Console.WriteLine($"{item.Name}");
            Console.WriteLine("Ingredients: ");
            foreach (var ingredient in item.Ingredients)
            {
                Console.WriteLine($"- {ingredient}");
            }
            Console.WriteLine($"Price: ${item.Price}");
            Console.WriteLine("Dietary Info: ");
            foreach (var info in item.DietaryInfo)
            {
                Console.WriteLine($"- {info}");
            }
            Console.WriteLine();
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

    //this function is used to see if the user is a customer or not, if yes then it returns true, if no then false.
    private static bool IsCustomer(object result)
    {
        return result is Customer;
    }
}