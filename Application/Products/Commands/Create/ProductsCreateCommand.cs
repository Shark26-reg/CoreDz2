using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Commands.Create;

public record ProductsCreateCommand(string Name, string? Description, decimal Price, Guid? CategoryId) : ICommand<Result<ProductResultResponse, Error>>;
