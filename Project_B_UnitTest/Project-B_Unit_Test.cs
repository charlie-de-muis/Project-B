namespace Project_B_UnitTest;

// made by Orestis
[TestClass]
public class UnitTest5
{
    [TestMethod]
    public void Test_Delete_Item()
    {
        //Adding the menu item Lasagna
        MenuItem Lasagna = new MenuItem(1010,"Lasagna", new List<string> (){"pasta", "sauce", "cheese", "minced meat"}, 5.00, new List<string>(){"x"});
        JSON.WriteJSON(new List<MenuItem>(){Lasagna},"current");

        //Removing menu item Lasagna by reading the Json file, deleting the item, checking if the ID still exists and returns!
        List<MenuItem> menuItems = JSON.ReadJSON("current");
        Delete_Item.DeleteChosenItem(1010, menuItems);
        MenuItem deletedLasagna = menuItems.FirstOrDefault(item => item.ID == 1010);
        Assert.IsNull(deletedLasagna);
    }

    [TestMethod]
    public void Test_Information_Restaurant()
    {
        //First create instances of StringReader, which is a tool that inputs text into the AboutTextAdmin method when called
        StringReader sr = new StringReader("Testing text 1\nTesting text 2\nTesting text 3\n");
        Console.SetIn(sr);
        //Calling the method
        Main_Menu.AboutTextAdmin();

        //Read the contents of the restaurant info file, make it an array
        //Check if it has 3 lines & is the same as the lines above
        string[] text = TXT.ReadFromTXT().Split('\n');
        Assert.AreEqual(3, text.Length);
        Assert.AreEqual("Testing text 1", text[0].Trim());
        Assert.AreEqual("Testing text 2", text[1].Trim());
        Assert.AreEqual("Testing text 3", text[2].Trim());
    }

    [TestMethod]
    public void Test_Current_Menu()
    {
        //First add an item to the current menu so we can later check if its actually there
        //using the Display_Menu function!
        MenuItem Creme_Brulee = new MenuItem(1020,"Creme Brulee", new List<string> (){"eggs", "milk", "sugar", "gelatin"}, 7.00, new List<string>(){"x"});
        JSON.WriteJSON(new List<MenuItem>(){Creme_Brulee},"current");

        //Then create instances of StringWriter, which is a tool that tells you what the console output is
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);

        //Calling the method
        MenuManager.DisplayMenu();

        //Processing & Asserting
        var output = sw.GetStringBuilder().ToString().Trim();
        //This line of code is a bit advanced, it splits the output into an array of strings, where each string is a line from the file
        var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        var lastItem = lines[lines.Length - 9];
        Assert.IsNotNull(output);
        Assert.AreEqual(lastItem, "1020. Creme Brulee");
    }
}