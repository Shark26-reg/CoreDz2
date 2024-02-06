using System.Text.Json.Serialization;

namespace Contracts.Products;

public class ProductResponse
{
    [JsonPropertyName("product_id")] 
    public Guid Id { get; set; }

    [JsonPropertyName("name")] 
    public required string Name { get; set; }

    [JsonPropertyName("price")] 
    public decimal Price { get; set; }

    [JsonPropertyName("description")] 
    public string? Description { get; set; }

    [JsonPropertyName("category_id")] 
    public Guid? CategoryId { get; set; }
}


