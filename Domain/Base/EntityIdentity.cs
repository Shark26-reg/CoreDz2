namespace Domain.Base;

public abstract class EntityIdentity : Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

