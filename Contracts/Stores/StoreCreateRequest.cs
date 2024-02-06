using System.Text.Json.Serialization;

namespace Contracts.Stores;

public class StoreCreateRequest
{
    [JsonPropertyName("identity_number")] public required string IdentityNumber { get; set; }
}
