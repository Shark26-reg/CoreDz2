using Application.Stores.Commands.Create;
using Application.Stores.Commands.Delete;
using Application.Stores.Commands.UpdateIdentityNumber;
using Application.Stores.Queries.GetAll;
using Application.Stores.Queries.GetById;
using AutoMapper;
using Contracts.Stores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Presentation.Controllers;

public class StoresController : BaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public StoresController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new StoresGetAllQuery());
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StoreResponse>));
    }

    [HttpGet]
    [Route("by/id/")]
    public async Task<IActionResult> GetById([FromQuery] StoreByIdRequest request)
    {
        var query = _mapper.Map<StoresGetByIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(StoreCreateRequest request)
    {
        var command = _mapper.Map<StoresCreateCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }

    [HttpPut]
    [Route("update/identity")]
    public async Task<IActionResult> UpdateIdentityNumber(StoreUpdateIdentityNumberRequest request)
    {
        var command = _mapper.Map<StoresUpdateIdentityNumberCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(StoreDeleteRequest request)
    {
        var command = _mapper.Map<StoresDeleteCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }
}
