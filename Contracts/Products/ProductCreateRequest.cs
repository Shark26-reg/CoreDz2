using System.Text.Json.Serialization;

namespace Contracts.Products;

public class ProductCreateRequest
{
    [JsonPropertyName("name")] public required string Name { get; set; }
    [JsonPropertyName("price")] public decimal Price { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; } = null;
    [JsonPropertyName("category_id")] public Guid? CategoryId { get; set; } = null;
}
