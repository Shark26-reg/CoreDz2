using System.Text.Json.Serialization;

namespace Contracts.Stores;

public class StoreUpdateIdentityNumberRequest
{
    [JsonPropertyName("store_id")] public Guid Id { get; set; }
    [JsonPropertyName("identity_number")] public required string IdentityNumber { get; set; }
}
