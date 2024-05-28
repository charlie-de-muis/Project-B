public class EditReservations
{
    // bij de input wordt de reservering meegegeven
    // daarna wordt er gevraagd wat ze willen aanpassen
    // generics + constraint --> input kan een string, int of list<int> zijn
    // dat specifieke stukje in het reserveringsobject wordt aangepast
    // de aanpassing wordt geupload naar het csv bestand
    public static void Choose_Reservation()
    {
        List<Reservation> file = CSV.ReadFromCSVReservations("Reservation.csv");

        foreach (Reservation r in file)
        {
            if (r.Date == "Date") { continue; }
            
            List<int> keys = r.MenuOrders.Keys.ToList();
            string menuOrdersSTR = @"""order"" x ""count""";
            foreach (int key in keys) { menuOrdersSTR += $" : {key} x {r.MenuOrders[key]}"; }
            
            Console.WriteLine(@$"
Date: {r.Date}
Timeslot: {r.TimeSlot}
Table(s): {string.Join(",", r.Table)}
Customer name: {r.CustomerName}
Customer email: {r.CustomerEmail}
Amount of persons: {r.AmountofPersons}
Menu Orders: {menuOrdersSTR}
Reservation code: {r.ReservationCode}
Booking date: {r.DateOfBooking}
");
        }

        Console.WriteLine("Please enter the reservationcode of the reservation you want to change:");
        string code = Console.ReadLine();

        Edit_Reservations(file.Find(r => r.ReservationCode == code));
    }

    private static void Edit_Reservations(Reservation? reservation)
    {
        while (true)
        {
            Console.WriteLine(@"What do you want to change?
1. Date
2. Timeslot
3. Table
4. Customer name
5. Customer email
6. Amount of persons
7. Return to admin menu");

            int choice = 0;
            try { choice = int.Parse(Console.ReadLine()); Program.ConsoleClear(); }
            catch
            {
                Program.ConsoleClear();
                Console.WriteLine("Invalid choice. Press enter to continue...");
                Console.ReadLine();
            }

            if (choice == 7){break;}
            Console.WriteLine("Please enter the new data.");

            switch (choice)
            {
                // date -> string
                case 1: 
                    Console.WriteLine("Valid date format: DD-MM-YYYY");
                    string date = Console.ReadLine();
                    if (date.Contains("-")){Change_reservation(date, "date", reservation); break;}
                    else{Console.WriteLine("Invalid format"); continue;}

                // timeslot -> string
                case 2: Change_reservation(Console.ReadLine(), "time", reservation); break; 

                // tables -> list<int>
                case 3: 
                    Console.WriteLine("Seperate the tables with a comma (,)");
                    Change_reservation(Console.ReadLine(), reservation); break; 

                // name -> string
                case 4: Change_reservation(Console.ReadLine(), "name", reservation); break; 

                // email -> string
                case 5:  
                    string email = Console.ReadLine();
                    if (email.Contains("@") && email.Contains(".")){Change_reservation(email, "email", reservation); break;}
                    else{Console.WriteLine("Invalid format"); continue;}

                // persons -> int
                case 6: Change_reservation(int.Parse(Console.ReadLine()), reservation); break; 
                default: Console.WriteLine("Invalid choice."); continue;
            }
        }
    }

// hier is van allebei maar 1 optie
    private static void Change_reservation<T>(T new_data, Reservation reservation)
    {
        if (new_data is string)
        {   // maakt van de string een lijst met integers
            reservation.Table = Convert.ToString(new_data).Split(",").Select(int.Parse).ToList();

            CSV.Update_CSV_Reservations(reservation);

            Console.WriteLine("Reservation updated.");
        }
        else if (new_data is int)
        {
            reservation.AmountofPersons = Convert.ToInt32(new_data);
            CSV.Update_CSV_Reservations(reservation);

            Console.WriteLine("Reservation updated.");
        }
    }

// meegeven wat er aangepast wordt
    private static void Change_reservation(string new_data, string type_of_data, Reservation reservation)
    {
        switch (type_of_data)
        {
            case "date": 
                reservation.Date = new_data; 
                CSV.Update_CSV_Reservations(reservation); 
                Console.WriteLine("Reservation updated."); 
                break;
            case "time":
                reservation.TimeSlot = new_data; 
                CSV.Update_CSV_Reservations(reservation); 
                Console.WriteLine("Reservation updated."); 
                break;
            case "name": 
                reservation.CustomerName = new_data; 
                CSV.Update_CSV_Reservations(reservation); 
                Console.WriteLine("Reservation updated."); 
                break;
            case "email": 
                reservation.CustomerEmail = new_data; 
                CSV.Update_CSV_Reservations(reservation); 
                Console.WriteLine("Reservation updated."); 
                break;
        }
    }
}