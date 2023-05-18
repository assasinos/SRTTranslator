
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;
using System.Net.NetworkInformation;
using SRTTranslator;


const string TargetLanguage = "pl"; // Set the target language code here (e.g., "pl" for Polish)




Console.WriteLine("Checking connection ...");
if (!Connection("api-free.deepl.com"))
{
    throw new Exception("No connection to translator");
}
Console.WriteLine("connection established");
Console.Clear();



// getting file to translate
Console.WriteLine("Insert path to srt file");

var name = Console.ReadLine();

while (!File.Exists(name))
{
    Console.WriteLine("Path is wrong");
    name =Console.ReadLine();
}


var file = File.Create(Path.GetFileName(name));

string apikey = Environment.GetEnvironmentVariable("DeeplApiKey") ?? throw new InvalidOperationException();


Uri site = new("https://api-free.deepl.com/v2/translate");

var http = new HttpClient { BaseAddress = site };
http.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("DeepL-Auth-Key", apikey);


Console.Clear();
Console.WriteLine($"Translating File: {Path.GetFileName(file.Name)}");
try
{
    await Task.Run(async () =>
    {
        string wholeFile = string.Empty;
        foreach (var line in await File.ReadAllLinesAsync(name))
        {
            wholeFile += (line + "\n");
        }

        var s = wholeFile.Split("\n");
        var t1 = ReadThrough(s.Take((int)Math.Floor((decimal)s.Length / 2)).ToList(), 1);
        var t2 = ReadThrough(s.Skip((int)Math.Ceiling((decimal)s.Length / 2)).ToList(), 2);
        Task.WaitAll(t1, t2);

        for (int i =1; i<3; i++)
        {
            foreach (var line in await File.ReadAllLinesAsync($"temp{i}"))
            {
                file.Write(new UTF8Encoding(true).GetBytes(line+"\n") );
            }
            File.Delete($"temp{i}");
        }


        /*        await readThroughWithOpen();*/
    });

}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    System(file.Name);

    file.Close();
    http.Dispose();
    
    Console.Write("File translated");
    Console.ReadKey();
}


async Task ReadThrough(List<string> text,int l)
{

    var counter = 0;

    var temp = File.Create("temp" +l) ;

    string textToTranslate = string.Empty;

    //go through each line of textfile
    foreach (var line in text)
    {

        counter++;
        Res($"Tłumaczenie lini {counter}", l) ;
        //Console.WriteLine(counter);

        //checks if line is empty
        if (line.Length < 1)
        {
            temp.Write(chars.nl);
            continue;
        }


        //Checks if line is srt index
        //if so translate and write it into the file
        //Then write index
        if (!line.Contains("-->") && Int32.TryParse(line[0].ToString(), out _))
        {

            if (textToTranslate != string.Empty)
            {

                await temp.WriteAsync(new UTF8Encoding(true).GetBytes(await Translate(textToTranslate) + "\n"));

                textToTranslate = string.Empty;
            }


            temp.Write(new UTF8Encoding(true).GetBytes($"{line}\n"));
            continue;
        }

        //checks if line is srt time value
        if (Int32.TryParse(line[0].ToString(), out _) && line.Contains("-->"))
        {

            temp.Write(new UTF8Encoding(true).GetBytes(line));
            continue;

        }

        textToTranslate += line + "\n";


    }

    //Translate and write las line of the file
    if (textToTranslate != string.Empty)
    {

        await temp.WriteAsync(new UTF8Encoding(true).GetBytes("\n" + await Translate(textToTranslate)));

        textToTranslate = string.Empty;
    }
    temp.Close();
}




// Opens explorer on windows, on linux displays path

void System(string fileName)
{
    if (OperatingSystem.IsWindows())
    {
        Process.Start("explorer.exe", "/select," + $"\"{fileName}\"");
        return;
    }
    Console.WriteLine($"File: {fileName}");
}



// check connection to server

 bool Connection(string address)
{
    return new Ping().Send(address).Status == IPStatus.Success;
}



// Send a text to Deepl Translator API and return translated version

async Task<string> Translate(string textToTranslate)
{
    var json = await http.PostAsync($"?text={textToTranslate}&target_lang={TargetLanguage}", null);
    if (json.StatusCode != global::System.Net.HttpStatusCode.OK)
    {
        Console.CursorTop = 4;
        Console.WriteLine($"Line not translated {textToTranslate.Substring(0, 5)}");
        return textToTranslate;
    }
    var response = await json.Content.ReadAsStringAsync();
    var model = JObject.Parse(response);
    return model["translations"].First["text"].ToString() ?? throw new  ReturnedJsonException(response);
}

static void Res(string text,int line)
{

    Console.SetCursorPosition(0, line);
    Console.Write("                                                          ");
    Console.SetCursorPosition(0, line);
    Console.Write(text);

}



