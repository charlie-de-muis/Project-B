static class ConsoleGUI
{
    public static int OptionGUI(string prompt, string[] options, int printID)
    {
        int selectedIndex = 0;

        while (true)
        {
            Program.ConsoleClear();

            switch (printID)
            {
                case 1: PrintGUI(prompt, options, selectedIndex); break;
                case 2: PrintMenuGUI(prompt, options, selectedIndex); break;
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
            {
                selectedIndex++;
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Program.ConsoleClear();
                return selectedIndex;
            }
        }
    }

    private static void PrintGUI(string prompt, string[] options, int selectedIndex)
    {
        Console.WriteLine(prompt);

        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.WriteLine($"► {options[i]}");
            }
            else
            {
                Console.WriteLine(options[i]);
            }
        }
    }

    private static void PrintMenuGUI(string prompt, string[] options, int selectedIndex)
    {
        Console.WriteLine(prompt);

        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.WriteLine($"║ ► {options[i]}".PadRight(40) + "║");
            }
            else
            {
                Console.WriteLine($"║ {options[i]}".PadRight(40) + "║");
            }
        }

        Console.WriteLine("╚═══════════════════════════════════════╝");
    }
}