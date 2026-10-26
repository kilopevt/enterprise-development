namespace BeautySalon.Domain.Entities;

/// <summary>
/// Мастер салона красоты.
/// </summary>
public class Master : Person
{
    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Специализация.
    /// </summary>
    public required string Specialization { get; set; }

    /// <summary>
    /// Стаж работы в годах.
    /// </summary>
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Записи мастера на услуги.
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];
}