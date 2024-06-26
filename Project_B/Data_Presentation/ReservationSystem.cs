// Made by Melvern & parts by Olivier

public static class ReservationSystem
{
    private static string DateSelect;
    private static string TimeSlot;
    private static int AmountofPersons;
    private static List<int> TableChoices;
    private static Account Customer;
    private static List<int> menuOrders;
    private const double DISCOUNT_RATE = 0.10;

    public static void ReservationMenu(Account account)
    {
        Customer = account;

        while (true)
        {
            string prompt = $"╔═══════════════════════════════════════╗\n║ Want to make a reservation?           ║\n╠═══════════════════════════════════════╣";
            string[] options = { "Display The Tables", "Make A Reservation", "Return To The Main Menu" };
            int choice = ConsoleGUI.OptionGUI(prompt, options, 2);

            switch (choice)
            {
                case 0: 
                    Console.WriteLine(Table.DisplayTables(new List<Table>()));
                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine(); Program.ConsoleClear(); continue;
                case 1: MakeReservation(); continue;
                case 2: return;
            }
        }
    }

    public static void MakeReservation()
    {
        List<Reservation> reservations = CSV.ReadFromCSVReservations("Reservation.csv", false);

        //CUSTOMER SELECTS ON WHICH DATE HE/SHE WANTS TO RESERVE.
        Console.WriteLine("For which day do you want to book?");
        while (true)
        {
            DateSelect = "";
            AmountofPersons = 0;
            TableChoices = new List<int>();
            //List<int> menuOrders; (all the orders chosen from the menu) (NEXT SPRINT)
            //More variables coming?

            //First the sure selects on of the future dates to book.
            Console.WriteLine("Or type 'cancel' to cancel the reservation.\n");
            Console.WriteLine($"1.  {DateTime.Now.AddDays(1):dd-MM-yyyy}");
            Console.WriteLine($"2.  {DateTime.Now.AddDays(2):dd-MM-yyyy}");
            Console.WriteLine($"3.  {DateTime.Now.AddDays(3):dd-MM-yyyy}");
            Console.WriteLine($"4.  {DateTime.Now.AddDays(4):dd-MM-yyyy}");
            Console.WriteLine($"5.  {DateTime.Now.AddDays(5):dd-MM-yyyy}");
            Console.WriteLine($"6.  {DateTime.Now.AddDays(6):dd-MM-yyyy}");
            Console.WriteLine($"7.  {DateTime.Now.AddDays(7):dd-MM-yyyy}");
            Console.WriteLine($"8.  {DateTime.Now.AddDays(8):dd-MM-yyyy}");
            Console.WriteLine($"9.  {DateTime.Now.AddDays(9):dd-MM-yyyy}");
            Console.WriteLine($"10. {DateTime.Now.AddDays(10):dd-MM-yyyy}");
            Console.WriteLine($"11. {DateTime.Now.AddDays(11):dd-MM-yyyy}");
            Console.WriteLine($"12. {DateTime.Now.AddDays(12):dd-MM-yyyy}");
            Console.WriteLine($"13. {DateTime.Now.AddDays(13):dd-MM-yyyy}");
            Console.WriteLine($"14. {DateTime.Now.AddDays(14):dd-MM-yyyy}");

            string entry = Console.ReadLine().Replace(" ", "");
            Program.ConsoleClear();
            string[] availableDates = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
            if (availableDates.Contains(entry)) { DateSelect = DateTime.Now.AddDays(int.Parse(entry)).ToString("dd-MM-yyyy"); }
            else if (entry == "cancel") { return; }
            else { Console.WriteLine("Invalid entry, for which day do you want to book?"); continue; }
            
            //The following code has been organized for readability, comments are in the methods.
            while (true)
            {
                string resultTimeSlot = SelectTimeSlot();
                if (resultTimeSlot == "cancel") { return; }
                if (resultTimeSlot == "select date") { break; }

                string resultCustomerCount = SelectCustomerCount();
                if (resultCustomerCount == "cancel") { return; }
                if (resultCustomerCount == "select date") { break; }
                if (resultCustomerCount == "select timeslot") { continue; }

                string resultTables = SelectTables(reservations);
                if (resultTables == "cancel") { return; }
                if (resultTables == "select date") { break; }
                if (resultTables == "no tables") { continue; }

                string resultItems = SelectItems();
                if (resultItems == "cancel") { return; }
                if (resultItems == "select date") { break; }
                if (resultItems == "select timeslot") { continue; }

                string resultReceipt = PrintReceipt(reservations);
                if (resultReceipt == "main menu") { return; }
                if (resultReceipt == "select date") { break; }
                if (resultReceipt == "select timeslot") { continue; }
            }
        }
    }

