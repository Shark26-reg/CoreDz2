using Application.Abstractions;
using Application.Base;
using Application.Stores.Responses;

namespace Application.Stores.Commands.UpdateIdentityNumber;

public sealed record StoresUpdateIdentityNumberCommand(Guid Id, string IdentityNumber) : ICommand<Result<StoreResultResponse, Error>>;
