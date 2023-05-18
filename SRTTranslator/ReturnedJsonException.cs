using System.Text;

namespace SRTTranslator;

/// <summary>
/// Custom exception for easy access to error
/// <param>Returned error JSON
///     <name>json</name>
/// </param>
/// </summary>
public class ReturnedJsonException : Exception
{
    public ReturnedJsonException(string json)
    {
        Console.WriteLine($"There was an error in the returned json (Returned JSON will also be stored in a file): \n{json}");
        var file = new FileStream("ReturnedJson.txt", FileMode.Create);
        file.Write(new UTF8Encoding(true).GetBytes(json));
        file.Close();
    }
}