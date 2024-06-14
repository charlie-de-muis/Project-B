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
        Reservation res = new Reservation("12-12-2030", "dinner", new List<int>(){1}, "Tester", "test@gmail.com", 2, new Dictionary<int,int>(), "test123", "06-06-2024");
        CSV.WriteToCSVReservations(res, "Reservation.csv", true);

        Reservation latest = CSV.ReadFromCSVReservations("Reservation.csv", true).FirstOrDefault(res => res.Date == "12-12-2030");
        Assert.AreEqual(latest.ReservationCode, res.ReservationCode);
    }

    //Bente
    [TestMethod]
    public void Test_Sort_Price()
    {
        // Arrange
        MenuItem Pizza = new MenuItem(1000, "Pizza pepperoni", new List<string> {"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string> {"x"});
        MenuItem Burger = new MenuItem(1001, "Cheeseburger", new List<string> {"bun", "cheese", "beef"}, 3.00, new List<string> {"y"});
        MenuItem Salad = new MenuItem(1002, "Caesar Salad", new List<string> {"lettuce", "dressing", "croutons"}, 5.00, new List<string> {"z"});
        List<MenuItem> menu = new List<MenuItem> { Pizza, Burger, Salad };

        JSON.WriteJSON(menu, "current", true);

        // Act
        MenuManager.DisplaySortedMenu();

        // Assert
        var sortedMenu = JSON.ReadJSON("current", true).OrderBy(item => item.Price).ToList();
        Assert.AreEqual(Burger.ID, sortedMenu[0].ID); // Burger should be first (cheapest)
        Assert.AreEqual(Pizza.ID, sortedMenu[1].ID);  // Pizza should be second
        Assert.AreEqual(Salad.ID, sortedMenu[2].ID);  // Salad should be third (most expensive)
    }

//     //Bente DON'T TOUCH ITS A PHANTOM BOMB
//     [TestMethod]
//     public void Test_Filter_By_Price()
//     {
//         // Arrange
//         MenuItem Pizza = new MenuItem(1000, "Pizza pepperoni", new List<string> {"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string> {"x"});
//         MenuItem Burger = new MenuItem(1001, "Cheeseburger", new List<string> {"bun", "cheese", "beef"}, 3.00, new List<string> {"y"});
//         MenuItem Salad = new MenuItem(1002, "Caesar Salad", new List<string> {"lettuce", "dressing", "croutons"}, 5.00, new List<string> {"z"});
//         List<MenuItem> menu = new List<MenuItem> { Pizza, Burger, Salad };

//         JSON.WriteJSON(menu, "current", true);

//         // Act
//         MenuManager.ViewMenu();

//         // Assert
//         // Sort menu manually
//         var sortedMenu = menu.OrderBy(item => item.Price).ToList();
//         var displayedMenu = JSON.ReadJSON("current", true);

//         // Filter menu based on price and make sure it matches the sorted menu
//         Assert.IsTrue(displayedMenu.SequenceEqual(sortedMenu));
//     }

    // Bente
[TestMethod]
        public void Change_Res()
        {
            // Arrange
            // Create a sample reservation using the assumed constructor with parameters
            var reservation = new Reservation(
                "07-06-2024",
                "dinner",
                new List<int> { 1 }, // table number
                "tiffany",
                "hi@hi.nl",
                2,
                new Dictionary<int, int> { { 4, 1 }, { 6, 1 } }, //  (meaning 1 order of item 4 and  1 order of item 6)
                "BTSnTQoz",
                "06-06-2024"
            );

            // Act
            // Change the number of people from 2 to 3
                EditReservations.Change_reservation(3, reservation);

            // Assert
            // Verify that the reservation was updated correctly
            Assert.AreEqual(3, reservation.AmountofPersons);
        }

    // Bente
    [TestMethod]
    public void Sort_Ingredients()
    {
        // Arrange
        MenuItem Pizza = new MenuItem(1000, "Pizza pepperoni", new List<string> {"dough", "sauce", "cheese", "pepperoni"}, 4.50, new List<string> {"x"});
        MenuItem Burger = new MenuItem(1001, "Cheeseburger", new List<string> {"bun", "cheese", "beef"}, 3.00, new List<string> {"y"});
        MenuItem Salad = new MenuItem(1002, "Caesar Salad", new List<string> {"lettuce", "dressing", "croutons"}, 5.00, new List<string> {"z"});
        List<MenuItem> menu = new List<MenuItem> { Pizza, Burger, Salad };

        // Mock JSON.ReadJSON method by directly setting the menu list
        JSON.WriteJSON(menu, "current", true);

        // Act
        MenuManager.DisplayFilteredMenu("cheese");

        // Assert
        // Since the filter is "cheese", we expect to get Pizza and Burger, which contain "cheese"
        var expectedFilteredMenu = new List<MenuItem> { Pizza, Burger };
        var actualFilteredMenu = JSON.ReadJSON("current", true).Where(item => 
            item.Ingredients.Any(ingredient => ingredient.ToLower().Contains("cheese"))).ToList();

        CollectionAssert.AreEqual(expectedFilteredMenu, actualFilteredMenu);
    }
}

