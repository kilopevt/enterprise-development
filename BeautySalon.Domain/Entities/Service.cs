namespace BeautySalon.Domain.Entities;

/// <summary>
/// Услуга салона красоты.
/// </summary>
public class Service
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Название услуги.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги.
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// Стоимость услуги.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Длительность услуги.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Записи, в которых оказана эта услуга.
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];
}