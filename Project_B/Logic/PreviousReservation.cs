public class PreviousReservation
{ 
    // let's customers print the receipt of their reservation again
    public static void PreRes(Customer c)
    {
        Console.WriteLine("Please enter your reservation code:");
        string code = Console.ReadLine();
        bool found = false;

        // search for the correct reservation
        foreach (Reservation R in CSV.ReadFromCSVReservations("Reservation.csv", false))
        {
            string TableChoicesSTR = string.Join(", ", R.Table);
            Program.ConsoleClear();

            List<int> stringLengths = new List<int>() {R.TimeSlot.Length, TableChoicesSTR.Length };
            int spacing = Math.Max(10, stringLengths.Max());
            int width = 28;

            if (spacing > 10) { width -= spacing - 10; }
            
            // if it is the correct reservation, print the receipt
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