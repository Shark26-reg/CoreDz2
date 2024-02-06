using Application.Categories.Commands.Create;
using Application.Categories.Commands.Delete;
using Application.Categories.Commands.UpdateName;
using Application.Categories.Queries.GetById;
using Application.Categories.Responses;
using AutoMapper;
using Contracts.Categories;

namespace Presentation.Mappers;

public class CategoryContractProfile : Profile
{
    public CategoryContractProfile()
    {
        CreateMap<CategoryResultResponse, CategoryResponse>(MemberList.Destination);

        CreateMap<CategoryGetByIdRequest, CategoriesGetByIdQuery>(MemberList.Destination);

        CreateMap<CategoryCreateRequest, CategoriesCreateCommand>(MemberList.Destination);
        CreateMap<CategoryUpdateNameRequest, CategoriesUpdateNameCommand>(MemberList.Destination);
        CreateMap<CategoryDeleteRequest, CategoriesDeleteCommand>(MemberList.Destination);
    }
}