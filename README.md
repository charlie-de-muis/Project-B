# Project-B TOOMB
Welcome to the restaurant booking system program!
Here is a brief overview of the possibilities:

From the main menu:
	Log in as a Customer or make an account: This gives you permission to make reservations, Log in/Make account by following instructions on screen

	Log in as an Admin: The admin account has every feature except for making reservations, Log in using the admin user/password

	View the restaurant menu: This option lets you view the current of future restaurant menu

	View information about the restaurant: This option shows you 3 lines of information about the restaurant, if it's blank, it's because the admin need 	to fill it in

When you are logged in as a customer:
	Log out: This option let's you log out

	Quit: This option let's you exit the program

	Make reservation: You go to the reservation menu by selecting this option

	Display tables: The restaurant layout will be displayed on screen

	Make a reservation: You will be shown available dates to make a reservation at the restaurant, then you can select a timeslot, then you can select 	how many people are in your party, it ranges from 1 to 16. You can also type 'select date' or 'select timeslot' to reserve on a different timeslot

When you are logged in as the admin:
	Add Menu Items: Here you add new menu items from the 'current' or 'future' menu.
	You add an item by typing in the (name, ingredients, price, dietary info)

	Delete Menu Items: Here you delete menu items from the 'current' or 'future' menu.
	You delete an item by typing the ID of the desired menu item.

	View Reservations: Gives you an overview of all the reservations in the system.

	Edit Reservations: By entering the code of a specific reservation, you can edit most of the values within the reservation:
	- You select what you want to change, and after entering a new value, it will be updated.

	Add / Edit Restaurant Info: Here you write and enter each specific line of text, max 3 lines.

	Manage Menu's:
	- 'Upload Current Menu' and 'Upload Future Menu':
		Type your uploaded filename.json and it will become the current/future menu, in case you already have a current/future menu,
		the two file names will be switched.
	- 'Switch Between Menu's':
		Switches the names between a filename and current/future.
	- 'View Past Menu's':
		Here you can view all the menu names in your system.
	- 'Delete Menu':
		Delete a menu by typing filename.json


Other data:
Admin username: Admin
Admin password: Admin

Test account username: Tester
Test account password: Test123

When running the unit tests, the menu files might get mixed up. We added a copy of the current and future menu below.

Menu current:
[{"Price":5.0,"DietaryInfo":["vegetarian"],"ID":1,"Name":"garlic bread","Ingredients":["bread"," butter"," garlic"]},{"Price":3.0,"DietaryInfo":["X"],"ID":2,"Name":"Spaghetti","Ingredients":["Pasta","Tomato Sauce","Cheese","Meatballs"]},{"Price":8.0,"DietaryInfo":["vegetarian"],"ID":3,"Name":"Margherita Pizza","Ingredients":["Pizza Dough","Tomato Sauce","Fresh Mozzarella","Basil"]},{"Price":6.5,"DietaryInfo":["vegetarian","vegan","dairyfree"],"ID":4,"Name":"Falafel Wrap","Ingredients":["Falafel","Pita Bread","Lettuce","Tomato","Tahini Sauce"]},{"Price":7.0,"DietaryInfo":["vegetarian","vegan","glutenfree","dairyfree"],"ID":5,"Name":"Quinoa Salad","Ingredients":["Quinoa","Cucumber","Cherry Tomatoes","Red Onion","Lemon Dressing"]},{"Price":5.5,"DietaryInfo":["vegetarian","vegan","glutenfree","dairyfree"],"ID":6,"Name":"Vegetable Stir-Fry","Ingredients":["Mixed Vegetables","Tofu","Soy Sauce","Rice"]},{"Price":6.0,"DietaryInfo":["vegetarian","vegan","dairyfree"],"ID":7,"Name":"Avocado Toast","Ingredients":["Toast","Avocado","Cherry Tomatoes","Lemon","Salt","Pepper"]},{"Price":8.0,"DietaryInfo":["vegetarian","vegan","glutenfree","dairyfree"],"ID":8,"Name":"Vegetable Curry","Ingredients":["Mixed Vegetables","Coconut Milk","Curry Paste","Rice"]},{"Price":6.0,"DietaryInfo":["vegetarian","glutenfree"],"ID":9,"Name":"Caprese Salad","Ingredients":["Tomato","Fresh Mozzarella","Basil","Balsamic Glaze"]},{"Price":7.5,"DietaryInfo":["vegetarian","vegan","dairyfree"],"ID":10,"Name":"Hummus Plate","Ingredients":["Hummus","Pita Bread","Carrot Sticks","Cucumber Slices","Bell Pepper Strips"]}]

Menu future
[{"ID":"1","Name":"garlic bread","Ingredients":["bread"," butter"," garlic"],"Price":5.0,"DietaryInfo":["vegetarian"]},{"ID":"2","Name":"Spaghetti","Ingredients":["Pasta","Tomato Sauce","Cheese","Meatballs"],"Price":3.0,"DietaryInfo":["X"]},{"ID":"3","Name":"Margherita Pizza","Ingredients":["Pizza Dough","Tomato Sauce","Fresh Mozzarella","Basil"],"Price":8.0,"DietaryInfo":["vegetarian"]},{"ID":"4","Name":"Falafel Wrap","Ingredients":["Falafel","Pita Bread","Lettuce","Tomato","Tahini Sauce"],"Price":6.5,"DietaryInfo":["vegetarian","vegan","dairyfree"]},{"ID":"5","Name":"Quinoa Salad","Ingredients":["Quinoa","Cucumber","Cherry Tomatoes","Red Onion","Lemon Dressing"],"Price":7.0,"DietaryInfo":["vegetarian","vegan","glutenfree","dairyfree"]},{"ID":"6","Name":"Vegetable Stir-Fry","Ingredients":["Mixed Vegetables","Tofu","Soy Sauce","Rice"],"Price":5.5,"DietaryInfo":["vegetarian","vegan","glutenfree","dairyfree"]},{"ID":"7","Name":"Avocado Toast","Ingredients":["Toast","Avocado","Cherry Tomatoes","Lemon","Salt","Pepper"],"Price":6.0,"DietaryInfo":["vegetarian","vegan","dairyfree"]},{"ID":"8","Name":"Vegetable Curry","Ingredients":["Mixed Vegetables","Coconut Milk","Curry Paste","Rice"],"Price":8.0,"DietaryInfo":["vegetarian","vegan","glutenfree","dairyfree"]},{"ID":"9","Name":"Caprese Salad","Ingredients":["Tomato","Fresh Mozzarella","Basil","Balsamic Glaze"],"Price":6.0,"DietaryInfo":["vegetarian","glutenfree"]},{"ID":"10","Name":"Hummus Plate","Ingredients":["Hummus","Pita Bread","Carrot Sticks","Cucumber Slices","Bell Pepper Strips"],"Price":7.5,"DietaryInfo":["vegetarian","vegan","dairyfree"]},{"ID":"11","Name":"Fruit Salad","Ingredients":["Mixed Fruit (e.g., Pineapple, Mango, Strawberry, Blueberry)"],"Price":4.0,"DietaryInfo":["vegetarian","vegan","glutenfree","dairyfree"]}]