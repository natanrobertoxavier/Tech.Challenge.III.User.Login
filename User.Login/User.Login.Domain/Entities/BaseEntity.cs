namespace User.Login.Domain.Entities;
public class BaseEntity
{
    public BaseEntity(
        Guid id,
        DateTime registrationDate)
    {
        Id = id;
        RegistrationDate = registrationDate;
    }

    public BaseEntity() { }

    public Guid Id { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}
