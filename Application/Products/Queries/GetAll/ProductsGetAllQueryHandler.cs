using Application.Abstractions;
using Application.Base;
using Application.Products.Cache;
using Application.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Products.Queries.GetAll;

public class ProductsGetAllQueryHandler : IQueryHandler<ProductsGetAllQuery, Result<IEnumerable<ProductResultResponse>, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsGetAllQueryHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<ProductResultResponse>, Error>> Handle
        (ProductsGetAllQuery request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue("products", out List<ProductResultResponse>? response))
        {
            return response!;
        }

        var products = await _context.Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

        response = products.Select(_mapper.Map<ProductResultResponse>).ToList()!;
        _cache.Set(ProductKeys.All(), response, Constants.Validity);

        return response;
    }
}
