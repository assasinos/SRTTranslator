using System.Text;

namespace SRTTranslator
{
    public class translations
    {
        public  string? detected_source_language { get; set; }
        public  string? text { get; set; }
    }
    public class chars
    {
        public static byte[] nl = new UTF8Encoding(true).GetBytes("\n");
    }
}
