using System.Text.Json.Serialization;

namespace Contracts.Categories;

public class CategoryResponse
{
    [JsonPropertyName("category_id")]
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

