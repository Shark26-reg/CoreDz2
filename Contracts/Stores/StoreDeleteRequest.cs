using System.Text.Json.Serialization;

namespace Contracts.Stores;

public class StoreDeleteRequest
{
    [JsonPropertyName("store_id")] public Guid Id { get; set; }
}
