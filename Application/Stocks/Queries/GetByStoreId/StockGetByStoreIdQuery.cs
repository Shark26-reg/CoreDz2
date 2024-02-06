using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;

namespace Application.Stocks.Queries.GetByStoreId;

public sealed record StockGetByStoreIdQuery(Guid StoreId) : IQuery<Result<IEnumerable<StockResultResponse>, Error>>;

