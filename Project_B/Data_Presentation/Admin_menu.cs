public class Admin_Menu
{
    public static void AdminMenu()
    {
        while (true)
        {
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Welcome to Admin menu!                ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Add Menu Items                     ║");
            Console.WriteLine("║ 2. Delete Menu Items                  ║");
            Console.WriteLine("║ 3. View Reservations                  ║");
            Console.WriteLine("║ 4. Edit Reservations                  ║");
            Console.WriteLine("║ 5. Add / Edit Restaurant Info         ║");
            Console.WriteLine("║ 6. Manage Menu's                      ║");
            Console.WriteLine("║ 7. Back To The Main Menu              ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║ Enter your choice.                    ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");

            int choice;
            try { choice = int.Parse(Console.ReadLine()); Program.ConsoleClear(); }
            catch
            {
                Program.ConsoleClear();
                Console.WriteLine("Invalid choice. Press enter to continue...");
                Console.ReadLine(); Program.ConsoleClear();
                continue;
            }

            if (choice == 7) { break; }
            switch (choice)
            {
                case 1: Add_Item.Add_Items(); continue;
                case 2: Delete_Item.Delete_Items(); continue;
                case 3: ReservationSystem.ViewReservations(); continue;
                case 4: EditReservations.Choose_Reservation(); continue;
                case 5: Main_Menu.AboutTextAdmin(); continue;
                case 6: MenuManager.ManageAllMenus(); continue;
            }
            Console.WriteLine("Invalid choice. Press enter to continue...");
            Console.ReadLine(); Program.ConsoleClear(); continue;
        }
    }
}
