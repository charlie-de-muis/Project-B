public class MenuManager
{
    public static void DisplayMenu()
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON();
        PrintMenu(menuItems);
    }

    public static void DisplayFilteredMenu(string filter)
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON();
        var filteredMenu = FilterMenu(menuItems, filter);
        PrintMenu(filteredMenu);
    }

        public static void DisplaySortedMenu()
    {
        // Read menu items from JSON
        var menuItems = JSON.ReadJSON();

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
            Console.WriteLine($"{i + 1}. {item.Name}");
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
            Console.WriteLine("No items match the filter input.");
        }

        return filteredItems;
    }

    public static void ViewMenu()
    {
        Console.WriteLine("\nWould you like to filter or sort the menu? (yes/no)");
        string filterSortChoice = Console.ReadLine();

        if (filterSortChoice.ToLower() == "yes")
        {
            Console.WriteLine("\nWould you like to filter based on ingredients or diet, or would you like to sort based on price? (filter/sort)");
            string filterSortInput = Console.ReadLine();

            if (filterSortInput.ToLower() == "filter")
            {
                // Prompt user for filter input
                Console.WriteLine("Enter an ingredient or dietary restriction to filter the menu:");
                string filterInput = Console.ReadLine();
                DisplayFilteredMenu(filterInput);
            }
            else if (filterSortInput.ToLower() == "sort")
            {
                // Sort and display the menu
                DisplaySortedMenu();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'filter' or 'sort'.");
            }
        }
        else
        {
            DisplayMenu();
        }
    }
}