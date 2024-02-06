using System.Text.Json.Serialization;

namespace Contracts.Stocks;

public class StockByStoreIdRequest
{
    [JsonPropertyName("store_id")]
    public Guid StoreId { get; set; }
}
