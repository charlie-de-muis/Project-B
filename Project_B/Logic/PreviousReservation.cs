// Made by Tiffany

public class PreviousReservation
{ 
    // let's customers print the receipt of their reservation again
    public static void PreRes(Customer c)
    {
        List<Reservation> file = CSV.ReadFromCSVReservations("Reservation.csv", false);

        foreach (Reservation r in file)
        {
            if (r.CustomerName == c.UserName && r.CustomerEmail == c.Email)
            {                      
            Console.WriteLine(@$"
Date: {r.Date}
Timeslot: {r.TimeSlot}
Table(s): {string.Join(",", r.Table)}
Customer name: {r.CustomerName}
Customer email: {r.CustomerEmail}
Amount of persons: {r.AmountofPersons}
Reservation code: {r.ReservationCode}
Booking date: {r.DateOfBooking}
");
        }}

        Console.WriteLine("Please enter your reservation code:");
        string code = Console.ReadLine();
        bool found = false;

        // search for the correct reservation
        foreach (Reservation R in file)
        {
            string TableChoicesSTR = string.Join(", ", R.Table);
            Program.ConsoleClear();

            List<int> stringLengths = new List<int>() {R.TimeSlot.Length, TableChoicesSTR.Length };
            int spacing = Math.Max(30, stringLengths.Max());
            int width = 45;

            if (spacing > 30) { width -= spacing - 30; }
            
            // if it is the correct reservation, print the receipt
            if (R.CustomerEmail == c.Email && R.ReservationCode == code)
            {
                Reservation.PrintReceipt(width, spacing, R.Date, R.TimeSlot, Convert.ToString(R.AmountofPersons), TableChoicesSTR, c.UserName, R.MenuOrders, R.ReservationCode, R.DateOfBooking); found = true;
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