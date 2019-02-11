using JsonConvertor.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace JsonConvertor.Components.Writers
{
    public class JsonWriter : IOutputWriter
    {
        public bool FitsConversionType(ConsoleArgs args)
        {
            return args.InFile?.Extension == ".txt" &&
                args.OutFile?.Extension == ".json";
        }

        public void Write(string output, ConsoleArgs args)
        {
            if (output == null)
            {
                var message = "Unable to write to file. Null output.";
                Console.WriteLine(message);
                throw new ArgumentNullException(message);
            }

            JObject json;

            try
            {
                json = JObject.Parse(output);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            using (var file = File.CreateText(args.OutFile.FullName))
            using (var writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = Settings.IndentChar;
                writer.Indentation = Settings.Indentation;

                json.WriteTo(writer);
            }
        }
    }
}