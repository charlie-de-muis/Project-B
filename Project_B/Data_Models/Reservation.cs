public class Reservation
{
    public string Date;
    public string TimeSlot;
    public List<int> Table;
    public string CustomerName;
    public string CustomerEmail;
    public int AmountofPersons;
    public string ReservationCode;
    public string DateOfBooking;

    public Reservation(string date, string timeslot, List<int> table, string name, string email, int amount, string code, string dateofbooking)
    {
        this.Date = date;
        this.TimeSlot = timeslot;
        this.Table = table;
        this.CustomerName = name;
        this.CustomerEmail = email;
        this.AmountofPersons = amount;
        this.ReservationCode = code;
        this.DateOfBooking = dateofbooking;
    }
    public static void PrintReceipt(int width, int spacing, string DateSelect, string TimeSlot, string AmountofPersons, string TableChoicesSTR, string ReservationCode, string CurrentDate)
    {
        Console.WriteLine("╔═══════════════════════════════════════╗");
        Console.WriteLine("║            [RestaurantName]           ║");
        Console.WriteLine("╠═══════════════════════════════════════╣");
        Console.WriteLine("║                                       ║");
        Console.WriteLine("║------------- Your Receipt ------------║");
        Console.WriteLine("║                                       ║");
        Console.WriteLine("{0, -" + width + "} {1}", "║ Booking Date:", DateSelect.PadLeft(spacing) + " ║");
        Console.WriteLine("{0, -" + width + "} {1}", "║ TimeSlot:", TimeSlot.PadLeft(spacing) + " ║");
        Console.WriteLine("{0, -" + width + "} {1}", "║ Total Customers:", AmountofPersons.ToString().PadLeft(spacing) + " ║");
        Console.WriteLine("{0, -" + width + "} {1}", "║ Table Numbers:", TableChoicesSTR.PadLeft(spacing) + " ║");
        Console.WriteLine("{0, -" + width + "} {1}", "║ Reservation Code:", ReservationCode.PadLeft(spacing) + " ║");
        Console.WriteLine("║                                       ║");
        Console.WriteLine("║ Total Price:            Currently N/A ║");
        Console.WriteLine("║                                       ║");
        Console.WriteLine("║-------------- " + CurrentDate + " -------------║");
        Console.WriteLine("║                                       ║");
        Console.WriteLine("╚═══════════════════════════════════════╝");
        Console.WriteLine();
    }

    public void PrintThisReceipt()
    {
        string TableChoicesSTR = string.Join(", ", Table);
        List<int> stringLengths = new List<int>() { TimeSlot.Length, TableChoicesSTR.Length };
        int spacing = Math.Max(10, stringLengths.Max());
        int width = 28;

        if (spacing > 10) { width -= spacing - 10; }

        PrintReceipt(width, spacing, Date, TimeSlot, Convert.ToString(AmountofPersons), TableChoicesSTR, ReservationCode, DateOfBooking);
    }
}