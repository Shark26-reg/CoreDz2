using Application.Abstractions;
using Application.Base;
using Application.Products.Commands.Create;
using Application.Stores.Responses;

namespace Application.Stores.Queries.GetAll;

public sealed record StoresGetAllQuery() : IQuery<Result<IEnumerable<StoreResultResponse>, Error>>;
