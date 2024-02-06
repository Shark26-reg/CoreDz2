using System.Text.Json.Serialization;

namespace Contracts.Stocks;

public class StockResponse
{
    [JsonPropertyName("product_id")] public Guid ProductId { get; set; }
    [JsonPropertyName("store_id")] public Guid StoreId { get; set; }
    [JsonPropertyName("quantity_int_stock")] public int Quantity { get; set; }
}


