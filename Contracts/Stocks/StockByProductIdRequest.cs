using System.Text.Json.Serialization;

namespace Contracts.Stocks;

public record StockByProductIdRequest
{
    [JsonPropertyName("product_id")] 
    public Guid ProductId { get; set; }
}
