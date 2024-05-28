public class Add_Item
{
    public static void Add_Items()
    {
        //List<MenuItem> Menu = JSON.ReadJSON();
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

            do 
            {
                do 
                {
                    Console.WriteLine("Enter the name of the menu item:");
                    Name = Console.ReadLine();
                    Program.ConsoleClear();
                    Check++;
                } while (Check < 1);

                do
                {
                    Console.WriteLine("Enter the all the ingredients seperated by a comma:");
                    Ingredients = Console.ReadLine().Split(",").ToList();
                    Program.ConsoleClear();
                    Check++;
                } while (Check < 2);

                do
                {
                    Console.WriteLine("Enter the price of the menu item:");
                    try { Price = Convert.ToDouble(Console.ReadLine().Replace(".",",")); Check++; }
                    catch { Program.ConsoleClear(); Console.WriteLine("Invalid entry..."); }
                } while (Check < 3);

                Program.ConsoleClear();
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

            Menu.Add(new MenuItem(GenerateID(), Name, Ingredients, Price, DietaryInfo));

            Program.ConsoleClear();
            Console.WriteLine("Do you want to add more items? Please type yes or no");
            string Answer = Console.ReadLine().ToLower();

            Program.ConsoleClear();
            if (Answer == "yes"){MoreItems = "yes";}
            else if (Answer == "no"){MoreItems = "no";}
            else {Console.WriteLine("Invalid answer.");}

        } while (MoreItems == "yes");

        JSON.WriteJSON(Menu, "Menu_current");
    }

    private static int GenerateID()
    {
        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current");
        return (menuItems.Any() ? menuItems.MaxBy(menuItem => menuItem.ID).ID : 0) + 1;
    }
}