    private static string SelectTimeSlot()
    {
        //Customer selects which time of day he/she wants to reserve.
        Console.WriteLine("For what timeslot do you want to book? (Brunch, Lunch, Afternoon, Dinner)");
        while (true)
        {
            Console.WriteLine("Type 'select date' to reserve on a different date.");
            Console.WriteLine("Or type 'cancel' to cancel the reservation.");
            TimeSlot = Console.ReadLine().ToLower();
            Program.ConsoleClear();
            if (TimeSlot == "brunch" ||
                TimeSlot == "lunch" ||
                TimeSlot == "afternoon" ||
                TimeSlot == "dinner" ||
                TimeSlot == "cancel" ||
                TimeSlot == "select date") { return TimeSlot; }
            else
            {
                Console.WriteLine("Invalid entry, for what timeslot do you want to book? (Brunch, Lunch, Afternoon, Dinner)");
            }
        }
    }

    private static string SelectCustomerCount()
    {
        //Customer specifiec for how many people will be reserved.
        Console.WriteLine("For how many people do you order? (1 - 16)");
        while (true)
        {
            Console.WriteLine("Type 'select date' or 'select timeslot' to reserve on a different date or timeslot.");
            Console.WriteLine("Or type 'cancel' to cancel the reservation.");
            string choice = Console.ReadLine().ToLower();
            Program.ConsoleClear();
            if (choice == "cancel" || choice == "select date" || choice == "select timeslot") { return choice; }
            else
            {
                bool result = int.TryParse(choice.Replace(" ", ""), out int totalPersons);
                if (result == false)
                {
                    Console.WriteLine("Invalid entry, for how many people do you order? (1 - 16)");
                }
                else if (totalPersons < 1 || totalPersons > 16 )
                {
                    Console.WriteLine($"{totalPersons} is not within this range (1 - 16)");
                    Console.WriteLine("For how many people do you order?");
                }
                else { AmountofPersons = totalPersons; return choice; }
            }
        }
    }

    private static string SelectTables(List<Reservation> reservations)
    {
        //Customer selects a table/multiple tables.
        //This code will check if the tables will be available depending on a chosen timeslot.
        //This code will check if the tables will have enough seats for the amount of people the customer has specified.
        List<Table> tables = Table.GetTableInfo();

        foreach (Table table in tables) { table.GetTableReservation(reservations, DateSelect, TimeSlot); }
        bool anyAvailableTable = tables.Any(table => table.IsAvailable);

        while (true)
        {
            bool count = true;

            if (anyAvailableTable)
            {
                Console.WriteLine(Table.DisplayTables(tables));
            }
            else
            {
                Console.WriteLine($"Sorry, {TimeSlot} is fully booked on this date, please select a different timeslot or date.");
                Console.WriteLine("Press enter to continue."); Console.ReadLine(); Program.ConsoleClear(); return "no tables";
            }

            Console.WriteLine("\ntables marked X are already booked");

            Console.WriteLine("\nSelect a single table by its number.");
            Console.WriteLine("Select multiple tables for merging by their numbers, comma seperated. (up to 16 people)");
            Console.WriteLine("Type 'select date' to reserve on a different date.");
            Console.WriteLine("Or type 'cancel' to cancel the reservation.");

            string entryTables = Console.ReadLine();
            Program.ConsoleClear();

            if (entryTables == "cancel" || entryTables == "select date") { return entryTables; }

            List<string> entries = entryTables.Split(',').ToList();
            List<string> TableChoicesSTR = new List<string>();
            foreach (string number in entries) { TableChoicesSTR.Add(number.Replace(" ", "")); }

            List<string> tableNumbers = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
            foreach (string number in TableChoicesSTR) { if (!tableNumbers.Contains(number)) { Console.WriteLine("Invalid entry, please try again."); count = false; break; } }

            if (count)
            {
                bool isSuccess = GetTables(TableChoicesSTR, tables);
                if (isSuccess) { return entryTables; }
            }
        }
    }

