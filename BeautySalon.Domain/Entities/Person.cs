using BeautySalon.Domain.Enums;

namespace BeautySalon.Domain.Entities;

public abstract class Person
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
}