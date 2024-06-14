// Everyone contributed

using Newtonsoft.Json;

public static class JSON
{
    public static void WriteJSON(List<MenuItem> ValueToWrite, string menuType, bool test)
    {
            // Folder path where you want to store the JSON --> bool test is true for unit tests,
            // false for the normal program
            string folderPath;
            if (test){folderPath = Path.Combine("../../../..", "Project_B", "Data_Sources\\Menu_Storage");}
            else {folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");}

            // File path within the folder
            string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Error: File not found, or the file is currently not available.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey();
            }

            List<MenuItem> Menu = ReadJSON(menuType, test);
            Menu.AddRange(ValueToWrite);

            // writing the data
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(JsonConvert.SerializeObject(Menu));
            }        
    }

    public static void DeletedItemsWriteJSON(List<MenuItem> Menu, string menuType, bool test = false)
    {
            // Folder path where you want to store the JSON --> bool test is true for unit tests,
            // false for the normal program
            string folderPath;
            if (test){folderPath = Path.Combine("../../../..", "Project_B", "Data_Sources\\Menu_Storage");}
            else {folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");}

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

    public static List<MenuItem> ReadJSON(string menuType, bool test)
    {
        // create an empty Streamreader
        StreamReader reader = null!;

            try
            {
                // Folder path where you want to read the JSON --> bool test is true for unit tests,
                // false for the normal program
                string folderPath;
                if (test){folderPath = Path.Combine("../../../..", "Project_B", "Data_Sources\\Menu_Storage");}
                else {folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");}
                
                // File path within the folder
                string filePath = Path.Combine(folderPath, $"{GetCurrentMenuName(folderPath, menuType)}.json");

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

            catch (FileNotFoundException e)
                {Console.WriteLine($"Missing JSON file. {e.Message}");}

            catch (JsonReaderException e)
                {Console.WriteLine($"Invalid JSON. {e.Message}");}

            finally
                {reader?.Close();}

            // return empty list for errors
            return new List<MenuItem>();
    }

    public static List<string> ReadMenusJSON()
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

            catch (FileNotFoundException e)
                {Console.WriteLine($"Missing JSON file. {e.Message}");}

            catch (JsonReaderException e)
                {Console.WriteLine($"Invalid JSON. {e.Message}");}

            // return empty list for errors
            return new List<string>();
    }

    public static void SetMenuAs(string menuType, string fileName)
    {
            // search for the files
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources", "Menu_Storage");
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string menuTypeName = GetCurrentMenuName(folderPath, menuType);

            if (!string.IsNullOrEmpty(menuTypeName))
            {
                SwitchMenu(fileName, menuType);
            }
            else
            {
                // if file is found, it will change its name to Menu_current_dd-MM-yyyy.json
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found. Please make sure the file name is correct and try again.");
                    return;
                }

                string newCurrentName = Path.Combine(folderPath, $"{menuType}_{DateTime.Now:dd-MM-yyyy}.json");

                try
                {
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
            string folderPath = Path.Combine(Environment.CurrentDirectory, "Data_Sources\\Menu_Storage");
            string fullPath = Path.Combine(folderPath, menuFileName);
            string menuTypeName = GetCurrentMenuName(folderPath, menuType);

            if (string.IsNullOrEmpty(menuTypeName))
            {
                Console.WriteLine($"No {menuType} found, upload a new {menuType} file.");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey(); return;
            }

            string fullPathMenuType = Path.Combine(folderPath, $"{menuTypeName}.json");

            if (!File.Exists(fullPath) || !File.Exists(fullPathMenuType))
            {
                Console.WriteLine($"One or both files don't exist");
                Console.WriteLine("Press any key to continue..."); Console.ReadKey(); return;
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

    public static void DeleteMenu(string menuFileName)
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

    private static string GetCurrentMenuName(string folderPath, string menuType)
    {
        List<string> menuNames = Directory.GetFiles(folderPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToList()!;

        return menuNames.FirstOrDefault(menuName => menuName.Contains(menuType))!;
    }
}