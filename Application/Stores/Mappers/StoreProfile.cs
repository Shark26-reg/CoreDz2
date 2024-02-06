using Application.Stores.Commands.Create;
using Application.Stores.Responses;
using AutoMapper;
using Domain;

namespace Application.Stores.Mappers;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreResultResponse>(MemberList.Destination);
        CreateMap<StoresCreateCommand, Store>(MemberList.Destination);

    }
}
