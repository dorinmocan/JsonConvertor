namespace JsonConvertor.Interfaces
{
    public interface IOutputWriter : IComponent
    {
        void Write(string output, ConsoleArgs args);
    }
}
