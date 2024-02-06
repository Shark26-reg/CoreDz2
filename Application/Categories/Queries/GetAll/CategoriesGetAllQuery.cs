using Application.Abstractions;
using Application.Base;
using Application.Categories.Responses;
using Application.Products.Queries.GetById;

namespace Application.Categories.Queries.GetAll;

public record CategoriesGetAllQuery() : IQuery<Result<IEnumerable<CategoryResultResponse>, Error>>;
