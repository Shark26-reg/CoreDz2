using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Commands.UpdateDescription;

public record ProductsUpdateDescriptionCommand(Guid Id, string Description) : ICommand<Result<ProductResultResponse, Error>>;
