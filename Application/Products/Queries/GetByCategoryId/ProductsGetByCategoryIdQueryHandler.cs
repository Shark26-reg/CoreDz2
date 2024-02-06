using Application.Abstractions;
using Application.Base;
using Application.Categories.Errors;
using Application.Products.Responses;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Contexts;

namespace Application.Products.Queries.GetByCategoryId;

public class ProductsGetByCategoryIdQueryHandler : IQueryHandler<ProductsGetByCategoryIdQuery, Result<IEnumerable<ProductResultResponse>, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;

    public ProductsGetByCategoryIdQueryHandler(StoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProductResultResponse>, Error>> Handle(ProductsGetByCategoryIdQuery query, CancellationToken cancellationToken)
    {

        List<Product> products;
        if (query.CategoryId is not null)
        {
            var category = await _context
                .Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == query.CategoryId, cancellationToken: cancellationToken);

            if (category is null)
            {
                return CategoryErrors.NotFound(query.CategoryId.Value);
            }
        }
        products = await _context.Products
            .Where(x => x.CategoryId == query.CategoryId)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        return products.Select(_mapper.Map<ProductResultResponse>).ToList()!;
    }
}
