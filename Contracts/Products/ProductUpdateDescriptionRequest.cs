using System.Text.Json.Serialization;

namespace Contracts.Products;

public class ProductUpdateDescriptionRequest
{
    [JsonPropertyName("product_id")]
    public Guid Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
