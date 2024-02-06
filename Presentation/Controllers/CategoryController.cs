using Application.Categories.Commands.Create;
using Application.Categories.Commands.Delete;
using Application.Categories.Commands.UpdateName;
using Application.Categories.Queries.GetAll;
using Application.Categories.Queries.GetById;
using AutoMapper;
using Contracts.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class CategoryController : BaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CategoryController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("all")] 
    public async Task<IActionResult> GetAll()
    {
        var query = new CategoriesGetAllQuery();
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<CategoryResponse>).ToList());
    }

    [HttpGet]
    [Route("by/id/")]
    public async Task<IActionResult> GetById([FromQuery] CategoryGetByIdRequest request)
    {
        var query = _mapper.Map<CategoriesGetByIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<CategoryResponse>(result.Value!));
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        var command = _mapper.Map<CategoriesCreateCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<CategoryResponse>(result.Value!));
    }

    [HttpPut]
    [Route("update/name/")]
    public async Task<IActionResult> UpdateName(CategoryUpdateNameRequest request)
    {
       var command = _mapper.Map<CategoriesUpdateNameCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<CategoryResponse>(result.Value!));
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(CategoryDeleteRequest request)
    {
        var command = _mapper.Map<CategoriesDeleteCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return NoContent();
    }
}
