using Application.Abstractions;
using Application.Base;
using Application.Categories.Cache;
using Application.Categories.Errors;
using Application.Categories.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Persistence.Contexts;

namespace Application.Categories.Commands.UpdateName;

public sealed class CategoriesUpdateNameCommandHandler : ICommandHandler<CategoriesUpdateNameCommand, Result<CategoryResultResponse, Error>>
{

    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public CategoriesUpdateNameCommandHandler(StoreContext context, IMapper mapper, IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesUpdateNameCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == request.Name, cancellationToken);
        if (category is not null)
        {
            return CategoryErrors.AlreadyExist(request.Name);
        }

        category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (category is null)
        {
            return CategoryErrors.NotFound(request.Id);
        }

        category.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);

        _cache.Remove(CategoryKeys.All());
        return _mapper.Map<CategoryResultResponse>(category)!;
    }
}