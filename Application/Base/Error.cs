namespace Application.Base;

public abstract class Error
{
   protected Error(string message, string description)
    {
        Message = message;
        Description = description;
    }
    
    public abstract ErrorType Type { get; }
    public string Message { get; }
    public string Description { get;  }
}
