public class Delete_Item
{
    public static void Delete_Items()
    {
        // load the menu
        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current", false);
        
        while (true)
        {
            MenuManager.DisplayMenu();

            // ask which item needs to be deleted
            Console.WriteLine("\nWhich item do you want to delete? Enter an index or type cancel.");
            string choice = Console.ReadLine();
            Program.ConsoleClear();

            if (choice.ToLower() == "cancel") {return;}
            
            // check if the answer is valid
            if (!int.TryParse(choice, out int index))
            {
                Console.WriteLine("Invalid entry. Please enter a correct item index.\nPress enter to continue...");
                Console.ReadKey(); Program.ConsoleClear();
                continue;
            }
            
            menuItems = DeleteChosenItem(index, menuItems);
        }
    }

    public static List<MenuItem> DeleteChosenItem(int index, List<MenuItem> menuItems)
    {
        try
        {
            // remove the item from the menu
            int removed = menuItems.RemoveAll(menuItem => menuItem.ID == index);
            if (removed > 0)
            {
                // rewrite the menu
                JSON.DeletedItemsWriteJSON(menuItems, "Menu_current");
            }
            else
            {
                Console.WriteLine($"Item number {index} not found");
                Console.WriteLine("Press enter to continue..."); Console.ReadKey(); Program.ConsoleClear();
            }
            return menuItems;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("Press enter to continue..."); Console.ReadKey(); Program.ConsoleClear();
            return menuItems;
        }
    }
}
