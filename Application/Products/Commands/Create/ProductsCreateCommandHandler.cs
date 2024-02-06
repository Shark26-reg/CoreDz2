using Application.Abstractions;
using Application.Base;
using Application.Categories.Errors;
using Application.Products.Cache;
using Application.Products.Errors;
using Application.Products.Responses;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Products.Commands.Create;

public class ProductsCreateCommandHandler : ICommandHandler<ProductsCreateCommand, Result<ProductResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsCreateCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsCreateCommand request, CancellationToken cancellationToken)
    {

        if (request.CategoryId is not null)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken: cancellationToken);
            if (category is null)
            {
                return CategoryErrors.NotFound(request.CategoryId.Value);
            }
        }
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
        if (product is not null)
        {
            return ProductErrors.AlreadyExist(request.Name);
        }

        product = _mapper.Map<Product>(request)!;
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(ProductKeys.All());

        return _mapper.Map<ProductResultResponse>(product)!;

    }
}
