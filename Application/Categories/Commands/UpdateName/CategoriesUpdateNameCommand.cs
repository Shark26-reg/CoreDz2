using Application.Abstractions;
using Application.Base;
using Application.Categories.Responses;

namespace Application.Categories.Commands.UpdateName;

public sealed record CategoriesUpdateNameCommand(Guid Id, string Name) : ICommand<Result<CategoryResultResponse, Error>>;
