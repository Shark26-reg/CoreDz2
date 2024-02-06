using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Queries.GetByCategoryId;

public record ProductsGetByCategoryIdQuery(Guid? CategoryId) : IQuery<Result<IEnumerable<ProductResultResponse>, Error>>;
