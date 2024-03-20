class Program
{
    public static void Main()
    {
        Console.WriteLine(PasswordEncoding.EncodeString("Hallo"));
        Console.WriteLine(PasswordEncoding.DecodeString("ZsDDG"));
    }

    // assert.isequal unit test om te checken of de woorden goed encoded en decoded worden.
    // assert.isequal unit test om te checken of hij errors goed kan handelen.
}