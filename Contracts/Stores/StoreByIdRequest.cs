using System.Text.Json.Serialization;

namespace Contracts.Stores;

public class StoreByIdRequest
{
    [JsonPropertyName("store_id")] public Guid Id { get; set; }
}
