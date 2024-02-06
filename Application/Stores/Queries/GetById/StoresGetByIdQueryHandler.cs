using Application.Abstractions;
using Application.Base;
using Application.Stores.Cache;
using Application.Stores.Errors;
using Application.Stores.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Stores.Queries.GetById;

public sealed class StoresGetByIdQueryHandler : IQueryHandler<StoresGetByIdQuery, Result<StoreResultResponse, Error>>
{

    private readonly StoreContext _context;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public StoresGetByIdQueryHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Result<StoreResultResponse, Error>> Handle(StoresGetByIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(request.Id, out StoreResultResponse? response))
        {
            return response!;
        }

        var store = await _context.Stores.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (store is null)
        {
            return StoreErrors.NotFound(request.Id);
        }

        response = _mapper.Map<StoreResultResponse>(store)!;
        _cache.Set(StoreKeys.Id(request.Id), response, Constants.Validity);

        return response;
    }
}
