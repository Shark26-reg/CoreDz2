using Application.Abstractions;
using Application.Base;
using Application.Categories.Responses;
using Domain.Base;

namespace Application.Categories.Commands.Create;

public sealed record CategoriesCreateCommand(string Name) : ICommand<Result<CategoryResultResponse, Error>>;
