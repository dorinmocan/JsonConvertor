using JsonConvertor.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JsonConvertor.Components.Convertors
{
    public class PathsToJsonConvertor : IConvertor
    {
        public Regex Regex { get; } = new Regex("^(\"[^\"\t]*\"\\.)*(\"[^\"\t]*\")\t[^\t]+\\z");

        public bool FitsConversionType(ConsoleArgs args)
        {
            return args.InFile?.Extension == ".txt" &&
                args.OutFile?.Extension == ".json";
        }

        public string Convert(string input)
        {
            if (input == null)
            {
                var message = "Unable to convert. Null input.";
                Console.WriteLine(message);
                throw new ArgumentNullException(message);
            }

            var paths = input
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToArray();

            foreach (var path in paths)
            {
                if (!Regex.IsMatch(path))
                {
                    var message = "Unable to convert. Format not supported.";
                    Console.WriteLine(message);
                    throw new FormatException(message);
                }
            }

            return ParsePaths(paths).ToString(Formatting.None);
        }

        private static JObject ParsePaths(string[] paths)
        {
            var keyValuePairs = new Dictionary<int, Tuple<string, string, JObject>>();
            var nestedValues = new Dictionary<int, string>();

            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i].Split(Settings.ValueSeparator)[0].Contains(Settings.KeySeparator))
                {
                    nestedValues.Add(i, paths[i]);
                }
                else
                {
                    keyValuePairs.Add(i, Tuple.Create(
                        paths[i].Split(Settings.ValueSeparator)[0].Replace(Settings.KeyDelimiter.ToString(), string.Empty),
                        paths[i].Split(Settings.ValueSeparator)[1],
                        null as JObject));
                }
            }

            var groupsOfPaths = nestedValues
                .GroupBy(p => p.Value.Substring(0, p.Value.IndexOf(Settings.KeySeparator)));

            foreach (var group in groupsOfPaths)
            {
                keyValuePairs.Add(group.First().Key, Tuple.Create(
                    group.Key.Replace(Settings.KeyDelimiter.ToString(), string.Empty),
                    null as string,
                    ParsePaths(group.Select(p => p.Value.Substring(p.Value.IndexOf(Settings.KeySeparator) + 1)).ToArray())));
            }

            var json = new JObject();

            foreach (var pair in keyValuePairs.OrderBy(p => p.Key))
            {
                if (pair.Value.Item2 != null)
                {
                    json.Add(pair.Value.Item1, pair.Value.Item2);
                }
                else
                {
                    json.Add(pair.Value.Item1, pair.Value.Item3);
                }
            }

            return json;
        }
    }
}