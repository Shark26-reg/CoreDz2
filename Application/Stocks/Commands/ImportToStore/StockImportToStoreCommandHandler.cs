using Application.Abstractions;
using Application.Base;
using Application.Products.Errors;
using Application.Stocks.Errors;
using Application.Stocks.Responses;
using Application.Stores.Errors;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stocks.Commands.ImportToStore;

public sealed class StockImportToStoreCommandHandler : ICommandHandler<StockImportToStoreCommand, Result<StockResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public StockImportToStoreCommandHandler(StoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<StockResultResponse, Error>> Handle(StockImportToStoreCommand request, CancellationToken cancellationToken)
    {

        if (request.Quantity <= 0)
        {
            return StockErrors.QuantityMustBePositive(request.Quantity);
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken: cancellationToken);
        if (product == null)
        {
            return ProductErrors.NotFound(request.ProductId);
        }

        var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == request.StoreId, cancellationToken: cancellationToken);
        if (store == null)
        {
            return StoreErrors.NotFound(request.StoreId);
        }

        var stock = await _context.Stocks.FirstOrDefaultAsync(sp => sp.ProductId == request.ProductId && sp.StoreId == sp.StoreId, cancellationToken: cancellationToken);
        if (stock is null)
        {
            stock = _mapper.Map<Stock>(request)!;
            await _context.Stocks.AddAsync(stock, cancellationToken);
        }

        stock.Quantity += request.Quantity;
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StockResultResponse>(stock)!;

    }
}
