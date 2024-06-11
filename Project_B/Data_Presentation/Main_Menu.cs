// Made by Olivier

class Main_Menu
{
    public static object account = null;

    public static void MainMenu()
    {
        while (true)
        {
            Log_in.CheckAdmin();

            Program.ConsoleClear();
            PrintMenuChoices(account);

            int choice;
            try { choice = int.Parse(Console.ReadLine()); Program.ConsoleClear(); }
            catch
            {
                Program.ConsoleClear();
                Console.WriteLine("Invalid choice. Press enter to continue...");
                Console.ReadLine();
                continue;
            }

            // Main Menu when Admin is logged in
            if (account is Admin)
            {
                if (choice == 5) { Console.WriteLine("You exited the program."); break; }
                switch (choice)
                {
                    case 1: account = null; continue;
                    case 2: MenuManager.ViewMenu(); continue;
                    case 3: PrintAboutText(); continue;
                    case 4: Admin_Menu.AdminMenu(); continue;
                }
                Console.WriteLine("Invalid choice. Press enter to continue...");
                Console.ReadLine(); Program.ConsoleClear(); continue;
            }

            // Main Menu when Customer is logged in
            if (account is Customer)
            {
                if (choice == 6) { Console.WriteLine("You exited the program."); break; }
                switch (choice)
                {
                    case 1: account = null; continue;
                    case 2: MenuManager.ViewMenu(); continue;
                    case 3: ReservationSystem.ReservationMenu(account as Customer); continue;
                    case 4: PreviousReservation.PreRes(account as Customer); continue;
                    case 5: PrintAboutText(); continue;
                }
                Console.WriteLine("Invalid choice. Press enter to continue...");
                Console.ReadLine(); Program.ConsoleClear(); continue;
            }

            // Main Menu when logged out
            else
            {
                if (choice == 4) { Console.WriteLine("You exited the program."); break; }
                // switch (choice)
                // {
                //     case 1: account = Log_in.Option(); continue;
                //     case 2: MenuManager.ViewMenu(); continue;
                //     case 3: PrintAboutText(); continue;
                // }
                Console.WriteLine("Invalid choice. Press enter to continue...");
                Console.ReadLine(); Program.ConsoleClear(); continue;
            }
        }
    }

    public static void PrintAboutText()
    {
        string text = TXT.ReadFromTXT();
        if (text != null)
        {
            Console.WriteLine(text);
        }
        Console.WriteLine("Press enter to return..."); Console.ReadLine(); Program.ConsoleClear();
    }

    public static void AboutTextAdmin()
    {
        Console.WriteLine("Type 3 lines of information about your restaurant:");
        TXT.WritetoTXT();
        TXT.WritetoTXT();
        TXT.WritetoTXT();
        Program.ConsoleClear();
    }

    private static void PrintMenuChoices(object account)
    {
        if (account is Admin)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine($"║ Logged in ► {(account as Admin).UserName.PadRight(26)}║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Restaurant Booking System.            ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Log Out                            ║");
            Console.WriteLine("║ 2. View Menu                          ║");
            Console.WriteLine("║ 3. About Restaurant                   ║");
            Console.WriteLine("║ 4. Admin Options                      ║");
            Console.WriteLine("║ 5. Quit                               ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║ Enter your choice.                    ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
        }
        else if (account is Customer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine($"║ Logged in ► {(account as Customer).UserName.PadRight(26)}║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Restaurant Booking System.            ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Log Out                            ║");
            Console.WriteLine("║ 2. View Menu                          ║");
            Console.WriteLine("║ 3. Make Reservation                   ║");
            Console.WriteLine("║ 4. View Past Reservations             ║");
            Console.WriteLine("║ 5. About Restaurant                   ║");
            Console.WriteLine("║ 6. Quit                               ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║ Enter your choice.                    ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
        }
        else
        {
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Not Logged in                         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Restaurant Booking System.            ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Make Account / Login               ║");
            Console.WriteLine("║ 2. View Menu                          ║");
            Console.WriteLine("║ 3. About Restaurant                   ║");
            Console.WriteLine("║ 4. Quit                               ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║ Enter your choice.                    ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
        }
    }
}
