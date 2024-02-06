using System.Text.Json.Serialization;

namespace Contracts.Products;

public class ProductDeleteRequest
{
    [JsonPropertyName("product_id")] 
    public Guid Id { get; set; }
}