    public static bool GetTables(List<string> TableChoicesSTR, List<Table> tables)
    {
        TableChoices = new List<int>();
        foreach (string number in TableChoicesSTR) { TableChoices.Add(int.Parse(number)); }

        //Single table chosen.
        if (TableChoices.Count == 1)
        {
            if (!tables[TableChoices[0] - 1].IsAvailable)
            {
                Console.WriteLine($"Table {TableChoices[0]} is already booked during {TimeSlot}.");
            }
            else if (AmountofPersons > tables[TableChoices[0] - 1].TotalSeats)
            {
                Console.WriteLine($"Table {TableChoices[0]} does not have enough seats for {AmountofPersons} people.\n");
            }
            else
            {
                Console.WriteLine($"You have selected table {TableChoices[0]}\n");
                return true;
            }
        }
        //Multiple tables chosen.
        else if (TableChoices.Count > 1)
        {
            bool availableTables = true;
            foreach (int number in TableChoices)
            {
                if (!tables[number - 1].IsAvailable)
                {
                    Console.WriteLine($"Table {number} is already booked during {TimeSlot}.");
                    availableTables = false;
                }
            }

            //After checking table availablility, the amount of seats must be higher than the number of people they reserve for.
            if (availableTables)
            {
                string tablesMerge = string.Join(", ", TableChoices);
                int mergedSeats = 0;
                foreach (int number in TableChoices) { mergedSeats += tables[number - 1].TotalSeats; }

                if (AmountofPersons > mergedSeats)
                {
                    Console.WriteLine($"Tables '{tablesMerge}' do not have enough seats for {AmountofPersons} people.\n");
                }
                else
                {
                    Console.WriteLine($"You have selected/merged tables '{tablesMerge}' for reservation.\n");
                    return true;
                }
            }
        }

        return false;
    }

