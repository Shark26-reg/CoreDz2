using Application.Products.Commands.Create;
using Application.Products.Commands.Delete;
using Application.Products.Commands.UpdateCategory;
using Application.Products.Commands.UpdateDescription;
using Application.Products.Commands.UpdateName;
using Application.Products.Commands.UpdatePrice;
using Application.Products.Queries.GetByCategoryId;
using Application.Products.Responses;
using AutoMapper;
using Contracts.Products;

namespace Presentation.Mappers;

public class ProductContractProfile : Profile 
{
    public ProductContractProfile()
    {
        CreateMap<ProductResultResponse, ProductResponse>(MemberList.Destination);

        CreateMap<ProductGetByIdRequest, ProductsGetByCategoryIdQuery>(MemberList.Destination);
        CreateMap<ProductGetByCategoryIdRequest, ProductsGetByCategoryIdQuery>(MemberList.Destination);

        CreateMap<ProductCreateRequest, ProductsCreateCommand>(MemberList.Destination);
        CreateMap<ProductUpdateNameRequest, ProductsUpdateNameCommand>(MemberList.Destination);
        CreateMap<ProductUpdatePriceRequest, ProductsUpdatePriceCommand>(MemberList.Destination);
        CreateMap<ProductUpdateDescriptionRequest, ProductsUpdateDescriptionCommand>(MemberList.Destination);
        CreateMap<ProductUpdateCategoryRequest, ProductsUpdateCategoryCommand>(MemberList.Destination);
        CreateMap<ProductDeleteRequest, ProductsDeleteCommand>(MemberList.Destination);
    }
}
