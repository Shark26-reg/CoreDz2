using Application.Abstractions;
using Application.Base;
using Application.Products.Errors;
using Application.Stocks.Errors;
using Application.Stocks.Responses;
using Application.Stores.Errors;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stocks.Commands.ExportFromStore;

public sealed class StockExportFromStoreCommandHandler : ICommandHandler<StockExportFromStoreCommand, Result<StockResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public StockExportFromStoreCommandHandler(StoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<StockResultResponse, Error>> Handle(StockExportFromStoreCommand request, CancellationToken cancellationToken)
    {
        
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken: cancellationToken);
            if (product == null)
            {
            return ProductErrors.NotFound(request.ProductId);
            }

            var store = await _context.Stores.FirstOrDefaultAsync(s => 
                s.Id == request.StoreId, 
                cancellationToken: cancellationToken);
            if (store == null)
            {
                return StoreErrors.NotFound(request.StoreId);
            }

            var stock = await _context.Stocks
                .FirstOrDefaultAsync(sp => 
                sp.ProductId == request.ProductId && 
                sp.StoreId == request.StoreId, 
                cancellationToken: cancellationToken);
            if (stock is null)
            {               
                return StockErrors.NotFound(request.ProductId, request.StoreId);
            }

            if (stock.Quantity < request.Quantity)
            {
                return StockErrors.NotEnoughQuantity(request.ProductId, request.StoreId, stock.Quantity);
            }

            stock.Quantity -= request.Quantity;

            if (stock.Quantity == 0)
            {
                _context.Stocks.Remove(stock);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<StockResultResponse>(stock)!;
    }
}
