using System.Text.Json.Serialization;

namespace Contracts.Categories;

public record CategoryGetByIdRequest
{
    [JsonPropertyName("category_id")]
    public Guid Id { get; set; }
}