    private static string SelectItems()
    {
        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current", false);
        if (!menuItems.Any())
        {
            Console.WriteLine("No menu or menu items available at the moment.\nPress any key to continue.");
            Console.ReadKey();
            return "select date";
        }
        List<int> itemListID = menuItems.Select(menuItems => menuItems.ID).ToList();

        menuOrders = new List<int>();
        int selectedItem = 0;
        int selectByIndex = 0;

        while (true)
        {
            if (selectedItem > menuOrders.Count - 1) { selectedItem = menuOrders.Count - 1; }
            Program.ConsoleClear();
            MenuManager.DisplayMenu("Menu_current");
            Console.WriteLine();
            Console.WriteLine($"Select the items you would like to order > {itemListID[selectByIndex]}");
            Console.WriteLine();
            Console.Write("Selections: ");
            for (int i = 0; i < menuOrders.Count; i++)
            {
                if (i == selectedItem)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write($"{menuOrders[i]} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"{menuOrders[i]} ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Esc.   c (confirm items).   ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("d (select date).   t (select timeslot).   ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("▲   ▼ (find item).   Enter (add item).   ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("►   ◄ (select item).   Backspace (remove item).");
            Console.ResetColor();

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Backspace) { if (menuOrders.Count > 0 && selectedItem >= 0) { menuOrders.RemoveAt(selectedItem); if (selectedItem > 0) { selectedItem--; } } }
            else if (keyInfo.Key == ConsoleKey.Enter) { if (selectByIndex < itemListID.Count) { menuOrders.Add(itemListID[selectByIndex]); } }

            else if (keyInfo.Key == ConsoleKey.UpArrow) { if (selectByIndex > 0) { selectByIndex--; } }
            else if (keyInfo.Key == ConsoleKey.DownArrow) { if (selectByIndex < itemListID.Count - 1) { selectByIndex++; } }
            else if (keyInfo.Key == ConsoleKey.LeftArrow) { if (selectedItem > 0) { selectedItem--; } }
            else if (keyInfo.Key == ConsoleKey.RightArrow) { if (selectedItem < menuOrders.Count - 1) { selectedItem++; } }

            else if (keyInfo.Key == ConsoleKey.Escape) { return "cancel"; }
            else if (keyInfo.Key == ConsoleKey.D) { return "select date"; }
            else if (keyInfo.Key == ConsoleKey.T) { return "select timeslot"; }
            else if (keyInfo.Key == ConsoleKey.C && menuOrders.Count > 0) { break; }

            else { continue; }
        }

        return "order completed";
    }

    private static string PrintReceipt(List<Reservation> reservations)
    {
        string TableChoicesSTR = string.Join(", ", TableChoices);
        List<int> stringLengths = new List<int>() { TimeSlot.Length, TableChoicesSTR.Length };
        int spacing = Math.Max(30, stringLengths.Max());
        int width = 45;

        if (spacing > 30) { width -= spacing - 30; }

        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current", false);
        Dictionary<int, int> orderCounts = menuOrders.GroupBy(id => id).ToDictionary(group => group.Key, group => group.Count());
        ICollection<int> keys = orderCounts.Keys;

        //Final menu which displays their selections and what they can change, or they confirm of course.
        while (true)
        {
            Program.ConsoleClear();
            Console.WriteLine("This is what you have selected:");
            Console.WriteLine();
            Console.WriteLine("{0, -" + width + "} {1}", "Booking Date:", DateSelect.PadLeft(spacing));
            Console.WriteLine("{0, -" + width + "} {1}", "TimeSlot:", TimeSlot.PadLeft(spacing));
            Console.WriteLine("{0, -" + width + "} {1}", "Total Customers:", AmountofPersons.ToString().PadLeft(spacing));
            Console.WriteLine("{0, -" + width + "} {1}", "Table Numbers:", TableChoicesSTR.PadLeft(spacing));
            Console.WriteLine();
            Console.WriteLine("Current Orders:");
            Console.WriteLine();
            double totalPrice = 0;
            double discount = 0;

            foreach (int key in keys)
            {
                MenuItem item = menuItems.FirstOrDefault(menuItem => menuItem.ID == key);
                int count = orderCounts[key];
                double price = count * item.Price;
                totalPrice += price;
                Console.WriteLine("{0, -" + width + "} {1}", $"x{count} {item.Name}", $"{price}".PadLeft(spacing));
            }
            Console.WriteLine();
            if (AmountofPersons >= 5)
            {
                discount = totalPrice * DISCOUNT_RATE;
                Console.WriteLine("{0, -" + width + "} {1}", "Price before discount", $"{totalPrice:C}".PadLeft(spacing));
                Console.WriteLine("{0, -" + (width + 1) + "} {1}", $"Groupsize discount because of a group sive of {AmountofPersons}", "".PadLeft(spacing + 1));
                Console.WriteLine("{0, -" + width + "} {1}", "Discount (10%)", $"-{discount:C}".PadLeft(spacing));
            }
            double finalTotalPrice = totalPrice - discount;
            Console.WriteLine("{0, -" + width + "} {1}", "Total Price", $"{finalTotalPrice:C}".PadLeft(spacing));
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Current Options:");
            Console.WriteLine();
            Console.WriteLine("1. Confirm Reservation");
            Console.WriteLine("2. Select a different date (You will be making a new reservation)");
            Console.WriteLine("3. Select a different timeslot (You will be making a new reservation)");
            Console.WriteLine("4. Cancel Reservation");

            string entry = Console.ReadLine().Replace(" ", ""); Program.ConsoleClear();

            if (entry == "1") { Console.WriteLine("Reservation Confirmed.\nPress enter to continue..."); Console.ReadLine(); Program.ConsoleClear(); break; }
            else if (entry == "2") { return "select date"; }
            else if (entry == "3") { return "select timeslot"; }
            else if (entry == "4") { return "main menu"; }
            else { Console.WriteLine("Invalid entry, please try again.\n"); continue; }
        }

        //If the customer confirms reservation, the reservation is saved and a receipt will be printed.
        string ReservationCode = ReservationCodes.GenerateReservationCode(reservations);
        Reservation reservation = new Reservation(DateSelect, TimeSlot, TableChoices, Customer.UserName, Customer.Email, AmountofPersons, orderCounts, ReservationCode, DateTime.Now.ToString("dd-MM-yyyy"));
        CSV.WriteToCSVReservations(reservation, "Reservation.csv", false);

        Reservation.PrintReceipt(width, spacing, DateSelect, TimeSlot, AmountofPersons.ToString(), TableChoicesSTR, Customer.UserName, orderCounts, ReservationCode, DateTime.Now.ToString("dd-MM-yyyy"));
        Console.WriteLine("Your receipt with reservation code has been printed.");
        Console.WriteLine("You will need this code once you arrive. See you soon!");
        Console.WriteLine("");
        Console.WriteLine("You can also find this receipt through the main menu.");
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine(); Program.ConsoleClear();

        return "main menu";
    }

    public static void ViewReservations()
    {
        List<Reservation> file = CSV.ReadFromCSVReservations("Reservation.csv", false);
        foreach (Reservation r in file)
        {
            if (r.Date == "Date"){continue;}
            Console.WriteLine(@$"Date: {r.Date}
Timeslot: {r.TimeSlot}
Table(s): {string.Join(",", r.Table)}
Customer name: {r.CustomerName}
Customer email:{r.CustomerEmail}
Amount of persons:{r.AmountofPersons}
Reservation code: {r.ReservationCode}
Booking date: {r.DateOfBooking}
");
        }

        Console.WriteLine($"Press any key to return..."); Console.ReadKey();
    }
}