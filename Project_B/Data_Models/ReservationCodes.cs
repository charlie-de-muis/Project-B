public class ReservationCodes
{
    // returned false als de code nog niet bestaat, anders true
    public static bool CheckCode(string code)
    {
        List<Reservation> Reservations = CSV.ReadFromCSVReservations("Reservations.csv");
        try
        {
            foreach (Reservation r in Reservations)
            {
                if (r.ReservationCode == code){return true;}
            }
            return false;
        }
        catch (Exception e){return false;}
    }

    // maak een random code, check of hij al bestaan, en stuur de code of een error bericht terug.
    public static string GenerateReservationCode()
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

        if (CheckCode(code) == false){return code;}
        else {return "This code already exists.";}
    }
}