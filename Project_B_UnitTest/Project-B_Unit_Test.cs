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
    public void Test_Add_Items()
    {
        MenuItem Pizza = new MenuItem(1000,"Pizza pepperoni", new List<string> (){"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string>(){"x"});
        JSON.WriteJSON(new List<MenuItem>(){Pizza},"current", true);

        MenuItem latest = JSON.ReadJSON("current", true).FirstOrDefault(item => item.ID == 1000);
        Assert.AreEqual(Pizza.ID, latest.ID);
        Assert.AreEqual(Pizza.Name, latest.Name);
        Assert.AreEqual(Pizza.Price, latest.Price);
    }

    [TestMethod]
    public void Test_Saved_ReservationCode()
    {
        Reservation res = new Reservation("12-12-2030", "dinner", new List<int>(){1}, "Tester", "test@gmail.com", 2, new Dictionary<int,int>(), "test123", "06-06-2024");
        CSV.WriteToCSVReservations(res, "Reservation.csv", true);

        Reservation latest = CSV.ReadFromCSVReservations("Reservation.csv", true).FirstOrDefault(res => res.Date == "12-12-2030");
        Assert.AreEqual(latest.ReservationCode, res.ReservationCode);
    }
}
