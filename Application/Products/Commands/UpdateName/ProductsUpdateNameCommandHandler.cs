using Application.Abstractions;
using Application.Base;
using Application.Products.Cache;
using Application.Products.Errors;
using Application.Products.Responses;
using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;

namespace Application.Products.Commands.UpdateName;

public class ProductsUpdateNameCommandHandler : ICommandHandler<ProductsUpdateNameCommand, Result<ProductResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductsUpdateNameCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }


    public async Task<Result<ProductResultResponse, Error>> Handle(ProductsUpdateNameCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
        if (product is not null)
        {
            return ProductErrors.AlreadyExist(request.Name);
        }

        product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(request.Id);
        }

        product.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(ProductKeys.All());
        _cache.Remove(ProductKeys.Id(request.Id));

        return _mapper.Map<ProductResultResponse>(product)!;
    }
}
