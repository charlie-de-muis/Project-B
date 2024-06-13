// Made by Orestis

public static class TXT
{
    // Read from the text file
    public static string ReadFromTXT(bool test)
    {
            // Folder path where you want to store the CSV --> bool test is true for unit tests,
            // false for the normal program
            string folderPath;
            if (test){folderPath = Path.Combine("../../../..", "Project_B", "Data_Sources");}
            else {folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");}
            string filePath = Path.Combine(folderPath, "restauranttext.txt");

            // create a file if it doesn't exist
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            if (File.Exists(filePath))
            {
                // add the data to the file
                string text = File.ReadAllText(filePath);
                return text;
            }
            else
            {
                return null;
            }
    }

    // Write to the text file (this is the append one, there needs to be an update one soon too)
    public static void WritetoTXT(bool test)
    {
            // Folder path where you want to store the CSV --> bool test is true for unit tests,
            // false for the normal program
            string folderPath;
            if (test){folderPath = Path.Combine("../../../..", "Project_B", "Data_Sources");}
            else {folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");}
            string filePath = Path.Combine(folderPath, "restauranttext.txt");

            // create a file if it doesn't exist
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            if (File.Exists(filePath))
            {
                // retrieve the data from the file
                string text = Console.ReadLine();
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(text);
                }
            }
    }
}