using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Commands.Delete;

public record ProductsDeleteCommand(Guid Id) : ICommand<Result<ProductResultResponse, Error>>;
