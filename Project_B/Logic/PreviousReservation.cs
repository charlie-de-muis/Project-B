public class PreviousReservation
{
    public static void PreRes(Customer c)
    {
        Console.WriteLine("Please enter your reservation code:");
        string code = Console.ReadLine();
        bool found = false;

        foreach (Reservation R in CSV.ReadFromCSVReservations("Reservation.csv"))
        {
            string TableChoicesSTR = string.Join(", ", R.Table);
            List<int> stringLengths = new List<int>() {R.TimeSlot.Length, TableChoicesSTR.Length };
            int spacing = Math.Max(10, stringLengths.Max());
            int width = 29;

            if (spacing > 10) { width -= spacing - 10; }
            
            if (R.CustomerName == c.UserName && R.ReservationCode == code){Reservation.PrintReceipt(width, spacing, R.Date, R.TimeSlot, Convert.ToString(R.AmountofPersons), TableChoicesSTR, R.ReservationCode, DateTime.Now.ToString("dd-MM-yyyy")); found = true;}
        }

        if (found == false){Console.WriteLine($"No reservation found with code: {code}");}
    }
}