using System.Runtime.CompilerServices;

namespace Project_B_UnitTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void EncodingTest()
    {
        Assert.AreEqual(PasswordEncoding.EncodeString("Hallo"), "ZsDDG");
        Assert.AreEqual(PasswordEncoding.EncodeString("Bye"), "TQw");

        Assert.AreEqual(PasswordEncoding.DecodeString("ZsDDG"), "Hallo");
        Assert.AreEqual(PasswordEncoding.DecodeString("TQw"), "Bye");
    }

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
}