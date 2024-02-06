using Application.Abstractions;
using Application.Base;
using Application.Stores.Cache;
using Application.Stores.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Stores.Queries.GetAll;

public sealed class StoresGetAllQueryHandler : IQueryHandler<StoresGetAllQuery, Result<IEnumerable<StoreResultResponse>, Error>>
{
    private readonly StoreContext _context;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public StoresGetAllQueryHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<StoreResultResponse>, Error>> Handle(StoresGetAllQuery request, CancellationToken cancellationToken)
    {
        if(_cache.TryGetValue(StoreKeys.All(), out List<StoreResultResponse>? response))
        {
            return response!;
        }

        var stores = await _context.Stores.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

        response = stores.Select(_mapper.Map<StoreResultResponse>).ToList()!;
        _cache.Set(StoreKeys.All(), response, Constants.Validity);

        return response;
    }
}
