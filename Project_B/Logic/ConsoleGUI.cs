static class ConsoleGUI
{
    public static int OptionGUI(string prompt, string[] options)
    {
        int selectedIndex = 0;

        while (true)
        {
            Program.ConsoleClear();
            PrintGUI(prompt, options, selectedIndex);

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
}