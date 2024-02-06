using System.Text.Json.Serialization;

namespace Contracts.Categories;

public class CategoryDeleteRequest
{
    [JsonPropertyName("category_id")]
    public Guid Id { get; set; }
}
