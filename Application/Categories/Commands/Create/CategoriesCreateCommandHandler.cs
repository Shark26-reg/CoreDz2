using Application.Abstractions;
using Application.Base;
using Application.Categories.Cache;
using Application.Categories.Errors;
using Application.Categories.Responses;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Categories.Commands.Create;

public sealed class CategoriesCreateCommandHandler : ICommandHandler<CategoriesCreateCommand, Result<CategoryResultResponse, Error>>
{

    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CategoriesCreateCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesCreateCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == request.Name, cancellationToken);

        if (category is not null)
        {
            return CategoryErrors.AlreadyExist(request.Name);
        }

        category = _mapper.Map<Category>(request)!;

        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(CategoryKeys.All());
        return _mapper.Map<CategoryResultResponse>(category)!;
    }
}
