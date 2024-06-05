public class MenuManager
{
    public static void DisplayMenu(string menuType)
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON(menuType);
        PrintMenu(menuItems);
    }

    public static void DisplayFilteredMenu(string filter, string menuType)
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON(menuType);
        var filteredMenu = FilterMenu(menuItems, filter);
        PrintMenu(filteredMenu);
    }

        public static void DisplaySortedMenu(string menuType)
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON(menuType);

        // Sort menu items by price, low to high
        menuItems = menuItems.OrderBy(item => item.Price).ToList();

        // Print the sorted menu
        PrintMenu(menuItems);
    }

    private static void PrintMenu(List<MenuItem> menuItems)
    {
        // Print menu items with index numbers and better readability
        for (int i = 0; i < menuItems.Count; i++)
        {
            var item = menuItems[i];
            // Display the index number and name of the menu item
            Console.WriteLine($"{item.ID}. {item.Name}");
            // Display the list of ingredients for the menu item
            Console.WriteLine("Ingredients: ");
            foreach (var ingredient in item.Ingredients)
            {
                Console.WriteLine($"- {ingredient}");
            }
            // Display the price of the menu item
            Console.WriteLine($"Price: ${item.Price}");
            // Display the dietary information for the menu item
            Console.WriteLine("Dietary Info: ");
            foreach (var info in item.DietaryInfo)
            {
                Console.WriteLine($"- {info}");
            }
            Console.WriteLine();
        }
    }

    private static List<MenuItem> FilterMenu(List<MenuItem> menuItems, string filter)
    {
        filter = filter.ToLower(); // Convert filter to lowercase for case-insensitive comparison
        // Filter menu items based on user input (ingredient or dietary restriction)
        var filteredItems = menuItems.Where(item =>
            item.Ingredients.Any(ingredient => ingredient.ToLower().Contains(filter)) ||
            item.DietaryInfo.Any(diet => diet.ToLower().Contains(filter)))
            .ToList();
        
        if (filteredItems.Count == 0)
        {
            Program.ConsoleClear();
            Console.WriteLine("No items match the filter input.");
            Console.WriteLine("Press enter to continue..."); Console.ReadLine(); Program.ConsoleClear();
        }

        return filteredItems;
    }

    public static void ViewMenu()
    {
        string prompt = "Choose a menu:";
        string[] options = { "current menu", "future menu" };
        int index = ConsoleGUI.OptionGUI(prompt, options);

        string menuType;
        if (index == 0) { menuType = "Menu_current"; }
        else { menuType = "Menu_future"; }

        Console.WriteLine("\nWould you like to filter or sort the menu? (yes/no)");
        string filterSortChoice = Console.ReadLine();

        if (filterSortChoice.ToLower() == "yes")
        {
            Program.ConsoleClear();
            Console.WriteLine("\nWould you like to filter based on ingredients or diet, or would you like to sort based on price? (filter/sort)");
            string filterSortInput = Console.ReadLine();

            if (filterSortInput.ToLower() == "filter")
            {
                // Prompt user for filter input
                Program.ConsoleClear();
                Console.WriteLine("Enter an ingredient or dietary restriction to filter the menu:");
                string filterInput = Console.ReadLine();
                
                Program.ConsoleClear();
                DisplayFilteredMenu(filterInput, menuType);
                Console.WriteLine("Press enter to continue..."); Console.ReadLine(); Program.ConsoleClear();
            }
            else if (filterSortInput.ToLower() == "sort")
            {
                // Sort and display the menu
                Program.ConsoleClear();
                DisplaySortedMenu(menuType);
                Console.WriteLine("Press enter to continue..."); Console.ReadLine(); Program.ConsoleClear();
            }
            else
            {
                Program.ConsoleClear();
                Console.WriteLine("Invalid input. Please enter 'filter' or 'sort'.");
                Console.WriteLine("Press enter to continue..."); Console.ReadLine(); Program.ConsoleClear();
            }
        }
        else
        {
            Program.ConsoleClear();
            DisplayMenu(menuType);
            Console.WriteLine("Press enter to continue..."); Console.ReadLine(); Program.ConsoleClear();
        }
    }

    public static void ManageAllMenus()
    {
        bool exit = false;

        while (!exit)
        {
            Program.ConsoleClear();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Menu Management System.               ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Upload Current Menu                ║");
            Console.WriteLine("║ 2. Upload Future Menu                 ║");
            Console.WriteLine("║ 3. Switch Between Menu's              ║");
            Console.WriteLine("║ 4. View Past Menu's                   ║");
            Console.WriteLine("║ 5. Delete Menu's                      ║");
            Console.WriteLine("║ 6. Exit                               ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║ Enter your choice:                    ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");

            string filterSortChoice = Console.ReadLine();

            switch (filterSortChoice)
            {
                case "1":
                    Program.ConsoleClear();
                    UploadMenu("current");
                    break;
                case "2":
                    Program.ConsoleClear();
                    UploadMenu("future");
                    break;
                case "3":
                    Program.ConsoleClear();
                    SwitchMenu();
                    break;
                case "4":
                    Program.ConsoleClear();
                    ViewPastMenus();
                    Console.WriteLine("\nPress any key to return..."); Console.ReadKey();
                    break;
                case "5":
                    Program.ConsoleClear();
                    DeleteMenu();
                    break;
                case "6":
                    Program.ConsoleClear();
                    Console.WriteLine("Exiting...");
                    exit = true;
                    break;
                default:
                    Program.ConsoleClear();
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    private static void UploadMenu(string menuType)
    {
        Console.WriteLine($"Uploading {menuType} Menu...");
        Console.WriteLine("Please enter the name of the JSON file you uploaded:");
        string fileName = Console.ReadLine();

        JSON.SetMenuAs(menuType, fileName);
    }

    private static void SwitchMenu()
    {
        Console.WriteLine("Switching Between Menus...");
        ViewPastMenus();

        Console.WriteLine("Please enter the name of the menu file you want to set as current:");
        string menuFileName = Console.ReadLine();
        Console.WriteLine("\nPlease enter current or future of which you want to set it to:");
        string menuType = Console.ReadLine().ToLower();
        
        if (menuType == "current" || menuType == "future")
        {
            JSON.SwitchMenu(menuFileName, menuType);
        }
        else
        {
            Console.WriteLine("Invalid menu type, set the menu type to current or future");
            Console.WriteLine("Press any key to continue..."); Console.ReadKey();
        }
    }

    private static void ViewPastMenus()
    {
        Console.WriteLine("Viewing Past Menus...");
        List<string> files = JSON.ReadMenusJSON();

        if (files.Count == 0)
        {
            Console.WriteLine("No menus found.");
        }
        else
        {
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
    }

    private static void DeleteMenu()
    {
        Console.WriteLine("Deleting Menus...");
        ViewPastMenus();

        Console.WriteLine("\nPlease enter the name of the menu file you want to delete:");
        string menuFileName = Console.ReadLine();
        
        JSON.DeleteMenu(menuFileName);
    }
}