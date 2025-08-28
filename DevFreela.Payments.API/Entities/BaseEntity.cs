namespace DevFreela.Payments.API.Entities;

public abstract class BaseEntity
{
    public BaseEntity()
    {
        CreatedAt = DateTime.Now;
    }

    public long Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted => DeletedAt.HasValue;

    public void SetAsDeleted() => DeletedAt = DateTime.Now;
    public void SetId(long id) => Id = id;
}