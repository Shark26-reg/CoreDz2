namespace Application.Base;

public class NotFound(string message, string description) :
    Error(message, description)
{
    public override ErrorType Type => ErrorType.NotFound;

}