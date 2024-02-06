using System.Text.Json.Serialization;

namespace Contracts.Products;

public class ProductUpdateCategoryRequest
{
    [JsonPropertyName("product_id")]
    public Guid Id { get; set; }

    [JsonPropertyName("category_id")] 
    public Guid? CategoryId { get; set; } = null;
}

