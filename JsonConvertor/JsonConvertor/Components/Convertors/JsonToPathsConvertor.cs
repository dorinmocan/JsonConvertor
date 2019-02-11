using JsonConvertor.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace JsonConvertor.Components.Convertors
{
    public class JsonToPathsConvertor : IConvertor
    {
        public bool FitsConversionType(ConsoleArgs args)
        {
            return args.InFile?.Extension == ".json" &&
                args.OutFile?.Extension == ".txt";
        }

        public string Convert(string input)
        {
            if (input == null)
            {
                var message = "Unable to convert. Null input.";
                Console.WriteLine(message);
                throw new ArgumentNullException(message);
            }

            var paths = string.Empty;
            JObject json;
            
            try
            {
                json = JObject.Parse(input);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            foreach (var property in json.Properties())
            {
                paths += ParseProperty(property.Name, property.Value);
            }

            return Format(paths);
        }

        private static string ParseProperty(string path, JToken value)
        {
            string paths = string.Empty;

            if (!value.HasValues)
            {
                paths += Settings.KeySeparator
                    + path
                    + Settings.ValueSeparator
                    + value
                    + Environment.NewLine;
            }
            else
            {
                foreach (var child in value.Children<JProperty>())
                {
                    paths += ParseProperty(path + Settings.KeySeparator + child.Name, child.Value);
                }
            }

            return paths;
        }

        private static string Format(string paths)
        {
            var lines = paths.Split(Environment.NewLine.ToCharArray());
            paths = string.Empty;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                paths += Settings.KeyDelimiter
                    + line.Split(Settings.ValueSeparator)[0]
                    .TrimStart(Settings.KeySeparator)
                    .Replace(Settings.KeySeparator.ToString(),
                    Settings.KeyDelimiter + Settings.KeySeparator.ToString() + Settings.KeyDelimiter)
                    + Settings.KeyDelimiter
                    + Settings.ValueSeparator
                    + line.Split(Settings.ValueSeparator)[1]
                    + Environment.NewLine;
            }

            return paths;
        }
    }
}