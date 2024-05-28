class CSV
{
    public static List<Account> ReadFromCSV()
    {
        try
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, "account_data.csv");
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
            string filePath = Path.Combine(folderPath, "account_data.csv");
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
                    File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Email;Amount of persons;Reservationcode;Date of booking\n");
                }

                using(var reader = new StreamReader(filePath))
                {
                    List<Reservation> reservations = new List<Reservation>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line == "Date;Timeslot;Table;Name;Email;Amount of persons;Reservationcode;Date of booking") { continue; }

                        string[] values = line.Split(";");
                        List<string> tables = values[2].Split(",").ToList();
                        List<int> tablesInt = new();
                        for (int i = 0; i < tables.Count; i++)
                        {
                            if (int.TryParse(tables[i], out int tableInt))
                            {
                                tablesInt.Add(tableInt);
                            }
                        }

                        Dictionary<int, int> menuOrders = new Dictionary<int, int>();

                        List<string> parts = values[6].Split('-').ToList();
                        foreach (string part in parts)
                        {
                            List<string> keyValue = part.Split(',').ToList();
                            if (int.TryParse(keyValue[0], out int key))
                            {
                                int value = int.Parse(keyValue[1]);
                                menuOrders[key] = value;
                            }
                        }

                        int NumPers = 0;
                        if (int.TryParse(values[5], out int PersInt))
                        {
                            NumPers = PersInt;
                        }

                        reservations.Add(new Reservation(values[0], values[1], tablesInt, values[3], values[4], NumPers, menuOrders, values[7], values[8]));
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
                    File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Email;Amount of persons;MenuOrders;Reservationcode;Date of booking\n");
                }

                using(var reader = new StreamReader(filePath))
                {
                    List<Reservation> reservations = new List<Reservation>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line == "Date;Timeslot;Table;Name;Email;Amount of persons;MenuOrders;Reservationcode;Date of booking") { continue; }

                        string[] values = line.Split(";");
                        List<string> tables = values[2].Split(",").ToList();
                        List<int> tablesInt = new();
                        for (int i = 0; i < tables.Count; i++)
                        {
                            if (int.TryParse(tables[i], out int tableInt))
                            {
                                tablesInt.Add(tableInt);
                            }
                        }

                        Dictionary<int, int> menuOrders = new Dictionary<int, int>();

                        List<string> parts = values[6].Split('-').ToList();
                        foreach (string part in parts)
                        {
                            List<string> keyValue = part.Split(',').ToList();
                            if (int.TryParse(keyValue[0], out int key))
                            {
                                int value = int.Parse(keyValue[1]);
                                menuOrders[key] = value;
                            }
                        }

                        int NumPers = 0;
                        if (int.TryParse(values[5], out int PersInt))
                        {
                            NumPers = PersInt;
                        }

                        reservations.Add(new Reservation(values[0], values[1], tablesInt, values[3], values[4], NumPers, menuOrders, values[7], values[8]));
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
    public static void WriteToCSV(Account account)
    {
        try
        {
            // Folder path where you want to store the CSV
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, "account_data.csv");

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
            string filePath = Path.Combine(folderPath, "account_data.csv");

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
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Email;Amount of persons;MenuOrders;Reservationcode;Date of booking\n");
            }

            string menuOrders = "";
            
            foreach (var kvp in reservation.MenuOrders)
            {
                menuOrders += $"{kvp.Key},{kvp.Value}-";
            }
            // Write new data to the CSV file
            string userDataString = $"{reservation.Date};{reservation.TimeSlot};" + string.Join(",", reservation.Table) + $";{reservation.CustomerName};{reservation.CustomerEmail};{reservation.AmountofPersons};{menuOrders};{reservation.ReservationCode};{reservation.DateOfBooking}\n";

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
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Email;Amount of persons;MenuOrders;Reservationcode;Date of booking\n");
            }

            string menuOrders = "";
            
            foreach (var kvp in reservation.MenuOrders)
            {
                menuOrders += $"{kvp.Key},{kvp.Value}-";
            }
            // Write new data to the CSV file
            string userDataString = $"{reservation.Date};{reservation.TimeSlot};" + string.Join(",", reservation.Table) + $";{reservation.CustomerName};{reservation.CustomerEmail};{reservation.AmountofPersons};{menuOrders};{reservation.ReservationCode};{reservation.DateOfBooking}\n";

            File.AppendAllText(filePath, userDataString);
        }
    }

    public static void Update_CSV_Reservations(Reservation reservation)
    {
        // replacing the old reservation
        List<Reservation> old_reservations = ReadFromCSVReservations("Reservation.csv");

        for (int i = 0; i < old_reservations.Count; i++)
        {
            if (old_reservations[i].ReservationCode == reservation.ReservationCode && old_reservations[i].DateOfBooking == reservation.DateOfBooking)
            {
                old_reservations[i] = reservation;
            }
        }


        // loading the file
        string folderPath;
        string filePath;

        try
        {
            folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");
            filePath = Path.Combine(folderPath, "Reservation.csv");

            // Check if the file already exists, if not create a new file and write headers
            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Email;Amount of persons;Reservationcode;Date of booking\n");
            }
        }
        catch
        {
            folderPath = Path.Combine("..", "..", "..", "Data_Sources");
            filePath = Path.Combine(folderPath, "Reservation.csv");

            // Check if the file already exists, if not create a new file and write headers
            if (!File.Exists(filePath))
            {
                // Write headers
                File.WriteAllText(filePath, "Date;Timeslot;Table;Name;Email;Amount of persons;Reservationcode;Date of booking\n");
            }
        }

        // clearing the file
        File.WriteAllText(filePath, string.Empty);


        // Write new data to the CSV file
        foreach (Reservation r in old_reservations)
        {
            string menuOrders = "";
            
            foreach (var kvp in r.MenuOrders)
            {
                menuOrders += $"{kvp.Key},{kvp.Value}-";
            }

            string userDataString = $"{r.Date};{r.TimeSlot};" + string.Join(",", r.Table) + $";{r.CustomerName};{r.CustomerEmail};{r.AmountofPersons};{menuOrders};{r.ReservationCode};{r.DateOfBooking}\n";
            File.AppendAllText(filePath, userDataString);
        }
    }
}
