using Application.Abstractions;
using Application.Base;
using Application.Products.Cache;
using Application.Products.Errors;
using Application.Products.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Products.Queries.GetById;

public class ProductsGetByIdQueryHandler : IQueryHandler<ProductsGetByIdQuery, Result<ProductResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsGetByIdQueryHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public IMemoryCache Cache => _cache;

    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsGetByIdQuery query, CancellationToken cancellationToken)
    {
        if(_cache.TryGetValue(ProductKeys.Id(query.Id), out ProductResultResponse? response))
        {
            return response!;
        }

        var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => 
                x.Id == query.Id, 
                cancellationToken: cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(query.Id);
        }

        response = _mapper.Map<ProductResultResponse>(response)!;
        _cache.Set(ProductKeys.Id(query.Id), response);

        return response;

    }
}
