using System.Text.Json.Serialization;

namespace Contracts.Stocks;

public class StockMoveRequest
{
    [JsonPropertyName("product_id")] public Guid ProductId { get; set; }
    [JsonPropertyName("sender_store_id")] public Guid FromStoreId { get; set; }
    [JsonPropertyName("target_store_id")] public Guid ToStoreId { get; set; }
    [JsonPropertyName("quantity_for_export")] public int Quantity { get; set; }
}