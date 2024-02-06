using Application.Abstractions;
using Application.Base;
using Application.Stores.Cache;
using Application.Stores.Errors;
using Application.Stores.Responses;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Stores.Commands.Create;

public sealed class StoresCreateCommandHandler : ICommandHandler<StoresCreateCommand, Result<StoreResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public StoresCreateCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<StoreResultResponse, Error>> Handle(StoresCreateCommand request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(c => c.IdentityNumber == request.IdentityNumber, cancellationToken: cancellationToken);
        if (store is not null)
        {
            return StoreErrors.AlreadyExist(request.IdentityNumber);
        }

        store = _mapper.Map<Store>(request)!;

        await _context.Stores.AddAsync(store, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(StoreKeys.All());

        return _mapper.Map<StoreResultResponse>(store)!;

    }
}


