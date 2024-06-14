// Made by Melvern

public class Delete_Item
{
    public static void Delete_Items()
    {
        string prompt = "Which menu are you editing?";
        string[] options = { "Current Menu", "Future Menu" };
        string menuType = ConsoleGUI.OptionGUI(prompt, options, 1) == 0 ? "Menu_current" : "Menu_future";

        // load the menu
        List<MenuItem> menuItems = JSON.ReadJSON(menuType, false);
        
        while (true)
        {
            MenuManager.DisplayMenu(menuType);

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
            
            menuItems = DeleteChosenItem(index, menuItems, menuType);
        }
    }

    public static List<MenuItem> DeleteChosenItem(int index, List<MenuItem> menuItems, string menuType, bool isTest = false)
    {
        try
        {
            // remove the item from the menu
            int removed = menuItems.RemoveAll(menuItem => menuItem.ID == index);
            if (removed > 0)
            {
                // rewrite the menu
                JSON.DeletedItemsWriteJSON(menuItems, menuType, isTest);
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
