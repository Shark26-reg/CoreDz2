using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;
using Application.Stores.Commands.Create;
using Domain;

namespace Application.Stocks.Commands.MoveBetweenStores;

public sealed record StockMoveBetweenStoresCommand(Guid ProductId, Guid SenderStoreId, Guid TargetStoreId, int Quantity)
        :ICommand<Result<IEnumerable<StockResultResponse>, Error>>;
