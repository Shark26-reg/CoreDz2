using System.Text.Json.Serialization;

namespace Contracts.Stores;

public sealed class StoreResponse
{
    [JsonPropertyName("store_id")] public Guid Id { get; set; }
    [JsonPropertyName("identity_number")] public required string IdentityNumber { get; set; }
}