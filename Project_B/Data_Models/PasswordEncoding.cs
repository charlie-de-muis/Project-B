public class PasswordEncoding
{
    // allowed chars in password
    public const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!?@.,_-*#$%:<>=/";
    // dictionaries to convert chars
    private static readonly Dictionary<char,char> EncodeDict = AddPairs();
    private static readonly Dictionary<char,char> DecodeDict = AddPairsDecode();

    // fill the dictionary, this automatically adjusts if there are more chars added to the allowed chars
    private static Dictionary<char,char> AddPairs()
    {
        Dictionary<char,char> EncodeDict = new Dictionary<char, char>();
        for (int i = 0; i < Chars.Length; i++)
        {
            char key = Chars[i];
            char value;

            if (i + 18 >= Chars.Length)
            {
                value = Chars[i + 18 - Chars.Length];
                EncodeDict.Add(key, value);
            }
            else
            {
                value = Chars[i + 18];
                EncodeDict.Add(key, value);
            }
        }
        return EncodeDict;
    }

    // fill the decode dictionary
    private static Dictionary<char,char> AddPairsDecode()
    {
        Dictionary<char,char> DecodeDict = new Dictionary<char, char>();
        for (int i = 0; i < Chars.Length; i++)
        {
            char key;
            char value = Chars[i];

            if (i + 18 >= Chars.Length)
            {
                key = Chars[i + 18 - Chars.Length];
                DecodeDict.Add(key, value);
            }
            else
            {
                key = Chars[i + 18];
                DecodeDict.Add(key, value);
            }
        }
        return DecodeDict;
    }
    
    // encode the given string
    public static string EncodeString(string to_encode)
    {
        AddPairs();
        List<char> Encoded = new();

        foreach(Char c in to_encode)
        {
            if (EncodeDict.ContainsKey(c))
            {
                Encoded.Add(EncodeDict[c]);
            }
            else {return $"ERROR: Invalid entry. Char {c} is not allowed.";}
        }
        return string.Join("", Encoded);
    }

    // decode the given string
    public static string DecodeString(string to_decode)
    {
        AddPairsDecode();
        List<char> Decoded = new();

        foreach(Char c in to_decode)
        {
            if (DecodeDict.ContainsKey(c))
            {
                Decoded.Add(DecodeDict[c]);
            }
            else {return $"ERROR: Invalid entry. Char {c} is not allowed.";}
        }
        return string.Join("", Decoded);
    }
}
