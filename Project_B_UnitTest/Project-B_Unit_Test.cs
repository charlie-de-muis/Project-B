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
// Hij kan het JSON bestand niet vinden. De code zelf zou moeten werken, we moeten alleen even kijken hoe we 
// zorgen dat hij het bestand kan vinden.
    // [TestMethod]
    // public void Test_Add_Items()
    // {
    //     MenuItem Pizza = new MenuItem("Pizza pepperoni", new List<string> (){"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string>(){"x"});
    //     JSON.WriteJSON(new List<MenuItem>(){Pizza});

    //     CollectionAssert.Contains(JSON.ReadJSON(), Pizza);
    // }

    // [TestMethod]
    // public void Test_Saved_ReservationCode
    // {
    //      hoe moet je het testen als er een input is?
    // }
}