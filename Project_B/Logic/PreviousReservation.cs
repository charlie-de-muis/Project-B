public class PreviousReservation
{
    public static void PreRes(Customer c)
    {
        List<Reservation> file = CSV.ReadFromCSVReservations("Reservation.csv");
        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current");

        foreach (Reservation r in file)
        {
            if (r.Date == "Date") { continue; }
            else if (r.CustomerName == c.UserName && r.CustomerEmail == c.Email)
            {            
                List<int> keys = r.MenuOrders.Keys.ToList();
                string menuOrdersSTR = "";
                foreach (int key in keys) { menuOrdersSTR += $"{menuItems.FirstOrDefault(menuItem => menuItem.ID == key)?.Name} x {r.MenuOrders[key]}"; }
                
                Console.WriteLine(@$"
Date: {r.Date}
Timeslot: {r.TimeSlot}
Table(s): {string.Join(",", r.Table)}
Customer name: {r.CustomerName}
Customer email: {r.CustomerEmail}
Amount of persons: {r.AmountofPersons}
Menu Orders: {menuOrdersSTR}
Reservation code: {r.ReservationCode}
Booking date: {r.DateOfBooking}
");
            }
        }

        Console.WriteLine("Please enter the reservation code from the reservation you want to see:");
        string code = Console.ReadLine();
        bool found = false;

        foreach (Reservation R in file)
        {
            string TableChoicesSTR = string.Join(", ", R.Table);
            Program.ConsoleClear();

            List<int> stringLengths = new List<int>() {R.TimeSlot.Length, TableChoicesSTR.Length };
            int spacing = Math.Max(10, stringLengths.Max());
            int width = 28;

            if (spacing > 10) { width -= spacing - 10; }
            
            if (R.CustomerEmail == c.Email && R.ReservationCode == code)
            {
                Reservation.PrintReceipt(width, spacing, R.Date, R.TimeSlot, Convert.ToString(R.AmountofPersons), TableChoicesSTR, R.MenuOrders, R.ReservationCode, R.DateOfBooking); found = true;
                Console.WriteLine("\nPress enter to continue..."); Console.ReadLine(); Program.ConsoleClear();
            }
        }

        if (found == false)
        {
            Console.WriteLine($"No reservation found with code: {code}");
            Console.WriteLine("Press enter to continue..."); Console.ReadLine(); Program.ConsoleClear();
        }
    }
}