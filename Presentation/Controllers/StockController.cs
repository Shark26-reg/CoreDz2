using Application.Stocks.Commands.ExportFromStore;
using Application.Stocks.Commands.ImportToStore;
using Application.Stocks.Commands.MoveBetweenStores;
using Application.Stocks.Queries.GetByProductId;
using Application.Stocks.Queries.GetByStoreId;
using AutoMapper;
using Contracts.Stocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class StockController : BaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public StockController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("by/productId")]
    public async Task<IActionResult> GetStockByProductId([FromQuery] StockByProductIdRequest request)
    {
        var query = _mapper.Map<StockGetByProductIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StockResponse>));
    }

    [HttpGet]
    [Route("by/storeId")]
    public async Task<IActionResult> GetStockByStoreId([FromQuery] StockByStoreIdRequest request)
    {
        var query = _mapper.Map<StockGetByStoreIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StockResponse>));
    }


    [HttpPut]
    [Route("import/product")]
    public async Task<IActionResult> ImportProductToStore(StockImportRequest request)
    {
        var command = _mapper.Map<StockImportToStoreCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StockResponse>(result.Value!));
    }

    [HttpPut]
    [Route("export/product")]
    public async Task<IActionResult> ExportProductFromStore(StockExportRequest request)
    {
        var command = _mapper.Map<StockExportFromStoreCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StockResponse>(result.Value!));
    }

    [HttpPut]
    [Route("relocate/product")]
    public async Task<IActionResult> MoveProductBetweenStores(StockMoveRequest request)
    {
        var command = _mapper.Map<StockMoveBetweenStoresCommand>(request)!;
        var result = await _sender.Send(command)!;

        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StockResponse>));
    }
}

