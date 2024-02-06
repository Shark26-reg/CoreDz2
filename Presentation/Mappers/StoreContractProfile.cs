using Application.Stores.Commands.Create;
using Application.Stores.Commands.Delete;
using Application.Stores.Commands.UpdateIdentityNumber;
using Application.Stores.Queries.GetById;
using Application.Stores.Responses;
using AutoMapper;
using Contracts.Stores;

namespace Presentation.Mappers;

public class StoreContractProfile : Profile
{
    public StoreContractProfile()
    {
        CreateMap<StoreResultResponse, StoreResponse>(MemberList.Destination);
        CreateMap<StoreByIdRequest, StoresGetByIdQuery>(MemberList.Destination);
        CreateMap<StoreCreateRequest, StoresCreateCommand>(MemberList.Destination);
        CreateMap<StoreDeleteRequest, StoresDeleteCommand>(MemberList.Destination);
        CreateMap<StoreUpdateIdentityNumberRequest, StoresUpdateIdentityNumberCommand>(MemberList.Destination);
    }
}
