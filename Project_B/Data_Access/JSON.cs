using Newtonsoft.Json;

public class JSON
{
    // Hoe willen we de data uploaden? --> data wordt ingevuld door de admin in het programma, en dan geupload 
    // uit overleg met Cigdem 2-4-2024s

    public static void WriteJSON(List<MenuItem> ValueToWrite, string menuType)
    {
        // deze try catch blokken zorgen ervoor dat de code werkt op verschillende computers. 
        // De ene zoekt in de bin map, de ander in de main project map
        try
        {
            // Folder path where you want to store the JSON
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");

            // File path within the folder
            string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error: File not found, or the file is currently not available.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            List<MenuItem> Menu = ReadJSON(menuType);
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
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources\\Menu_Storage");

            // File path within the folder
            string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error: File not found, or the file is currently not available.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            List<MenuItem> Menu = ReadJSON(menuType);
            Menu.AddRange(ValueToWrite);

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
        }
    }

    public static void DeletedItemsWriteJSON(List<MenuItem> Menu, string menuType)
    {
        // deze try catch blokken zorgen ervoor dat de code werkt op verschillende computers. 
        // De ene zoekt in de bin map, de ander in de main project map
        try
        {
            // Folder path where you want to store the JSON
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");

            // File path within the folder
            string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error: File not found, or the file is currently not available.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
        }

        catch
        {
            // Folder path where you want to store the JSON
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources\\Menu_Storage");

            // File path within the folder
            string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error: File not found, or the file is currently not available.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
        }
    }

    public static List<MenuItem> ReadJSON(string menuType)
    {
        // create an empty Streamreader
        StreamReader reader = null!;

        try
        {
            try
            {
                // Folder path where you want to read the JSON
                string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");

                // File path within the folder
                string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

                Console.WriteLine($"{GetCurrentMenuName(folderPath, menuType)}.json");

                if (string.IsNullOrEmpty(filePath))
                {
                    Console.WriteLine("Error: File not found, or the file is currently not available.");
                    Console.WriteLine("Press any key to continue..."); Console.ReadKey();
                }

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
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources\\Menu_Storage");

            // File path within the folder
            string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error: File not found, or the file is currently not available.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

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

    public static List<string> ReadMenusJSON()
    {
        try
        {
            try
            {
                // Folder path where you want to read the JSON
                string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    return new List<string>();
                }

                return Directory.GetFiles(folderPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToList()!;
            }

            catch (FileNotFoundException e){}
                // {Console.WriteLine($"Missing JSON file. {e.Message}");}

            catch (JsonReaderException e)
                {Console.WriteLine($"Invalid JSON. {e.Message}");}

            // return empty list for errors
            return new List<string>();
        }
        catch
        {   
            try
            {
                string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    return new List<string>();
                }

                return Directory.GetFiles(folderPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToList()!;
            }

            catch (FileNotFoundException e)
                {Console.WriteLine($"Missing JSON file. {e.Message}");}

            catch (JsonReaderException e)
                {Console.WriteLine($"Invalid JSON. {e.Message}");}

            // return empty list for errors
            return new List<string>();
        }
    }

    public static void SetMenuAs(string menuType, string fileName)
    {
        try
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources", "Menu_Storage");
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found. Please make sure the file name is correct and try again.");
                return;
            }

            string newCurrentName = Path.Combine(folderPath, $"Menu_{menuType}_{DateTime.Now:dd-MM-yyyy}.json");
            string previousFileRename = Path.Combine(folderPath, $"Menu_{DateTime.Now:dd-MM-yyyy}.json");

            try
            {
                string currentMenuName = GetCurrentMenuName(folderPath, menuType);

                if (!string.IsNullOrEmpty(currentMenuName))
                {
                    string filePathToCurrentMenu = Path.Combine(folderPath, $"{currentMenuName}.json");

                    File.Copy(filePathToCurrentMenu, previousFileRename);
                    File.Delete(filePathToCurrentMenu);
                }

                File.Copy(filePath, newCurrentName);
                File.Delete(filePath);
                Console.WriteLine($"File uploaded successfully as {newCurrentName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the file: {ex.Message}");
            }
        }
        catch
        {
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources", "Menu_Storage");
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found. Please make sure the file name is correct and try again.");
                return;
            }

            string newCurrentName = Path.Combine(folderPath, $"Menu_{menuType}_{DateTime.Now:dd-MM-yyyy}.json");
            string previousFileRename = Path.Combine(folderPath, $"Menu_{DateTime.Now:dd-MM-yyyy}.json");

            try
            {
                string currentMenuName = GetCurrentMenuName(folderPath, menuType);

                if (!string.IsNullOrEmpty(currentMenuName))
                {
                    string filePathToCurrentMenu = Path.Combine(folderPath, $"{currentMenuName}.json");

                    File.Copy(filePathToCurrentMenu, previousFileRename);
                    File.Delete(filePathToCurrentMenu);
                }

                File.Copy(filePath, newCurrentName);
                File.Delete(filePath);
                Console.WriteLine($"File uploaded successfully as {newCurrentName}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the file: {ex.Message}");
            }
        }
    }

