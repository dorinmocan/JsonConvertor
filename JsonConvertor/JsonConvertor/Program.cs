using CommandLineParser.Exceptions;
using System;

namespace JsonConvertor
{
    public class Program
    {
        public static string SupportedFormats { get; } =
            "\t" + "Json -> paths \tExample: { key1: { key2: value } } -> " +
            Settings.KeyDelimiter + "key1" + Settings.KeyDelimiter + Settings.KeySeparator +
            Settings.KeyDelimiter + "key2" + Settings.KeyDelimiter + Settings.ValueSeparator +
            "value" + Environment.NewLine +
            "\t" + "Paths -> json \tExample: " +
            Settings.KeyDelimiter + "key1" + Settings.KeyDelimiter + Settings.KeySeparator +
            Settings.KeyDelimiter + "key2" + Settings.KeyDelimiter + Settings.ValueSeparator +
            "value -> { key1: { key2: value } }" + Environment.NewLine;

        public static void Main(string[] args)
        {
            var consoleArgs = new ConsoleArgs();
            var consoleArgsParser = new CommandLineParser.CommandLineParser();

            try
            {
                consoleArgsParser.ShowUsageHeader = "Find below information on usage and supported formats:";
                consoleArgsParser.ShowUsageFooter = "Supported formats:" + Environment.NewLine + SupportedFormats;
                consoleArgsParser.ShowUsageOnEmptyCommandline = true;
                consoleArgsParser.ExtractArgumentAttributes(consoleArgs);
                consoleArgsParser.ParseCommandLine(args);

                if (!consoleArgsParser.ParsingSucceeded)
                {
                    return;
                };
            }
            catch (CommandLineException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            try
            {
                var conversionManager = new ConversionManager(consoleArgs, new ComponentsManager());
                conversionManager.Convert();
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong :(");
                Console.WriteLine("Please use /? for more information on usage and supported formats.");
                return;
            }
        }
    }
}
