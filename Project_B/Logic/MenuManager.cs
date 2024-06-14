// Made by Bente & Orestis
public class MenuManager
{
    public static void DisplayMenu(string menuType, bool isTest = false)
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON(menuType, isTest);
        PrintMenu(menuItems);
    }

    public static void DisplayFilteredMenu(string filter, string menuType)
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON(menuType, false);
        var filteredMenu = FilterMenu(menuItems, filter);
        PrintMenu(filteredMenu);
    }

        public static void DisplaySortedMenu(string menuType, bool isTest = false)
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON(menuType, isTest);

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
            item.Ingredients.Any(ingredient => ingredient.ToLower() == (filter)) ||
            item.DietaryInfo.Any(diet => diet.ToLower() == (filter)))
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
        int index = ConsoleGUI.OptionGUI(prompt, options, 1);

        string menuType;
        if (index == 0) { menuType = "Menu_current"; }
        else { menuType = "Menu_future"; }

        string prompt2 = "Would you like to filter or sort the menu? (yes / no)";
        string[] options2 = { "yes", "no" };
        int index2 = ConsoleGUI.OptionGUI(prompt2, options2, 1);

        if (index2 == 0)
        {
            string prompt3 = "Would you like to filter based on ingredients or diet, or would you like to sort based on price?";
            string[] options3 = { "filter", "sort" };
            int index3 = ConsoleGUI.OptionGUI(prompt3, options3, 1);

            if (index3 == 0)
            {
                // Prompt user for filter input
                Program.ConsoleClear();
                Console.WriteLine("Enter an ingredient or dietary restriction to filter the menu:");
                string filterInput = Console.ReadLine();
                
                Program.ConsoleClear();
                DisplayFilteredMenu(filterInput, menuType);
                Console.WriteLine("Press enter to return..."); Console.ReadLine(); Program.ConsoleClear();
            }
            else
            {
                // Sort and display the menu
                Program.ConsoleClear();
                DisplaySortedMenu(menuType);
                Console.WriteLine("Press enter to return..."); Console.ReadLine(); Program.ConsoleClear();
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
            string prompt = "╔═══════════════════════════════════════╗\n║ Menu Management System.               ║\n╠═══════════════════════════════════════╣";
            string[] options = { "Upload Current Menu", "Upload Future Menu", "Switch Between Menu's", "View Past Menu's", "Delete Menu's", "Exit" };
            int choice = ConsoleGUI.OptionGUI(prompt, options, 2);

            switch (choice)
            {
                case 0:
                    Program.ConsoleClear();
                    UploadMenu("Menu_current");
                    break;
                case 1:
                    Program.ConsoleClear();
                    UploadMenu("Menu_future");
                    break;
                case 2:
                    Program.ConsoleClear();
                    SwitchMenu();
                    break;
                case 3:
                    Program.ConsoleClear();
                    ViewPastMenus();
                    Console.WriteLine("\nPress any key to return..."); Console.ReadKey();
                    break;
                case 4:
                    Program.ConsoleClear();
                    DeleteMenu();
                    break;
                case 5:
                    Program.ConsoleClear();
                    Console.WriteLine("Exiting...");
                    exit = true;
                    break;
            }
        }
    }

    private static void UploadMenu(string menuType)
    {
        Console.WriteLine($"Uploading {menuType}...");
        Console.WriteLine("Please enter the name of the JSON file you uploaded (with .json added):");
        string fileName = Console.ReadLine();

        JSON.SetMenuAs(menuType, fileName);
    }

    private static void SwitchMenu()
    {
        Console.WriteLine("Switching Between Menus...");
        ViewPastMenus();

        Console.WriteLine("Please enter the name of the menu file you want to set as current (with .json added):");
        string menuFileName = Console.ReadLine();
        string prompt = "\nPlease enter current or future of which you want to set it to:";
        string[] options = { "Current", "Future" };
        int index = ConsoleGUI.OptionGUI(prompt, options, 1);
        
        JSON.SwitchMenu(menuFileName, index == 0 ? "Menu_current" : "Menu_future");
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

        Console.WriteLine("\nPlease enter the name of the menu file you want to delete (with .json added):");
        string menuFileName = Console.ReadLine();
        
        JSON.DeleteMenu(menuFileName);
    }
}