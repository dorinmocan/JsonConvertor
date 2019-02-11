namespace JsonConvertor.Interfaces
{
    public interface IComponentsManager
    {
        IInputReader GetReader(ConsoleArgs args);
        IConvertor GetConvertor(ConsoleArgs args);
        IOutputWriter GetWriter(ConsoleArgs args);
    }
}
