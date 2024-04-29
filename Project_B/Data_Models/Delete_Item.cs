public class Delete_Item
{
    public static void Delete_Items()
    {
        List<MenuItem> menuItems = JSON.ReadJSON();
        
        while (true)
        {
            MenuManager.DefaultPrintMenu(menuItems);

            Console.WriteLine("\nWhich item do you want to delete? Or type cancel.");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "cancel") {return;}
            
            if (!int.TryParse(choice, out int index) || index < 1 || index > menuItems.Count)
            {
                Console.WriteLine("Invalid entry. Please enter an item index or type 'cancel'.");
                continue;
            }

            menuItems = Delete_Item.DeleteChosenItem(index, menuItems);
        }
    }

    public static List<MenuItem> DeleteChosenItem(int index, List<MenuItem> menuItems)
    {
        try
        {
            menuItems.Remove(menuItems[index - 1]);
            JSON.DeletedItemsWriteJSON(menuItems);
            Console.WriteLine("Item Removed\n");
            return menuItems;
        }
        catch (ArgumentOutOfRangeException) {Console.WriteLine($"Item number {index} not found\n"); return menuItems; }
    }
}
