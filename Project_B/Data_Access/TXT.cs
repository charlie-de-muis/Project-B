public static class TXT
{
    // Read from the text file
    public static string ReadFromTXT()
    {
        try
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");
            string filePath = Path.Combine(folderPath, "restauranttext.txt");

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return text;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");
            string filePath = Path.Combine(folderPath, "restauranttext.txt");
        
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return text;
            }
            else
            {
                return null;
            } 
        }
    }

    // Write to the text file (this is the append one, there needs to be an update one soon too)
    public static void WritetoTXT()
    {
        try
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");
            string filePath = Path.Combine(folderPath, "restauranttext.txt");

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            if (File.Exists(filePath))
            {
                string text = Console.ReadLine();
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(text);
                }
            }
        }
        catch
        {
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");
            string filePath = Path.Combine(folderPath, "restauranttext.txt");

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            if (File.Exists(filePath))
            {
                string text = Console.ReadLine();
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(text);
                }
            }            
        }
    }
}