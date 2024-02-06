using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Commands.UpdateName;

public record ProductsUpdateNameCommand(Guid Id, string Name) : ICommand<Result<ProductResultResponse, Error>>;
