public class Table
{
    public int ID;
    public int TotalSeats;
    public bool IsAvailable;

    public Table(int id, int totalSeats)
    {
        ID = id;
        TotalSeats = totalSeats;
    }

    public static List<Table> GetTableInfo()
    {
        return new List<Table>() {
            new Table(1, 2), new Table(2, 2), new Table(3, 2), new Table(4, 2), new Table(5, 2),
            new Table(6, 6), new Table(7, 4), new Table(8, 4), new Table(9, 2), new Table(10, 2),
            new Table(11, 6), new Table(12, 4), new Table(13, 4), new Table(14, 4), new Table(15, 2) };
    }

    public void GetTableReservation(List<Reservation> reservations, string dateSelect, string timeSlot)
    {
        foreach (Reservation reservation in reservations)
        {
            if (reservation.Date == dateSelect && reservation.TimeSlot == timeSlot && reservation.Table.Contains(ID))
            {
                IsAvailable = false;
                return;
            }
        }
        IsAvailable = true;
    }

    public static void DisplayTables(List<Table> tables)
    {
        string[] layout = {
            "   _______      _______      _______      _______      _______",
            " 1.|__|__|    2.|__|__|    3.|__|__|    4.|__|__|    5.|__|__|",
            "",
            "   __________      _______      _______         ____      ____",
            " 6.|__|__|__|    7.|__|__|    8.|__|__|       9.|__|   10.|__|",
            "   |__|__|__|      |__|__|      |__|__|         |__|      |__|",
            "",
            "   __________      _______      _______      _______      ____",
            "11.|__|__|__|   12.|__|__|   13.|__|__|   14.|__|__|   15.|__|",
            "   |__|__|__|      |__|__|      |__|__|      |__|__|      |__|"
        };

        for (int i = 0; i < layout.Length; i++)
        {
            foreach (var table in tables)
            {
                if (table.IsAvailable == false)
                {
                    string original = $"{table.ID}.|__|";
                    string replacement = $"{table.ID}.|X|";
                    layout[i] = layout[i].Replace(original, replacement);
                }
            }
            Console.WriteLine(layout[i]);
        }

        Console.WriteLine("Legend:");
        Console.WriteLine("   X = Not Available\n");
    }
}
