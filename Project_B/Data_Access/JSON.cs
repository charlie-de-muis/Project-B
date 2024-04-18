using Newtonsoft.Json;

public class JSON
{
    // Hoe willen we de data uploaden? --> data wordt ingevuld door de admin in het programma, en dan geupload 
    // uit overleg met Cigdem 2-4-2024s

    public static void WriteJSON(List<MenuItem> ValueToWrite)
    {
        // deze try catch blokken zorgen ervoor dat de code werkt op verschillende computers. 
        // De ene zoekt in de bin map, de ander in de main project map
        try
        {
            // Folder path where you want to store the JSON
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, "Menu.json");

            List<MenuItem> Menu = ReadJSON();
            Menu.AddRange(ValueToWrite);

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
        }

        catch
        {
            // Folder path where you want to store the JSON
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, "Menu.json");

            List<MenuItem> Menu = ReadJSON();
            Menu.AddRange(ValueToWrite);

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
        }
    }

    public static void DeletedItemsWriteJSON(List<MenuItem> Menu)
    {
        // deze try catch blokken zorgen ervoor dat de code werkt op verschillende computers. 
        // De ene zoekt in de bin map, de ander in de main project map
        try
        {
            // Folder path where you want to store the JSON
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, "Menu.json");

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
        }

        catch
        {
            // Folder path where you want to store the JSON
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, "Menu.json");

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
        }
    }

    public static List<MenuItem> ReadJSON()
    {
        // create an empty Streamreader
        StreamReader reader = null!;

        try
        {
            try
            {
                // Folder path where you want to read the JSON
                string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources");

                // File path within the folder
                string filePath = Path.Combine(folderPath, "Menu.json");

                reader = new StreamReader(filePath);
                
                // read all the data
                string jsonString = reader.ReadToEnd();

                // add all the data to a list
                List<MenuItem> Menu = JsonConvert.DeserializeObject<List<MenuItem>>(jsonString)!;

                return Menu;
            }

            catch (FileNotFoundException e){}
                // {Console.WriteLine($"Missing JSON file. {e.Message}");}

            catch (JsonReaderException e)
                {Console.WriteLine($"Invalid JSON. {e.Message}");}

            finally
                {reader?.Close();}

            // return empty list for errors
            return new List<MenuItem>();
        }
        catch
        {   
            // Folder path where you want to read the JSON
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources");

            // File path within the folder
            string filePath = Path.Combine(folderPath, "Menu.json");

            try
            {
                reader = new StreamReader(filePath);
                
                // read all the data
                string jsonString = reader.ReadToEnd();

                // add all the data to a list
                List<MenuItem> Menu = JsonConvert.DeserializeObject<List<MenuItem>>(jsonString)!;

                return Menu;
            }

            catch (FileNotFoundException e)
                {Console.WriteLine($"Missing JSON file. {e.Message}");}

            catch (JsonReaderException e)
                {Console.WriteLine($"Invalid JSON. {e.Message}");}

            finally
                {reader?.Close();}

            // return empty list for errors
            return new List<MenuItem>();
        }
    }
}