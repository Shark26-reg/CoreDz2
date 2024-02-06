namespace Application.Base;

public class Problem(string message, string description) :
    Error(message, description)
{
    public override ErrorType Type => ErrorType.InternalServerError;
}

public class Conflict(string message, string description) :
    Error(message, description)
{
    public override ErrorType Type => ErrorType.Conflict;
}
