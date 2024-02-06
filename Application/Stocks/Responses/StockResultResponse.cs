namespace Application.Stocks.Responses;

public class StockResultResponse
{
    public Guid ProductId { get; set; }
    public Guid StoreId { get; set; }
    public int Quantity { get; set; }
}
