namespace Application.Base;

public enum ErrorType
{
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    Conflict = 409,
    InternalServerError = 500
}
