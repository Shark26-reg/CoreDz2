using Application.Abstractions;
using Application.Base;
using Application.Stores.Responses;

namespace Application.Stores.Queries.GetById;

public sealed record StoresGetByIdQuery(Guid Id) : IQuery<Result<StoreResultResponse, Error>>;
