using Application.Abstractions;
using Application.Base;
using Application.Stores.Queries.GetAll;
using Application.Stores.Responses;

namespace Application.Stores.Commands.Create;

public sealed record StoresCreateCommand(string IdentityNumber) : ICommand<Result<StoreResultResponse, Error>>;


