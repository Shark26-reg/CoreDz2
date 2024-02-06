using Application.Stocks.Commands.ExportFromStore;
using Application.Stocks.Commands.ImportToStore;
using Application.Stocks.Commands.MoveBetweenStores;
using Application.Stocks.Queries.GetByProductId;
using Application.Stocks.Queries.GetByStoreId;
using Application.Stocks.Responses;
using AutoMapper;
using Contracts.Stocks;

namespace Presentation.Mappers;

public class StockContractProfile : Profile
{
    public StockContractProfile()
    {
        CreateMap<StockResultResponse, StockResponse>(MemberList.Destination);

        CreateMap<StockByProductIdRequest, StockGetByProductIdQuery>(MemberList.Destination);
        CreateMap<StockByStoreIdRequest, StockGetByStoreIdQuery>(MemberList.Destination);

        CreateMap<StockImportRequest, StockImportToStoreCommand>(MemberList.Destination);
        CreateMap<StockExportRequest, StockExportFromStoreCommand>(MemberList.Destination);
        CreateMap<StockMoveRequest, StockMoveBetweenStoresCommand>(MemberList.Destination);
    }
}