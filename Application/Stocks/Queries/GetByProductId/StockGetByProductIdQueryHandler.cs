using Application.Abstractions;
using Application.Base;
using Application.Products.Errors;
using Application.Stocks.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stocks.Queries.GetByProductId;

public sealed class StockGetByProductIdQueryHandler : IQueryHandler<StockGetByProductIdQuery, Result<IEnumerable<StockResultResponse>, Error>>
{

    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public StockGetByProductIdQueryHandler(StoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<StockResultResponse>, Error>> Handle(StockGetByProductIdQuery request, CancellationToken cancellationToken)
    {

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken: cancellationToken);
        if (product == null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        var stocks = await _context.Stocks.Where(s => s.ProductId == request.ProductId).ToListAsync(cancellationToken: cancellationToken);

        return stocks.Select(s => _mapper.Map<StockResultResponse>(s)!).ToList();
    }
}
