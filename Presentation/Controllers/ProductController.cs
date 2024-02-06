using Application.Products.Commands.Create;
using Application.Products.Commands.Delete;
using Application.Products.Commands.UpdateCategory;
using Application.Products.Commands.UpdateDescription;
using Application.Products.Commands.UpdateName;
using Application.Products.Commands.UpdatePrice;
using Application.Products.Queries.GetAll;
using Application.Products.Queries.GetByCategoryId;
using Application.Products.Queries.GetById;
using AutoMapper;
using Contracts.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Presentation.Controllers;

public class ProductController : BaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductController(ISender sender, IMapper mapper, IMemoryCache cache)
    {
        _sender = sender;
        _mapper = mapper;
        _cache = cache;
    }

    [HttpGet]
    [Route("cache_statistic")]
    public FileContentResult GetStatistic()
    {
        const string textType = "text/plain";
        const string filename = "statistic.txt";

        var builder = new StringBuilder();
        var statistic = _cache.GetCurrentStatistics() ?? new MemoryCacheStatistics();

        builder.AppendLine("Statistic");
        builder.AppendLine($"{statistic.TotalHits} hits");
        builder.AppendLine($"{statistic.TotalMisses} misses");
        builder.AppendLine($"{statistic.CurrentEntryCount} total");
        builder.AppendLine($"{statistic.CurrentEstimatedSize} size");

        var content = Encoding.UTF8.GetBytes(builder.ToString());
        return File(content, textType, filename);
    }

    [HttpGet]
    [Route("products_catalog")]
    public async Task<FileContentResult> GetProducts()
    {
        const string textType = "text/csv";
        const string filename = "products.csv";

        var result = await _sender.Send(new ProductsGetAllQuery());
        if (result.IsFailure)
        {
            return File(Encoding.UTF8.GetBytes(string.Empty), textType, filename);
        }

        var products = _mapper.Map<IEnumerable<ProductResponse>>(result.Value);

        var builder = new StringBuilder();
        builder.AppendLine($"Id,Name,Price,Description,CategoryId");
        foreach (var product in products!)
        {
            builder.Append(product.Id.ToString() + ",");
            builder.Append(product.Name.ToString() + ",");
            builder.Append(product.Price.ToString() + ",");
            builder.Append(product.Description?.ToString() ?? string.Empty + ",");
            builder.AppendLine(product.CategoryId.ToString());
        }
        var content = Encoding.UTF8.GetBytes(builder.ToString());
        return File(content, textType, filename);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new ProductsGetAllQuery();
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var products = result.Value!.Select(_mapper.Map<ProductResponse>).ToList()!;
        return Ok(products);
    }

    [HttpGet]
    [Route("by/id/")]
    public async Task<IActionResult> GetById([FromQuery] ProductGetByIdRequest request)
    {
        var query = _mapper.Map<ProductsGetByIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);
        return Ok(product);
    }

    [HttpGet]
    [Route("all/by/category/")]
    public async Task<IActionResult> GetAllByCategoryId([FromQuery] ProductGetByCategoryIdRequest request)
    {
        var query = _mapper.Map<ProductsGetByCategoryIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var products = result.Value!.Select(_mapper.Map<ProductResponse>).ToList()!;
        return Ok(products);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var command = _mapper.Map<ProductsCreateCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update/name")]
    public async Task<IActionResult> UpdateName(ProductUpdateNameRequest request)
    {
        var command = _mapper.Map<ProductsUpdateNameCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update/description")]
    public async Task<IActionResult> UpdateDescription(ProductUpdateDescriptionRequest request)
    {
        var command = _mapper.Map<ProductsUpdateDescriptionCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update/price")]
    public async Task<IActionResult> UpdatePrice(ProductUpdatePriceRequest request)
    {
        var command = _mapper.Map<ProductsUpdatePriceCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update/category")]
    public async Task<IActionResult> UpdateCategory(ProductUpdateCategoryRequest request)
    {
        var command = _mapper.Map<ProductsUpdateCategoryCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);


        return Ok(product);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(ProductDeleteRequest request)
    {
        var command = _mapper.Map<ProductsDeleteCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }


}
