using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Commands.UpdatePrice;

public record ProductsUpdatePriceCommand(Guid Id, decimal Price) :ICommand<Result<ProductResultResponse, Error>>;

