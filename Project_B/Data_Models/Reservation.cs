public class Reservation
{
    public string Date;
    public string TimeSlot;
    public List<int> Table;
    public string CustomerName;
    public int AmountofPersons;
    public string ReservationCode;

    public Reservation(string date, string timeslot, List<int> table, string name, int amount, string code)
    {
        this.Date = date;
        this.TimeSlot = timeslot;
        this.Table = table;
        this.CustomerName = name;
        this.AmountofPersons = amount;
        this.ReservationCode = code;
    }
}