// Made by Tiffany
public class Admin_Menu
{
    public static void AdminMenu()
    {
        while (true)
        {
            string prompt = $"╔═══════════════════════════════════════╗\n║ Welcome to Admin menu!                ║\n╠═══════════════════════════════════════╣";
            string[] options = { "Add Menu Items", "Delete Menu Items", "View Reservations", "Edit Reservations", "Add / Edit Restaurant Info", "Managa Menu's", "Back To The Main Menu" };
            int choice = ConsoleGUI.OptionGUI(prompt, options, 2);

            if (choice == 6) { break; }
            switch (choice)
            {
                case 0: Add_Item.Add_Items(); continue;
                case 1: Delete_Item.Delete_Items(); continue;
                case 2: ReservationSystem.ViewReservations(); continue;
                case 3: EditReservations.Choose_Reservation(); continue;
                case 4: Main_Menu.AboutTextAdmin(); continue;
                case 5: MenuManager.ManageAllMenus(); continue;
            }
            Console.WriteLine("Invalid choice. Press enter to continue...");
            Console.ReadLine(); Program.ConsoleClear(); continue;
        }
    }
}
