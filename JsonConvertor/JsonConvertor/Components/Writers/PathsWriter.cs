using JsonConvertor.Interfaces;
using System;
using System.IO;

namespace JsonConvertor.Components.Writers
{
    public class PathsWriter : IOutputWriter
    {
        public bool FitsConversionType(ConsoleArgs args)
        {
            return args.InFile?.Extension == ".json" &&
                args.OutFile?.Extension == ".txt";
        }

        public void Write(string output, ConsoleArgs args)
        {
            if (output == null)
            {
                var message = "Unable to write to file. Null output.";
                Console.WriteLine(message);
                throw new ArgumentNullException(message);
            }

            File.WriteAllText(args.OutFile.FullName, output);
        }
    }
}