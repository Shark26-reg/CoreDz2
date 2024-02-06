using Application.Abstractions;
using Application.Base;
using Application.Products.Responses;
namespace Application.Products.Commands.UpdateCategory;

public record ProductsUpdateCategoryCommand(Guid Id, Guid? CategoryId) : ICommand<Result<ProductResultResponse, Error>>;
