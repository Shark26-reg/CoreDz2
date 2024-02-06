using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;

namespace Application.Stocks.Commands.ExportFromStore;

public sealed record StockExportFromStoreCommand(Guid ProductId, Guid StoreId, int Quantity) : ICommand<Result<StockResultResponse, Error>>;
