using Application.Products.Commands.Create;
using Application.Products.Responses;
using AutoMapper;
using Domain;

namespace Application.Products.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResultResponse>(MemberList.Destination);
        CreateMap<ProductsCreateCommand, Product>(MemberList.Destination);
    }
}
