using Application.Abstractions;
using Application.Base;
using Application.Categories.Errors;
using Application.Products.Cache;
using Application.Products.Errors;
using Application.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;

namespace Application.Products.Commands.UpdateCategory;

public class ProductsUpdateCategoryCommandHandler : ICommandHandler<ProductsUpdateCategoryCommand, Result<ProductResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsUpdateCategoryCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsUpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        if (request.CategoryId is not null)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken: cancellationToken);
            if (category is null)
            {
                return CategoryErrors.NotFound(request.CategoryId.Value);
            }
        }
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(request.Id);  
        }

        product.CategoryId = request.CategoryId;
        await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(ProductKeys.All());
        _cache.Remove(ProductKeys.Id(request.Id));

        return _mapper.Map<ProductResultResponse>(product)!;


    }
}
