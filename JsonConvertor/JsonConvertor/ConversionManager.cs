using JsonConvertor.Interfaces;

namespace JsonConvertor
{
    public class ConversionManager
    {
        public ConsoleArgs ConsoleArgs { get; set; }
        public IComponentsManager ComponentsManager { get; set; }

        public ConversionManager(
            ConsoleArgs consoleArgs,
            IComponentsManager componentsManager)
        {
            ConsoleArgs = consoleArgs;
            ComponentsManager = componentsManager;
        }

        public void Convert()
        {
            var reader = ComponentsManager.GetReader(ConsoleArgs);
            var input = ReadInput(reader, ConsoleArgs);
            var convertor = ComponentsManager.GetConvertor(ConsoleArgs);
            var output = Convert(convertor, input);
            var writer = ComponentsManager.GetWriter(ConsoleArgs);
            WriteOutput(output, writer, ConsoleArgs);
        }

        public string ReadInput(IInputReader reader, ConsoleArgs args)
        {
            return reader.Read(args);
        }

        public string Convert(IConvertor convertor, string input)
        {
            return convertor.Convert(input);
        }

        public void WriteOutput(string output, IOutputWriter writer, ConsoleArgs args)
        {
            writer.Write(output, args);
        }
    }
}