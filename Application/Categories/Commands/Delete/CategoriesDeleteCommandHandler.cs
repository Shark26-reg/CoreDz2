using Application.Abstractions;
using Application.Base;
using Application.Categories.Cache;
using Application.Categories.Errors;
using Application.Categories.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Categories.Commands.Delete;

public sealed class CategoriesDeleteCommandHandler : ICommandHandler<CategoriesDeleteCommand, Result<CategoryResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CategoriesDeleteCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesDeleteCommand request, CancellationToken cancellationToken)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);
        if (category is null)
        {
            return CategoryErrors.NotFound(request.Id);
        }

        if(await _context.Products.AnyAsync(p => p.CategoryId == request.Id, cancellationToken: cancellationToken))
        {
            return CategoryErrors.HasProducts(category.Name);
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(CategoryKeys.Id(request.Id));
        _cache.Remove(CategoryKeys.All());

        return _mapper.Map<CategoryResultResponse>(category)!;
    }
}

