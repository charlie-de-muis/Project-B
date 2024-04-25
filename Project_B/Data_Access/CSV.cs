class CSV
{
    public static List<Account> ReadFromCSV(string filename)
    {
        try
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);
            try
        {

            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Username;Password;Email\n");
            }

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
        catch
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);
            try
        {

            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Username;Password;Email\n");
            }

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
    }
    

    public static List<Reservation> ReadFromCSVReservations(string filename)
    {//datum, tijdslot, tafel, naam, reserveringscode
        try
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);
            try
        {

            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Amount of persons;Reservationcode\n");
            }

            using(var reader = new StreamReader(filePath))
            {
                List<Reservation> reservations = new List<Reservation>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string[] values = line.Split(";");
                    List<string> tables = values[2].Split(",").ToList();
                    List<int> tablesInt = new();
                    for (int i = 0; i < tables.Count; i++)
                        tablesInt.Add(Convert.ToInt32(tables[i]));

                    reservations.Add(new Reservation(values[0], values[1], tablesInt, values[3], Convert.ToInt32(values[4]), values[5]));
                }
                return reservations;
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found {ex}");
            return null;
        }
        }
        catch
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);
            try
        {

            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Amount of persons;Reservationcode\n");
            }

            using(var reader = new StreamReader(filePath))
            {
                List<Reservation> reservations = new List<Reservation>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    string[] values = line.Split(";");
                    List<string> tables = values[2].Split(",").ToList();
                    List<int> tablesInt = new();
                    for (int i = 0; i < tables.Count; i++)
                        tablesInt.Add(Convert.ToInt32(tables[i]));

                    reservations.Add(new Reservation(values[0], values[1], tablesInt, values[3], Convert.ToInt32(values[4]), values[5]));
                }
                return reservations;
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found {ex}");
            return null;
        }
        }
    }
    public static void WriteToCSV(Account account, string filename)
    {
        try
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);

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
        catch
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);

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

    public static void WriteToCSVReservations(Reservation reservation, string filename)
    {
        try
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);

            // Check if the file already exists, if not create a new file and write headers
            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Amount of persons;Reservationcode\n");
            }

            // Write new data to the CSV file
            string userDataString = $"{reservation.Date};{reservation.TimeSlot};{reservation.Table};{reservation.CustomerName};{reservation.AmountofPersons};{reservation.ReservationCode}\n";

            File.AppendAllText(filePath, userDataString);
        }
        catch
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, filename);

            // Check if the file already exists, if not create a new file and write headers
            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Amount of persons;Reservationcode\n");
            }

            // Write new data to the CSV file
            string userDataString = $"{reservation.Date};{reservation.TimeSlot};{reservation.Table};{reservation.CustomerName};{reservation.AmountofPersons};{reservation.ReservationCode}\n";

            File.AppendAllText(filePath, userDataString);
        }
    }
}
