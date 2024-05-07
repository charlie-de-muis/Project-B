public class Delete_Item
{
    public static void Delete_Items()
    {
        List<MenuItem> menuItems = JSON.ReadJSON();
        
        while (true)
        {
            MenuManager.DisplayMenu();

            Console.WriteLine("\nWhich item do you want to delete? Or type cancel.");
            string choice = Console.ReadLine();
            Program.ConsoleClear();
            Console.SetBufferSize(Console.WindowWidth, 100);

            if (choice.ToLower() == "cancel") {return;}
            
            if (!int.TryParse(choice, out int index))
            {
                Console.WriteLine("Invalid entry. Please enter a correct item index.\nPress enter to continue...");
                Console.ReadLine(); Program.ConsoleClear(); Console.SetBufferSize(Console.WindowWidth, 100);
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
            return menuItems;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Item number {index} not found");
            Console.WriteLine("Press enter to continue..."); Console.ReadLine(); Program.ConsoleClear(); Console.SetBufferSize(Console.WindowWidth, 100);
            return menuItems;
        }
    }
}
