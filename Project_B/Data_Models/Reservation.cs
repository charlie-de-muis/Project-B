public class Reservation
{
    public string Date;
    public string TimeSlot;
    public List<int> Table;
    public string CustomerName;
    public string CustomerEmail;
    public int AmountofPersons;
    public Dictionary<int, int> MenuOrders;
    public string ReservationCode;
    public string DateOfBooking;
    private const double DISCOUNT_RATE = 0.10;

    public Reservation(string date, string timeslot, List<int> table, string name, string email, int amount, Dictionary<int, int> menuOrders, string code, string dateofbooking)
    {
        this.Date = date;
        this.TimeSlot = timeslot;
        this.Table = table;
        this.CustomerName = name;
        this.CustomerEmail = email;
        this.AmountofPersons = amount;
        this.MenuOrders = menuOrders;
        this.ReservationCode = code;
        this.DateOfBooking = dateofbooking;
    }
    public static void PrintReceipt(int width, int spacing, string DateSelect, string TimeSlot, string AmountofPersons, string TableChoicesSTR, Dictionary<int, int> MenuOrders, string ReservationCode, string CurrentDate)
    {
        double totalPrice = 0;
        double discount = 0;

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
        Console.WriteLine("║---------------------------------------║");
        Console.WriteLine("║                                       ║");
        Console.WriteLine("║ Dishes/Drinks:                        ║");
        Console.WriteLine("║                                       ║");
        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current");
        ICollection<int> keys = MenuOrders.Keys;
        foreach (int key in keys)
            {
                MenuItem item = menuItems.FirstOrDefault(menuItem => menuItem.ID == key);
                int count = MenuOrders[key];

                double price = count * item.Price;
                totalPrice += price;
                Console.WriteLine("{0, -" + (width + 1) + "} {1}", $"║ x{count} {item.Name}", $"{price} ║".PadLeft(spacing + 1));
            }
        Console.WriteLine("║                                       ║");
        Console.WriteLine("{0, -" + (width + 1) + "} {1}", $"║ Price before discount", $"{totalPrice:C}   ║".PadLeft(spacing + 1));
        int AmountofPersons1 =int.Parse(AmountofPersons);
        if (AmountofPersons1 >= 5)
            {
                discount = totalPrice * DISCOUNT_RATE;
                Console.WriteLine("{0, -" + (width + 1) + "} {1}", $"║ Because totalcustomer = {AmountofPersons1}:", " ║".PadLeft(spacing + 1));
                Console.WriteLine("{0, -" + (width + 1) + "} {1}", $"║ Discount (10%)", $"-{discount:C}   ║".PadLeft(spacing + 1));
            }
        double finalTotalPrice = totalPrice - discount;
        Console.WriteLine("{0, -" + (width + 1) + "} {1}", $"║ Total Price", $"{finalTotalPrice:C}   ║".PadLeft(spacing + 1));
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

        PrintReceipt(width, spacing, Date, TimeSlot, Convert.ToString(AmountofPersons), TableChoicesSTR, MenuOrders, ReservationCode, DateOfBooking);
    }
}