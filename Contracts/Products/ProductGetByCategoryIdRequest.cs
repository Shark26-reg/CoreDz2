using System.Text.Json.Serialization;

namespace Contracts.Products;

public class ProductGetByCategoryIdRequest
{
    [JsonPropertyName("category_id")] public Guid? CategoryId { get; set; } = null;
}

