class CSV
{
    public static List<Account> ReadFromCSV()
    {
        try
        {
            string filePath = Path.GetFullPath("account_data.csv");

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            using(var reader = new StreamReader(filePath))
            {
                List<Account> accounts = new List<Account>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string[] values = line.Split(";");

                    accounts.Add(new Account(values[0], values[1], values[2]));
                }
                return accounts;
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found {ex}");
            return null;
        }
    }
    
    public static void WriteToCSV(Account account)
    {
        // Folder path where you want to store the CSV
        string folderPath = Path.Combine(Path.GetFullPath(Environment.CurrentDirectory), "Data_Sources");
        
        // File path where you want to store the CSV
        string filePath = Path.Combine(folderPath, Path.GetFullPath("account_data.csv"));

        // Check if the file already exists, if not create a new file and write headers
        if (!File.Exists(filePath))
        {
            // Write headers
            File.WriteAllText(filePath, "Username;Password;Email\n");
        }

        // Write new data to the CSV file
        string userDataString = $"{account.UserName};{account.PassWord};{account.Email}\n";

        File.AppendAllText(filePath, userDataString);
    }
}
