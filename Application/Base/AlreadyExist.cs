namespace Application.Base;

public class AlreadyExist(string message, string description) :
    Error(message, description)
{
    public override ErrorType Type => ErrorType.Conflict;
}
