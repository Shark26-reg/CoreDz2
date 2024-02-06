using Application.Abstractions;
using Application.Base;
using Application.Products.Errors;
using Application.Stocks.Commands.ExportFromStore;
using Application.Stocks.Errors;
using Application.Stocks.Responses;
using Application.Stores.Errors;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;

namespace Application.Stocks.Commands.MoveBetweenStores;

public sealed class StockMoveBetweenStoresCommandHandler : ICommandHandler<StockMoveBetweenStoresCommand, Result<IEnumerable<StockResultResponse>, Error>>
{

    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public StockMoveBetweenStoresCommandHandler(StoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<StockResultResponse>, Error>> Handle(StockMoveBetweenStoresCommand request, CancellationToken cancellationToken)
    {

        var product = await _context.Products
            .FirstOrDefaultAsync(p => 
            p.Id == request.ProductId, 
            cancellationToken: cancellationToken);
        if (product == null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        var sender = await _context.Stores
            .FirstOrDefaultAsync(s => 
            s.Id == request.SenderStoreId, 
            cancellationToken: cancellationToken);
        if (sender == null)
        {
            return StoreErrors.NotFound(request.SenderStoreId);
        }

        var target = await _context.Stores
            .FirstOrDefaultAsync(s => 
            s.Id == request.TargetStoreId, 
            cancellationToken: cancellationToken);
        if (target == null)
        {
            return StoreErrors.NotFound(request.TargetStoreId);
        }

        var senderStock = await _context.Stocks
            .FirstOrDefaultAsync(sp => 
            sp.ProductId == request.ProductId && sp.StoreId == request.SenderStoreId, 
            cancellationToken: cancellationToken);
        if (senderStock is null)
        {
            return new NotFound(nameof(StockExportFromStoreCommandHandler), $"Stock for product with id {request.ProductId} not found in sender with id {request.SenderStoreId}");
        }

        if (senderStock.Quantity < request.Quantity)
        {
            return StockErrors.NotEnoughQuantity(request.ProductId, request.SenderStoreId, request.Quantity);
        }

        var targetStock = await _context.Stocks
           .FirstOrDefaultAsync(sp => 
           sp.ProductId == request.ProductId && sp.StoreId == request.TargetStoreId, 
           cancellationToken: cancellationToken);
        if (targetStock is null)
        {
            targetStock = new() { ProductId = request.ProductId, StoreId = request.TargetStoreId, Quantity = 0 };
            await _context.Stocks.AddAsync(targetStock, cancellationToken);
        }

        senderStock.Quantity -= request.Quantity;
        targetStock.Quantity += request.Quantity;

        if (targetStock.Quantity == 0)
        {
            _context.Stocks.Remove(targetStock);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new List<StockResultResponse> { _mapper.Map<StockResultResponse>(senderStock)!, _mapper.Map<StockResultResponse>(targetStock)! };

    }
}
