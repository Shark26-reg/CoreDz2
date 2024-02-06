using Domain.Base;

namespace Domain;

public class Category : EntityIdentity
{
    public required string Name { get; set; }
}

