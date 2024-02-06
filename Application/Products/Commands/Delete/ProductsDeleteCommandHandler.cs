using Application.Abstractions;
using Application.Base;
using Application.Products.Cache;
using Application.Products.Errors;
using Application.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;

namespace Application.Products.Commands.Delete;

public class ProductsDeleteCommandHandler : ICommandHandler<ProductsDeleteCommand, Result<ProductResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsDeleteCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsDeleteCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(request.Id);
        }

        if (await _context.Stocks.AnyAsync(x => x.ProductId == request.Id, cancellationToken: cancellationToken))
        {
            return ProductErrors.ProductContainsInStocks(request.Id);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(ProductKeys.All());
        _cache.Remove(ProductKeys.Id(request.Id));

        return _mapper.Map<ProductResultResponse>(product)!;

    }
}
