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
        JSON.WriteJSON(new List<MenuItem>(){Pizza},"Menu_current", true);

        MenuItem? latest = JSON.ReadJSON("Menu_current", true).FirstOrDefault(item => item.ID == 1000);
        Assert.AreEqual(Pizza.ID, latest.ID);
        Assert.AreEqual(Pizza.Name, latest.Name);
        Assert.AreEqual(Pizza.Price, latest.Price);
    }
    // Tiffany
    [TestMethod]
    public void Test_Saved_ReservationCode()
    {
        string tempFile = Path.GetTempFileName();

        try
        {
            Reservation res = new Reservation("12-12-2030", "dinner", new List<int>() { 1 }, "Tester", "test@gmail.com", 2, new Dictionary<int, int>() { { 2, 5 } }, "test123", "06-06-2024");
            CSV.WriteToCSVReservations(res, tempFile, true);

            // Wait for a short time to ensure the file write is complete
            Thread.Sleep(100);

            Reservation? latest = CSV.ReadFromCSVReservations(tempFile, true).FirstOrDefault(r => r.Date == "12-12-2030");
            Assert.IsNotNull(latest, "The latest reservation should not be null");
            Assert.AreEqual(latest.ReservationCode, res.ReservationCode);
        }
        finally
        {
            File.Delete(tempFile);
        }
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
        Assert.AreEqual(Code, reservationRead.ReservationCode);
        Assert.AreEqual(CName, reservationRead.CustomerName);
        Assert.AreEqual(EMail, reservationRead.CustomerEmail);
        Assert.AreEqual(Date, reservationRead.Date);
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

    //orestis
    [TestMethod]
    public void Test_Delete_Item()
    {
        //Adding the menu item Lasagna
        MenuItem Lasagna = new MenuItem(1010,"Lasagna", new List<string> (){"pasta", "sauce", "cheese", "minced meat"}, 5.00, new List<string>(){"x"});
        JSON.WriteJSON(new List<MenuItem>(){Lasagna},"Menu_current", true);

        //Removing menu item Lasagna by reading the Json file, deleting the item, checking if the ID still exists and returns!
        List<MenuItem> menuItems = JSON.ReadJSON("Menu_current", true);
        Delete_Item.DeleteChosenItem(1010, menuItems, "Menu_current");
        MenuItem deletedLasagna = menuItems.FirstOrDefault(item => item.ID == 1010);
        Assert.IsNull(deletedLasagna);
    }

    //orestis
    [TestMethod]
    public void Test_Information_Restaurant()
    {
        //First create instances of StringReader, which is a tool that inputs text into the AboutTextAdmin method when called
        StringReader sr = new StringReader("Testing text 1\nTesting text 2\nTesting text 3");
        Console.SetIn(sr);
        //Calling the method
        Main_Menu.AboutTextAdmin(true);

        //Read the contents of the restaurant info file, make it an array
        //Check if it has 3 lines & is the same as the lines above
        string[] text = TXT.ReadFromTXT(true).Split('\n');
        Assert.AreEqual(4, text.Length);
        Assert.AreEqual("Testing text 1", text[0].Trim());
        Assert.AreEqual("Testing text 2", text[1].Trim());
        Assert.AreEqual("Testing text 3", text[2].Trim());
    }

    // orestis
    [TestMethod]
    public void Test_Current_Menu()
    {
        //First add an item to the current menu so we can later check if its actually there
        //using the Display_Menu function!
        MenuItem Creme_Brulee = new MenuItem(1020,"Creme Brulee", new List<string> (){"eggs", "milk", "sugar", "gelatin"}, 7.00, new List<string>(){"x"});
        JSON.WriteJSON(new List<MenuItem>(){Creme_Brulee},"Menu_current", true);

        //Then create instances of StringWriter, which is a tool that tells you what the console output is
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);

        //Calling the method
        MenuManager.DisplayMenu("Menu_current", true);

        //Processing & Asserting
        var output = sw.GetStringBuilder().ToString().Trim();
        //This line of code is a bit advanced, it splits the output into an array of strings, where each string is a line from the file
        var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        var lastItem = lines[lines.Length - 9];
        Assert.IsNotNull(output);
        Assert.AreEqual(lastItem, "1020. Creme Brulee");
    }


    //Bente
    [TestMethod]
    public void Test_Sort_Price()
    {
        // Arrange
        MenuItem Pizza = new MenuItem(1001, "Pizza pepperoni", new List<string> {"dough", "sauce", "cheese", "pepperoni"}, 0.02, new List<string> {"x"});
        MenuItem Burger = new MenuItem(1002, "Cheeseburger", new List<string> {"bun", "cheese", "beef"}, 0.01, new List<string> {"y"});
        MenuItem Salad = new MenuItem(1003, "Caesar Salad", new List<string> {"lettuce", "dressing", "croutons"}, 100.00, new List<string> {"z"});
        List<MenuItem> menu = JSON.ReadJSON("Menu_current", true);
        menu.Add(Pizza);
        menu.Add(Burger);
        menu.Add(Salad);

        JSON.WriteJSON(menu, "Menu_current", true);

        // Act
        //MenuManager.DisplaySortedMenu("Menu_current", true);

        // Assert
        List<MenuItem> sortedMenu = JSON.ReadJSON("Menu_current", true).OrderBy(item => item.Price).ToList();

        Assert.AreEqual(Burger.ID, sortedMenu[0].ID); // Burger should be first (cheapest)
        Assert.AreEqual(Salad.ID, sortedMenu[sortedMenu.Count - 1].ID);  // Salad should be third (most expensive)
    }

    //Bente
    /*[TestMethod]
    public void Test_Filter_By_Price()
    {
        // Arrange
        MenuItem Pizza = new MenuItem(1000, "Pizza pepperoni", new List<string> {"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string> {"x"});
        MenuItem Burger = new MenuItem(1001, "Cheeseburger", new List<string> {"bun", "cheese", "beef"}, 3.00, new List<string> {"y"});
        MenuItem Salad = new MenuItem(1002, "Caesar Salad", new List<string> {"lettuce", "dressing", "croutons"}, 5.00, new List<string> {"z"});
        List<MenuItem> menu = new List<MenuItem> { Pizza, Burger, Salad };
 
        JSON.WriteJSON(menu, "Menu_current", true);
 
        // Act
        MenuManager.ViewMenu();
 
        // Assert
        // Sort menu manually
        var sortedMenu = menu.OrderBy(item => item.Price).ToList();
        var displayedMenu = JSON.ReadJSON("Menu_current", true);
 
        // Filter menu based on price and make sure it matches the sorted menu
        Assert.IsTrue(displayedMenu.SequenceEqual(sortedMenu));
    }*/
 
    // Bente
    [TestMethod]
    public void Change_Res()
    {
        // Arrange
        // Create a sample reservation using the assumed constructor with parameters
        var reservation = new Reservation(
            "07-06-2024",
            "dinner",
            new List<int> { 1 },
            "tiffany",
            "hi@hi.nl",
            2,
            new Dictionary<int, int> { { 4, 1 }, { 6, 1 } },
            "BTSnTQoz",
            "06-06-2024"
        );

        // Act
        // Change the number of people from 2 to 3
        EditReservations.Change_reservation(3, reservation, true);

        // Assert
        // Verify that the reservation was updated correctly
        Assert.AreEqual(3, reservation.AmountofPersons);
    }

    // Bente
    // [TestMethod]
    // public void Sort_Ingredients()
    // {
    //     // Arrange
    //     MenuItem Pizza = new MenuItem(1000, "Pizza pepperoni", new List<string> {"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string> {"x"});
    //     MenuItem Burger = new MenuItem(1001, "Cheeseburger", new List<string> {"bun", "cheese", "beef"}, 3.00, new List<string> {"y"});
    //     MenuItem Salad = new MenuItem(1002, "Caesar Salad", new List<string> {"lettuce", "dressing", "croutons"}, 5.00, new List<string> {"z"});
    //     List<MenuItem> menu = new List<MenuItem> { Pizza, Burger, Salad };

    //     // Mock JSON.ReadJSON method by directly setting the menu list
    //     JSON.WriteJSON(menu, "current", true);

    //     // Act
    //     MenuManager.DisplayFilteredMenu("cheese");

    //     // Assert
    //     // Since the filter is "cheese", we expect to get Pizza and Burger, which contain "cheese"
    //     var expectedFilteredMenu = new List<MenuItem> { Pizza, Burger };
    //     var actualFilteredMenu = JSON.ReadJSON("current", true).Where(item => 
    //         item.Ingredients.Any(ingredient => ingredient.ToLower().Contains("cheese"))).ToList();

    //     CollectionAssert.AreEqual(expectedFilteredMenu, actualFilteredMenu);
    // }

