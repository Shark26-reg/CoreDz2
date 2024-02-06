using Application.Categories.Commands.Create;
using Application.Categories.Responses;
using AutoMapper;
using Domain;

namespace Application.Categories.Mappers;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResultResponse>();
        CreateMap<CategoriesCreateCommand, Category>();
    }
}
