using Application.Abstractions;
using Application.Base;
using Application.Stores.Cache;
using Application.Stores.Errors;
using Application.Stores.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Stores.Commands.Delete;

public sealed class StoresDeleteCommandHandler : ICommandHandler<StoresDeleteCommand, Result<StoreResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public StoresDeleteCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }


    public async Task<Result<StoreResultResponse, Error>> Handle(StoresDeleteCommand request, CancellationToken cancellationToken)
    {

        var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (store is null)
        {
            return StoreErrors.NotFound(request.Id);
        }

        if(await _context.Stocks.AnyAsync(s=>s.StoreId == request.Id, cancellationToken: cancellationToken))
        {
            return StoreErrors.NotEmpty(store.IdentityNumber);
        }

        _context.Stores.Remove(store);
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(StoreKeys.Id(request.Id));
        _cache.Remove(StoreKeys.All());

        return _mapper.Map<StoreResultResponse>(store)!;

    }
}

