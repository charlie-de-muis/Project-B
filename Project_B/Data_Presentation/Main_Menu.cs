class Main_Menu
{
    public static void MainMenu()
    {
        while (true)
        {
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
                    DisplayMenu();
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
        Console.WriteLine("Welcome to [Restaurant Name], your sustainable dining destination in Rotterdam! Our mission is simple: to provide delicious meals made from locally-sourced ingredients, supporting our community and minimizing our environmental impact.");
        Console.WriteLine("Led by founder Jake Darcy, our restaurant offers a diverse menu with options for meat lovers, seafood enthusiasts, vegetarians, and vegans. Our cozy space features intimate tables for two, group-friendly seating, and a vibrant bar area.");
        Console.WriteLine("Located at Wijnhaven 107, 3011 WN, [Restaurant Name] invites you to indulge in our 2, 3, and 4-course dining options, paired with our carefully curated wine selections. Join us for a memorable culinary experience that celebrates Rotterdam's flavors and sustainability efforts. See you soon at [Restaurant Name]!");
    }
}