namespace Demo.Application.Common
{
    public delegate void FoundCallback<in TResult>(TResult value);
}