using CommandLineParser.Arguments;
using CommandLineParser.Validation;
using System.IO;

namespace JsonConvertor
{
    [ArgumentGroupCertification("i,o", EArgumentGroupCondition.AllOrNoneUsed)]
    public class ConsoleArgs
    {
        [FileArgument('i', "input", Description = "Input file path", Optional = true, FileMustExist = true)]
        public FileInfo InFile { get; set; }

        [FileArgument('o', "output", Description = "Output file path", Optional = true, FileMustExist = false)]
        public FileInfo OutFile { get; set; }
    }
}
