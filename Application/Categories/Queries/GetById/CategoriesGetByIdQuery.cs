using Application.Abstractions;
using Application.Base;
using Application.Categories.Responses;

namespace Application.Categories.Queries.GetById;

public sealed record CategoriesGetByIdQuery(Guid Id) : IQuery<Result<CategoryResultResponse, Error>>;
