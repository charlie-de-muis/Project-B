static class ReservationSystem
{
    private static string DateSelect;
    private static string TimeSlot;
    private static int AmountofPersons;
    private static List<int> TableChoices;
    private static Account Customer;
    //private static List<int> menuOrders; (all the orders chosen from the menu) (NEXT SPRINT)
    private static string ReservationCode;
    //More variables coming?

    public static void ReservationMenu(Account account)
    {
        Customer = account;

        while (true)
        {
            Console.WriteLine("1. Display the tables");
            Console.WriteLine("2. Make a reservation");
            Console.WriteLine("3. Return to main menu");

            int choice = 0;
            try { choice = int.Parse(Console.ReadLine()); }
            catch { Console.WriteLine("Invalid entry"); }

            switch (choice)
            {
                case 1:
                    Table.DisplayTables();
                    break;
                case 2:
                    MakeReservation();
                    break;
                case 3:
                    return;
            }
        }
    }

    public static void MakeReservation()
    {
        List<Reservation> reservations = CSV.ReadFromCSVReservations("Reservation.csv");

        //CUSTOMER SELECTS ON WHICH DATE HE/SHE WANTS TO RESERVE.
        Console.WriteLine("For which day do you want to book?");
        while (true)
        {
            DateSelect = "";
            AmountofPersons = 0;
            TableChoices = new List<int>();
            //List<int> menuOrders; (all the orders chosen from the menu) (NEXT SPRINT)
            //More variables coming?

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
            List<string> availableDates = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
            if (availableDates.Contains(entry)) { DateSelect = DateTime.Now.AddDays(int.Parse(entry)).ToString("dd-MM-yyyy"); }
            else if (entry == "cancel") { return; }
            else { Console.WriteLine("Invalid entry, for which day do you want to book?"); continue; }
            
            while (true)
            {
                string resultTimeSlot = SelectTimeSlot();
                if (resultTimeSlot == "cancel") { return; }
                if (resultTimeSlot == "select date") { break; }

                string resultCustomerCount = SelectCustomerCount();
                if (resultCustomerCount == "cancel") { return; }
                if (resultCustomerCount == "select date") { break; }

                string resultTables = SelectTables(reservations);
                if (resultTables == "cancel") { return; }
                if (resultTables == "select date") { break; }
                if (resultTables == "no tables") { continue; }

                string resultReceipt = PrintReceipt(reservations);
                if (resultReceipt == "main menu") { return; }
                if (resultReceipt == "select date") { break; }
                if (resultReceipt == "select timeslot") { continue; }
            }
        }
    }

    private static string SelectTimeSlot()
    {
        //CUSTOMER SELECTS WHICH TIME OF DAY HE/SHE WANTS TO RESERVE.
        Console.WriteLine("For what timeslot do you want to book? (Brunch, Lunch, Afternoon, Dinner)");
        while (true)
        {
            Console.WriteLine("Type 'select date' to reserve on a different date.");
            Console.WriteLine("Or type 'cancel' to cancel the reservation.");
            TimeSlot = Console.ReadLine().ToLower();
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
        Console.WriteLine("For how many people do you order? (1 - 16)");
        while (true)
        {
            Console.WriteLine("Type 'select date' to reserve on a different date.");
            Console.WriteLine("Or type 'cancel' to cancel the reservation.");
            string choice = Console.ReadLine().ToLower();
            if (choice == "cancel" || choice == "select date") { return choice; }
            else
            {
                bool result = int.TryParse(choice.Replace(" ", ""), out int totalPersons);
                if (result == false)
                {
                    Console.WriteLine("Invalid entry, for how many people do you order? (1 - 16)");
                }
                else if (totalPersons < 1 || totalPersons > 16 ) { Console.WriteLine($"{totalPersons} is not within this range (1 - 16)"); }
                else { AmountofPersons = totalPersons; return choice; }
            }
        }
    }

    private static string SelectTables(List<Reservation> reservations)
    {
        List<Table> tables = Table.GetTableInfo();
        List<int> allAvailableTables = new List<int>();
        foreach (Table table in tables)
        {
            table.GetTableReservation(reservations, DateSelect, TimeSlot);
            if (table.IsAvailable) { allAvailableTables.Add(table.ID); }
        }

        while (true)
        {
            TableChoices = new List<int>();
            bool count = true;

            if (allAvailableTables.Count > 0)
            {
                Table.DisplayTables();
                Console.WriteLine("\nAvailable tables: " + string.Join(", ", allAvailableTables));
            }
            else
            {
                Console.WriteLine($"Sorry, {TimeSlot} is fully booked on this date, please select a different timeslot or date.");
                Console.WriteLine("Press enter to continue."); Console.ReadLine(); return "no tables";
            }

            Console.WriteLine("\nSelect a single table by its number.");
            Console.WriteLine("Select multiple tables for merging by their numbers, comma seperated. (up to 16 people)");
            Console.WriteLine("Type 'select date' to reserve on a different date.");
            Console.WriteLine("Or type 'cancel' to cancel the reservation.");

            string entryTables = Console.ReadLine();
            if (entryTables == "cancel" || entryTables == "select date") { return entryTables; }

            List<string> entries = entryTables.Split(',').ToList();
            List<string> TableChoicesSTR = new List<string>();
            foreach (string number in entries) { TableChoicesSTR.Add(number.Replace(" ", "")); }

            List<string> tableNumbers = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
            foreach (string number in TableChoicesSTR) { if (!tableNumbers.Contains(number)) { Console.WriteLine("Invalid entry."); count = false; break; } }

            if (count)
            {
                foreach (string number in TableChoicesSTR) { TableChoices.Add(int.Parse(number)); }

                //Single table.
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
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                        return entryTables;
                    }
                }
                //Multiple tables.
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

                    //After checking table availablility, they will be checked for merge capability.
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
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();
                            return entryTables;
                        }
                    }
                }
            }
        }
    }

    private static string PrintReceipt(List<Reservation> reservations)
    {
        string TableChoicesSTR = string.Join(", ", TableChoices);
        List<int> stringLengths = new List<int>() { TimeSlot.Length, TableChoicesSTR.Length };
        int spacing = Math.Max(10, stringLengths.Max());
        int width = 29;

        if (spacing > 10) { width -= spacing - 10; }

        while (true)
        {
            Console.WriteLine("This is what you have selected:");
            Console.WriteLine();
            Console.WriteLine("{0, -" + width + "} {1}", "Booking Date:", DateSelect.PadLeft(spacing));
            Console.WriteLine("{0, -" + width + "} {1}", "TimeSlot:", TimeSlot.PadLeft(spacing));
            Console.WriteLine("{0, -" + width + "} {1}", "Total Customers:", AmountofPersons.ToString().PadLeft(spacing));
            Console.WriteLine("{0, -" + width + "} {1}", "Table Numbers:", TableChoicesSTR.PadLeft(spacing));
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Current Options:");
            Console.WriteLine();
            Console.WriteLine("1. Confirm Reservation");
            Console.WriteLine("2. Select a different date (You will be making a new reservation)");
            Console.WriteLine("3. Select a different timeslot (You will be making a new reservation)");
            Console.WriteLine("4. Cancel Reservation");

            string entry = Console.ReadLine().Replace(" ", "");

            if (entry == "1") { Console.WriteLine("Reservation Confirmed.\n"); break; }
            else if (entry == "2") { return "select date"; }
            else if (entry == "3") { return "select timeslot"; }
            else if (entry == "4") { return "main menu"; }
            else { Console.WriteLine("Invalid entry, please try again.\n"); continue; }
        }

        string ReservationCode = GenerateReservationCode(reservations);
        Reservation reservation = new Reservation(DateSelect, TimeSlot, TableChoices, Customer.UserName, AmountofPersons, ReservationCode);
        CSV.WriteToCSVReservations(reservation, "Reservation.csv");

        Reservation.PrintReceipt(width, spacing, DateSelect, TimeSlot, AmountofPersons.ToString(), TableChoicesSTR, ReservationCode, DateTime.Now.ToString("dd-MM-yyyy"));
        Console.WriteLine("Your receipt with reservation code has been printed.");
        Console.WriteLine("You will need this code once you arrive. See you soon!");
        Console.WriteLine("");
        Console.WriteLine("You can also find this receipt through the main menu.");
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();

        return "main menu";
    }

    // maak een random code, check of hij al bestaat, en stuur de code of een error bericht terug.
    private static string GenerateReservationCode(List<Reservation> reservations)
    {
        int attempts = 100;

        while (true)
        {
            string code = "";

            Random rand = new Random();
            string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int length = 8;

            for(int i = 0; i < length; i++)
            {
                int pos = rand.Next(62);
                code = code + letters.ElementAt(pos);
            }

            if (CheckCode(code, reservations) == false) { return code; }

            attempts--;
            if (attempts <= 0) { return null; }
        }
    }

    // returned false als de code nog niet bestaat, anders true
    private static bool CheckCode(string code, List<Reservation> reservations)
    {
        try
        {
            foreach (Reservation r in reservations)
            {
                if (r.ReservationCode == code) { return true; }
            }
            return false;
        }
        catch (Exception e) { return false; }
    }
}
