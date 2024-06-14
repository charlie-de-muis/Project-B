// Made by Melvern and Olivier
public class Table
{
    public int ID;
    public int TotalSeats;
    public bool IsAvailable = true;

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
    }

    // shows the available tables
    public static string DisplayTables(List<Table> tables)
    {
        if (!tables.Any())
        {
            return " 1[ ][ ]  2[ ][ ]  3[ ][ ]  4[ ][ ]  5[ ][ ]\n\n 6[ ][ ][ ]  7[ ][ ]  8[ ][ ]   9[ ]   10[ ]\n  [ ][ ][ ]   [ ][ ]   [ ][ ]    [ ]     [ ]\n\n11[ ][ ][ ] 12[ ][ ] 13[ ][ ] 14[ ][ ] 15[ ]\n  [ ][ ][ ]   [ ][ ]   [ ][ ]   [ ][ ]   [ ]\n   ____  ____  ____  ____  ____  ____  ____  ____\n  _|__|__|__|__|__|__|__|__|__|__|__|__|__|__|__|___\n  |                      BAR                       |\n  |________________________________________________|";
        }
        
        string line1 = "";
        string line2 = "";
        string line3 = "";
        string line4 = "";
        string line5 = "";
        string line6 = "";
        string line7 = "";

        foreach (Table table in tables)
        {
            if (table.ID >= 1 && table.ID <= 5)
            {
                if (table.IsAvailable) { line1 += $" {table.ID}[ ][ ] "; }
                else { line1 += $" {table.ID}[x][x] "; }
            }
            else if (table.ID == 6)
            {
                if (table.IsAvailable) { line3 += $" {table.ID}[ ][ ][ ] "; line4 += "  [ ][ ][ ] "; }
                else { line3 += $" {table.ID}[x][x][x] "; line4 += "  [x][x][x] "; }
            }
            else if (table.ID == 7 || table.ID == 8)
            {
                if (table.IsAvailable) { line3 += $" {table.ID}[ ][ ] "; line4 += "  [ ][ ] "; }
                else { line3 += $" {table.ID}[x][x] "; line4 += "  [x][x] "; }
            }
            else if (table.ID == 9)
            {
                if (table.IsAvailable) { line3 += $"  {table.ID}[ ] "; line4 += "   [ ] "; }
                else { line3 += $"  {table.ID}[x] "; line4 += "   [x] "; }
            }
            else if (table.ID == 10)
            {
                if (table.IsAvailable) { line3 += $"  {table.ID}[ ] "; line4 += "    [ ]"; }
                else { line3 += $"  {table.ID}[x] "; line4 += "    [x] "; }
            }
            else if (table.ID == 11)
            {
                if (table.IsAvailable) { line6 += $"{table.ID}[ ][ ][ ] "; line7 += "  [ ][ ][ ] "; }
                else { line6 += $"{table.ID}[x][x][x] "; line7 += "  [x][x][x] "; }
            }
            else if (table.ID == 12 || table.ID == 13 || table.ID == 14)
            {
                if (table.IsAvailable) { line6 += $"{table.ID}[ ][ ] "; line7 += "  [ ][ ] "; }
                else { line6 += $"{table.ID}[x][x] "; line7 += "  [x][x] "; }
            }
            else if (table.ID == 15)
            {
                if (table.IsAvailable) { line6 += $"{table.ID}[ ]"; line7 += "  [ ]"; }
                else { line6 += $"{table.ID}[x]"; line7 += "  [x]"; }
            }
        }
        Console.WriteLine("Legend:");
        Console.WriteLine("   X = Not Available\n");
        return $"{line1}\n{line2}\n{line3}\n{line4}\n{line5}\n{line6}\n{line7}\n   ____  ____  ____  ____  ____  ____  ____  ____\n  _|__|__|__|__|__|__|__|__|__|__|__|__|__|__|__|___\n  |                      BAR                       |\n  |________________________________________________|";
    }
}