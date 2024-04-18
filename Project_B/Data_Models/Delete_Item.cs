public class Delete_Item
{
    public static void Delete_Items()
    {
        List<MenuItem> menuItems = JSON.ReadJSON();
        
        while (true)
        {
            Main_Menu.DisplayMenu();

            Console.WriteLine("\nWhich item do you want to delete? Or type cancel.");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "cancel") {return;}
            
            if (!int.TryParse(choice, out int index) || index < 1 || index > menuItems.Count)
            {
                Console.WriteLine("Invalid entry. Please enter an item index or type 'cancel'.");
                continue;
            }
            
            try
            {
                menuItems.Remove(menuItems[index - 1]);
                JSON.DeletedItemsWriteJSON(menuItems);
                Console.WriteLine("Item Removed\n");
            }
            catch (ArgumentOutOfRangeException) {Console.WriteLine("Item number not found\n");}
        }
    }
}