    public static void SwitchMenu(string menuFileName, string menuType)
    {
        try
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");
            string fullPath = Path.Combine(folderPath, $"{menuFileName}.json");
            string menuTypeName = GetCurrentMenuName(folderPath, menuType);

            if (string.IsNullOrEmpty(menuTypeName))
            {
                Console.WriteLine($"No {menuType} found, upload a new {menuType} file.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            string fullPathMenuType = Path.Combine(folderPath, $"{menuTypeName}.json");

            if (!File.Exists(fullPath) || !File.Exists(fullPathMenuType))
            {
                Console.WriteLine($"One or both files don't exist");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            try
            {
                string tempFileName1 = menuFileName + "_temp";
                string tempFileName2 = menuTypeName + "_temp";

                string tempFilePath1 = Path.Combine(folderPath, tempFileName1);
                string tempFilePath2 = Path.Combine(folderPath, tempFileName2);

                File.Move(fullPath, tempFilePath1);
                File.Move(fullPathMenuType, fullPath);
                File.Move(tempFilePath1, fullPathMenuType);

                Console.WriteLine($"{menuFileName} is now the current menu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while switching files: {ex.Message}");
            }
        }
        catch
        {
            string folderPath = Path.Combine("..", "..", "..", "Data_Sources\\Menu_Storage");
            string fullPath = Path.Combine(folderPath, $"{menuFileName}.json");
            string menuTypeName = GetCurrentMenuName(folderPath, menuType);

            if (string.IsNullOrEmpty(menuTypeName))
            {
                Console.WriteLine($"No {menuType} found, upload a new {menuType} file.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            string fullPathMenuType = Path.Combine(folderPath, $"{menuTypeName}.json");

            if (!File.Exists(fullPath) || !File.Exists(fullPathMenuType))
            {
                Console.WriteLine($"One or both files don't exist");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            try
            {
                string tempFileName1 = menuFileName + "_temp";
                string tempFileName2 = menuTypeName + "_temp";

                string tempFilePath1 = Path.Combine(folderPath, tempFileName1);
                string tempFilePath2 = Path.Combine(folderPath, tempFileName2);

                File.Move(fullPath, tempFilePath1);
                File.Move(fullPathMenuType, fullPath);
                File.Move(tempFilePath1, fullPathMenuType);

                Console.WriteLine($"{menuFileName} is now the current menu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while switching files: {ex.Message}");
            }
        }
    }

    public static void DeleteMenu(string menuFileName)
    {
        try
        {
            string fullPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage", menuFileName);

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                    Console.WriteLine($"{menuFileName} deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Menu file not found. Please make sure the file name is correct and try again.");
            }
        }
        catch
        {
            string fullPath = Path.Combine("..", "..", "..", "Data_Sources\\Menu_Storage", menuFileName);

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                    Console.WriteLine($"{menuFileName} deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Menu file not found. Please make sure the file name is correct and try again.");
            }
        }
    }

    private static string GetCurrentMenuName(string folderPath, string menuType)
    {
        try
        {
            folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");

            List<string> menuNames = Directory.GetFiles(folderPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToList()!;

            return menuNames.FirstOrDefault(menuName => menuName.Contains($"Menu_{menuType}"))!;
        }
        catch
        {
            folderPath = Path.Combine("..", "..", "..", "Data_Sources\\Menu_Storage");

            List<string> menuNames = Directory.GetFiles(folderPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToList()!;

            return menuNames.FirstOrDefault(menuName => menuName.Contains($"Menu_{menuType}"))!;
        }
    }
}