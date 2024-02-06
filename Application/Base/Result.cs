using Microsoft.IdentityModel.Tokens;

namespace Application.Base;

public class Result<T, E>
{
    public T? Value { get; set; }
    public IEnumerable<E> Errors { get; set; } = [];

    public bool IsSuccess => Errors.IsNullOrEmpty();
    public bool IsFailure => !IsSuccess;

    public static Result<T, E> Success(T value) => new() { Value = value };
    public static Result<T, E> Failure(E error) => new() { Errors = [error] };
    public static Result<T, E> Failure(IEnumerable<E> errors) => new() { Errors = errors };

    public static implicit operator Result<T, E>(T value) => Success(value);
    public static implicit operator Result<T, E>(E error) => Failure(error);
    public static implicit operator Result<T, E>(List<E> errors) => Failure(errors);
}
