public abstract class Add_Item
{
    public static void Add_Items()
    {
        List<MenuItem> Menu = new();
        string MoreItems = "yes";

        do
        {
            Console.WriteLine("Please enter all the details about the new menu item.");

            // to make sure all the items are filled in correctly
            int Check = 0;

            string Name = "Unknown";
            List<string> Ingredients = new();
            double Price = 0;
            List<string> DietaryInfo = new();

            // get all the info about the new menu item
            do 
            {
                // name
                do 
                {
                    Console.WriteLine("Enter the name of the menu item:");
                    Name = Console.ReadLine();
                    Program.ConsoleClear();
                    Check++;
                } while (Check < 1);

                // ingredients
                do
                {
                    Console.WriteLine("Enter the all the ingredients seperated by a comma:");
                    Ingredients = Console.ReadLine().Split(",").ToList();
                    Program.ConsoleClear();
                    Check++;
                } while (Check < 2);

                // price
                do
                {
                    Console.WriteLine("Enter the price of the menu item:");
                    try { Price = Convert.ToDouble(Console.ReadLine().Replace(".",",")); Check++; }
                    catch { Program.ConsoleClear(); Console.WriteLine("Invalid entry..."); }
                } while (Check < 3);

                Program.ConsoleClear();

                // dietary info
                Console.WriteLine(@"Enter the dietary information. Please seperate them by a comma.
If the dish doesn't fit these options, enter x.
These are all the options:
- vegetarian
- vegan
- glutenfree
- dairy free");

                string Diet = Console.ReadLine();
                Program.ConsoleClear();
                if (Diet.ToLower() == "x"){DietaryInfo = new(){"No dietary restrictions."};}
                else {DietaryInfo = Diet.Split(",").ToList();}
                Check ++;

            } while (Check < 4);

            // add the item to the menu
            Menu.Add(new MenuItem(GenerateID(), Name, Ingredients, Price, DietaryInfo));

            Program.ConsoleClear();

            // ask if the admin wants to add more items
            Console.WriteLine("Do you want to add more items? Please type yes or no");
            string Answer = Console.ReadLine().ToLower();

            Program.ConsoleClear();
            if (Answer == "yes"){MoreItems = "yes";}
            else if (Answer == "no"){MoreItems = "no";}
            else {Console.WriteLine("Invalid answer.");}

        } while (MoreItems == "yes");

        JSON.WriteJSON(Menu, "Menu_current", false);
    }

    // generate menu item ID
    private static int GenerateID()
    {
        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current", false);
        return (menuItems.Any() ? menuItems.MaxBy(menuItem => menuItem.ID).ID : 0) + 1;
    }
}