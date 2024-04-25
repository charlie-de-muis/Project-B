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

private static void PrintMenu(List<MenuItem> menuItems)
{
    // Print menu items with index numbers
    for (int i = 0; i < menuItems.Count; i++)
    {
        var item = menuItems[i];
        Console.WriteLine($"{i + 1}. {item.Name}");
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
        Console.WriteLine("\nWould you like to filter the menu? (yes/no)");
        string filterChoice = Console.ReadLine();

        if (filterChoice.ToLower() == "yes")
        {
            // Prompt user for filter input
            Console.WriteLine("Enter an ingredient or dietary restriction to filter the menu:");
            string filterInput = Console.ReadLine();
            DisplayFilteredMenu(filterInput);
        }
        else
        {
            DisplayMenu();
        }
    }
}