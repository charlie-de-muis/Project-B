using System.Runtime.CompilerServices;

namespace Project_B_UnitTest;

[TestClass]
public class UnitTest1
{
    // Tiffany
    [TestMethod]
    public void EncodingTest()
    {
        Assert.AreEqual(PasswordEncoding.EncodeString("Hallo"), "ZsDDG");
        Assert.AreEqual(PasswordEncoding.EncodeString("Bye"), "TQw");

        Assert.AreEqual(PasswordEncoding.DecodeString("ZsDDG"), "Hallo");
        Assert.AreEqual(PasswordEncoding.DecodeString("TQw"), "Bye");
    }
    // Tiffany
    [TestMethod]
    public void Test_Add_Items()
    {
        MenuItem Pizza = new MenuItem(1000,"Pizza pepperoni", new List<string> (){"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string>(){"x"});
        JSON.WriteJSON(new List<MenuItem>(){Pizza},"current", true);

        MenuItem latest = JSON.ReadJSON("current", true).FirstOrDefault(item => item.ID == 1000);
        Assert.AreEqual(Pizza.ID, latest.ID);
        Assert.AreEqual(Pizza.Name, latest.Name);
        Assert.AreEqual(Pizza.Price, latest.Price);
    }
    // Tiffany
    [TestMethod]
    public void Test_Saved_ReservationCode()
    {
        Reservation res = new Reservation("12-12-2030", "dinner", new List<int>(){1}, "Tester", "test@gmail.com", 2, new Dictionary<int,int>() { {2, 5} }, "test123", "06-06-2024");
        CSV.WriteToCSVReservations(res, "Reservation.csv", true);

        Reservation latest = CSV.ReadFromCSVReservations("Reservation.csv", true).FirstOrDefault(res => res.Date == "12-12-2030");
        Assert.AreEqual(latest.ReservationCode, res.ReservationCode);
    }

    // Melvern
    [TestMethod]
    public void WriteReservationTest()
    {
        string Date = "10-06-2024";
        string TimeSlot = "dinner";
        List<int> Tables = new List<int>() { 6, 7 };
        string CName = "Melvern";
        string EMail = "melvernvandijk@outlook.com";
        int CCount = 8;
        Dictionary<int, int> MItems = new Dictionary<int, int>() { {2, 5}, {1, 6} };
        string Code = "GJi5d8VB";
        string DateOfBooking = "06-06-2024";
        Reservation reservationWrite = new Reservation(Date, TimeSlot, Tables, CName, EMail, CCount, MItems, Code, DateOfBooking);

        CSV.WriteToCSVReservations(reservationWrite, "Reservation.csv", true);
        Reservation reservationRead = CSV.ReadFromCSVReservations("Reservation.csv", true).FirstOrDefault(reservation => reservation.ReservationCode == Code);

        Assert.AreEqual(Code, reservationRead.ReservationCode);
    }

    // Melvern
    [DataTestMethod]
    [DataRow(new string[] { "10" }, true)]
    [DataRow(new string[] { "12", "13" }, true)]
    public void TestChooseTables(string[] TablesArray, bool Expected)
    {
        List<string> Tables = TablesArray.ToList();
        bool result = ReservationSystem.GetTables(Tables, Table.GetTableInfo());

        Assert.AreEqual(Expected, result);
    }

    // Melvern
    [TestMethod]
    public void PrintTables()
    {
        string line1 = " 1[ ][ ]  2[ ][ ]  3[ ][ ]  4[ ][ ]  5[ ][ ]";
        string line2 = "";
        string line3 = " 6[ ][ ][ ]  7[ ][ ]  8[ ][ ]   9[ ]   10[ ]";
        string line4 = "  [ ][ ][ ]   [ ][ ]   [ ][ ]    [ ]     [ ]";
        string line5 = "";
        string line6 = "11[ ][ ][ ] 12[ ][ ] 13[ ][ ] 14[ ][ ] 15[ ]";
        string line7 = "  [ ][ ][ ]   [ ][ ]   [ ][ ]   [ ][ ]   [ ]";
        string line8 = "   ____  ____  ____  ____  ____  ____  ____  ____";
        string line9 = "  _|__|__|__|__|__|__|__|__|__|__|__|__|__|__|__|___";
        string line10 = "  |                      BAR                       |";
        string line11 = "  |________________________________________________|";

        string tableString = $"{line1}\n{line2}\n{line3}\n{line4}\n{line5}\n{line6}\n{line7}\n{line8}\n{line9}\n{line10}\n{line11}";
        string result = Table.DisplayTables(new List<Table>());

        Assert.AreEqual(tableString, result);
    }
}