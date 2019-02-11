namespace JsonConvertor.Interfaces
{
    public interface IInputReader : IComponent
    {
        string Read(ConsoleArgs args);
    }
}
