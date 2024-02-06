using Application.Abstractions;
using Application.Base;
using Application.Stocks.Responses;
using Application.Stores.Errors;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stocks.Queries.GetByStoreId;

public sealed class StockGetByStoreIdQueryHandler : IQueryHandler<StockGetByStoreIdQuery, Result<IEnumerable<StockResultResponse>, Error>>
{

    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public StockGetByStoreIdQueryHandler(StoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<StockResultResponse>, Error>> Handle(StockGetByStoreIdQuery request, CancellationToken cancellationToken)
    {

        var product = await _context.Stores.FirstOrDefaultAsync(p => p.Id == request.StoreId, cancellationToken: cancellationToken);
        if (product == null)
        {
            return StoreErrors.NotFound(request.StoreId);
        }

        var stocks = await _context.Stocks.Where(s => s.StoreId == request.StoreId).ToListAsync(cancellationToken: cancellationToken);

        return stocks.Select(s => _mapper.Map<StockResultResponse>(s)!).ToList();

    }
}

