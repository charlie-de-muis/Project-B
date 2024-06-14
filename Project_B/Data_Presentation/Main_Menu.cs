// Made by Olivier
public class Main_Menu
{
    public static object account = null;

    public static void MainMenu()
    {
        while (true)
        {
            Log_in.CheckAdmin();

            Program.ConsoleClear();
            int choice = PrintMenuChoices(account);

            // Main Menu when Admin is logged in
            if (account is Admin)
            {
                if (choice == 4) { Console.WriteLine("You exited the program."); break; }
                switch (choice)
                {
                    case 0: account = null; continue;
                    case 1: MenuManager.ViewMenu(); continue;
                    case 2: PrintAboutText(); continue;
                    case 3: Admin_Menu.AdminMenu(); continue;
                }
            }

            // Main Menu when Customer is logged in
            if (account is Customer)
            {
                if (choice == 5) { Console.WriteLine("You exited the program."); break; }
                switch (choice)
                {
                    case 0: account = null; continue;
                    case 1: MenuManager.ViewMenu(); continue;
                    case 2: ReservationSystem.ReservationMenu(account as Customer); continue;
                    case 3: PreviousReservation.PreRes(account as Customer); continue;
                    case 4: PrintAboutText(); continue;
                }
            }

            // Main Menu when logged out
            else
            {
                if (choice == 3) { Console.WriteLine("You exited the program."); break; }
                switch (choice)
                {
                    case 0: account = Log_in.Option(); continue;
                    case 1: MenuManager.ViewMenu(); continue;
                    case 2: PrintAboutText(); continue;
                }
            }
        }
    }

    public static void PrintAboutText()
    {
        string text = TXT.ReadFromTXT(false);
        if (text != null)
        {
            Console.WriteLine(text);
        }
        Console.WriteLine("Press enter to return..."); Console.ReadLine(); Program.ConsoleClear();
    }

    public static void AboutTextAdmin(bool isTest = false)
    {
        Console.WriteLine("Type 3 lines of information about your restaurant:");
        string folderPath;
        if (isTest){folderPath = Path.Combine("../../../..", "Project_B", "Data_Sources");}
        else {folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");}
        string filePath = Path.Combine(folderPath, "restauranttext.txt");
        using (FileStream fs = new FileStream(filePath, FileMode.Truncate))
        {
            // clearing file
        }
        TXT.WritetoTXT(isTest);
        TXT.WritetoTXT(isTest);
        TXT.WritetoTXT(isTest);
        Program.ConsoleClear();
    }

    private static int PrintMenuChoices(object account)
    {
        if (account is Admin)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine($"║ Logged in ► {(account as Admin).UserName.PadRight(26)}║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            string prompt = $"╔═══════════════════════════════════════╗\n║ Restaurant Booking System.            ║\n╠═══════════════════════════════════════╣";
            string[] options = { "Log Out", "View Menu", "About Restaurant", "Admin Options", "Quit" };
            return ConsoleGUI.OptionGUI(prompt, options, 2);
        }
        else if (account is Customer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine($"║ Logged in ► {(account as Customer).UserName.PadRight(26)}║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            string prompt = $"╔═══════════════════════════════════════╗\n║ Restaurant Booking System.            ║\n╠═══════════════════════════════════════╣";
            string[] options = { "Log Out", "View Menu", "Make Reservation", "View Past Reservations", "About Restaurant", "Quit" };
            return ConsoleGUI.OptionGUI(prompt, options, 2);
        }
        else
        {
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║ Not Logged in                         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.WriteLine();
            
            string prompt = $"╔═══════════════════════════════════════╗\n║ Restaurant Booking System.            ║\n╠═══════════════════════════════════════╣";
            string[] options = { "Make Account / Login", "View Menu", "About Restaurant", "Quit" };
            return ConsoleGUI.OptionGUI(prompt, options, 2);
        }
    }
}