// orestis
    [TestMethod]
    public void Test_Make_Reservations()
    {
        // First make the reservations
        // brunch time
        string Bdate = "17-06-2024";
        string BtimeSlot = "brunch";
        List<int> Btables = new List<int>() { 6, 7 };
        string Bname = "OrestisTesting";
        string BeMail = "orestingtesting@testing.com";
        int BCount = 5;
        Dictionary<int, int> Bitems = new Dictionary<int, int>() { { 2, 5 } };
        string Bcode = "GJi5d5XD";
        string BdateOfBooking = "14-06-2024";
        Reservation reservationStuffList = new Reservation(Bdate, BtimeSlot, Btables, Bname, BeMail, BCount, Bitems, Bcode, BdateOfBooking);
        CSV.WriteToCSVReservations(reservationStuffList, "Reservation.csv", true);
 
        // lunch time
        string Ldate = "17-06-2024";
        string LtimeSlot = "lunch";
        List<int> Ltables = new List<int>() { 6, 7 };
        string Lname = "OrestisTesting";
        string LeMail = "orestingtesting@testing.com";
        int LCount = 5;
        Dictionary<int, int> Litems = new Dictionary<int, int>() { { 2, 5 } };
        string Lcode = "GJi5d5CF";
        string LdateOfBooking = "14-06-2024";
        Reservation reservationStuffList2 = new Reservation(Ldate, LtimeSlot, Ltables, Lname, LeMail, LCount, Litems, Lcode, LdateOfBooking);
        CSV.WriteToCSVReservations(reservationStuffList2, "Reservation.csv", true);
 
        // afternoon time
        string Adate = "17-06-2024";
        string AtimeSlot = "afternoon";
        List<int> Atables = new List<int>() { 6, 7 };
        string Aname = "OrestisTesting";
        string AeMail = "orestingtesting@testing.com";
        int ACount = 5;
        Dictionary<int, int> Aitems = new Dictionary<int, int>() { { 2, 5 } };
        string Acode = "GJi5d5VG";
        string AdateOfBooking = "14-06-2024";
        Reservation reservationStuffList3 = new Reservation(Adate, AtimeSlot, Atables, Aname, AeMail, ACount, Aitems, Acode, AdateOfBooking);
        CSV.WriteToCSVReservations(reservationStuffList3, "Reservation.csv", true);
 
        // dinner time
        string Ddate = "17-06-2024";
        string DtimeSlot = "dinner";
        List<int> Dtables = new List<int>() { 6, 7 };
        string Dname = "OrestisTesting";
        string DeMail = "orestingtesting@testing.com";
        int DCount = 5;
        Dictionary<int, int> Ditems = new Dictionary<int, int>() { { 2, 5 } };
        string Dcode = "GJi5d5BH";
        string DdateOfBooking = "14-06-2024";
        Reservation reservationStuffList4 = new Reservation(Ddate, DtimeSlot, Dtables, Dname, DeMail, DCount, Ditems, Dcode, DdateOfBooking);
        CSV.WriteToCSVReservations(reservationStuffList4, "Reservation.csv", true);
 
        // Read the existing reservations. Then select the last 4 by adding them to a list
        List<Reservation> reservations = CSV.ReadFromCSVReservations("Reservation.csv", true);
        var lastReservations = reservations.Skip(reservations.Count - 4).ToList();
 
        // Asserting Stage:
        // Brunch Asserting
        Assert.AreEqual(lastReservations[0].TimeSlot, BtimeSlot);
        Assert.AreEqual(lastReservations[0].ReservationCode, Bcode);
 
        // Lunch Asserting
        Assert.AreEqual(lastReservations[1].TimeSlot, LtimeSlot);
        Assert.AreEqual(lastReservations[1].ReservationCode, Lcode);
 
        // Afternoon Asserting
        Assert.AreEqual(lastReservations[2].TimeSlot, AtimeSlot);
        Assert.AreEqual(lastReservations[2].ReservationCode, Acode);
 
        // Dinner Asserting
        Assert.AreEqual(lastReservations[3].TimeSlot, DtimeSlot);
        Assert.AreEqual(lastReservations[3].ReservationCode, Dcode);
    }
}