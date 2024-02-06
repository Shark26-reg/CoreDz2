using Application.Abstractions;
using Application.Base;
using Application.Categories.Cache;
using Application.Categories.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Categories.Queries.GetAll;

public class CategoriesGetAllQueryHandler : IQueryHandler<CategoriesGetAllQuery, Result<IEnumerable<CategoryResultResponse>, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CategoriesGetAllQueryHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<IEnumerable<CategoryResultResponse>, Error>> Handle(CategoriesGetAllQuery request, CancellationToken cancellationToken)
    {

        if (_cache.TryGetValue(CategoryKeys.All(), out List<CategoryResultResponse>? response))
        {
            return response!;
        }

        var categories = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);

        response = categories.Select(c => _mapper.Map<CategoryResultResponse>(c)!).ToList()!;

        _cache.Set(CategoryKeys.All(), response, Constants.Validity);
        return response;
    }
}
