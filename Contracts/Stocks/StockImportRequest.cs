using System.Text.Json.Serialization;

namespace Contracts.Stocks;

public class StockImportRequest
{
    [JsonPropertyName("product_id")] public Guid ProductId { get; set; }
    [JsonPropertyName("store_id")] public Guid StoreId { get; set; }
    [JsonPropertyName("quantity_for_import")] public int Quantity { get; set; }
}

