using Application.Stocks.Commands.ImportToStore;
using Application.Stocks.Responses;
using AutoMapper;
using Domain;

namespace Application.Stocks.Mappers;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockResultResponse>(MemberList.Destination);
        CreateMap<StockImportToStoreCommand, Stock>(MemberList.Destination);

    }
}
