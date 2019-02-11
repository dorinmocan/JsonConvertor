using JsonConvertor.Interfaces;
using System.IO;

namespace JsonConvertor.Components.Readers
{
    public class FileReader : IInputReader
    {
        public bool FitsConversionType(ConsoleArgs args)
        {
            return args.InFile != null;
        }

        public string Read(ConsoleArgs args)
        {
            return File.ReadAllText(args.InFile.FullName);
        }
    }
}