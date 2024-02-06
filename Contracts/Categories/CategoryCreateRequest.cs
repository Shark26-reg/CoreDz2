using System.Text.Json.Serialization;

namespace Contracts.Categories;

public class CategoryCreateRequest
{
    [JsonPropertyName("name")] 
    public required string Name { get; set; }
}
