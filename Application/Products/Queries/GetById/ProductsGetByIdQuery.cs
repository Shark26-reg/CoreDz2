using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;

namespace Application.Products.Queries.GetById;

public record ProductsGetByIdQuery(Guid Id) : IQuery<Result<ProductResultResponse, Error>>;
