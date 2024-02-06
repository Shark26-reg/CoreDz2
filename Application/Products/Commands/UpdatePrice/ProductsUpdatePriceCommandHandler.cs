using Application.Abstractions;
using Application.Base;
using Application.Products.Cache;
using Application.Products.Errors;
using Application.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Products.Commands.UpdatePrice;

public class ProductsUpdatePriceCommandHandler : ICommandHandler<ProductsUpdatePriceCommand, Result<ProductResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsUpdatePriceCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsUpdatePriceCommand request, CancellationToken cancellationToken)
    {

        if (request.Price < 0)
        {
            return ProductErrors.PriceCanNotBeLessZero(request.Price);
        }

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(request.Id);
        }

        product.Price = request.Price;
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(ProductKeys.All());
        _cache.Remove(ProductKeys.Id(request.Id));

        return _mapper.Map<ProductResultResponse>(product)!;
    }
}

