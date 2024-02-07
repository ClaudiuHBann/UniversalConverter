namespace Server.Exceptions
{
public class ValueException : BaseException
{
    public ValueException(string value) : base(value)
    {
    }

    public ValueException(string value, Exception inner) : base(value, inner)
    {
    }
}
}
