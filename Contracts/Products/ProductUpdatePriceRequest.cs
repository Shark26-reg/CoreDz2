using System.Text.Json.Serialization;

namespace Contracts.Products;

public class ProductUpdatePriceRequest
{
    [JsonPropertyName("product_id")]
    public Guid Id { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}

