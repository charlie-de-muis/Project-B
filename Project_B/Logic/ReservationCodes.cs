public class ReservationCodes
{
    // returns false if the reservationcode doesn't already exist. Otherwise it returns true
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

    // creates a random code, checks if the code already exists, and returns the generated code
    public static string GenerateReservationCode(List<Reservation> reservations)
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
}