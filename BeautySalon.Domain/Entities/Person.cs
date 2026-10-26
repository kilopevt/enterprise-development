using BeautySalon.Domain.Enums;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Базовый класс для всех людей в системе.
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// ФИО.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public Gender Gender { get; set; }
}