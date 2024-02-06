using Application.Abstractions;
using Application.Base;
using Application.Categories.Cache;
using Application.Categories.Errors;
using Application.Categories.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Categories.Queries.GetById;

public sealed class CategoriesGetByIdQueryHandler : IQueryHandler<CategoriesGetByIdQuery, Result<CategoryResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CategoriesGetByIdQueryHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesGetByIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(request.Id, out CategoryResultResponse? response))
        {
            return response!;
        }

        var category = await _context.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (category is null)
        {
            return CategoryErrors.NotFound(request.Id);
        }

        response = _mapper.Map<CategoryResultResponse>(category)!;
        _cache.Set(CategoryKeys.Id(response.Id), response, Constants.Validity);

        return response;
    }
}
