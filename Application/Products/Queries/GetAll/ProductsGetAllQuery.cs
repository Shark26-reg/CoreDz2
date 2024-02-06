using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Queries.GetAll;

public record ProductsGetAllQuery() : IQuery<Result<IEnumerable<ProductResultResponse>, Error>>;
