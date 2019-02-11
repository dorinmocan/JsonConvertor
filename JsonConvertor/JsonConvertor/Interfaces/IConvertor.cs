namespace JsonConvertor.Interfaces
{
    public interface IConvertor : IComponent
    {
        string Convert(string input);
    }
}
