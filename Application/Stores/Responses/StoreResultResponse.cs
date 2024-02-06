namespace Application.Stores.Responses;

public sealed class StoreResultResponse
{
    public Guid Id { get; set; }
    public required string IdentityNumber { get; set; }
}

