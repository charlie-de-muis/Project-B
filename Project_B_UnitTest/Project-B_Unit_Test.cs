namespace Project_B_UnitTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void EncodingTest()
    {
        Assert.AreEqual(PasswordEncoding.EncodeString("Hallo"), "ZsDDG");
        Assert.AreEqual(PasswordEncoding.DecodeString("ZsDDG"), "Hallo");
    }
}