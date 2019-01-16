namespace Demo.Zoo.Domain
{
    using ValueOf;

    public class Error : ValueOf<(string name, string message), Error>
    {
    }
}