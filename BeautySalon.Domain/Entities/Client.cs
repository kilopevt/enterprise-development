namespace BeautySalon.Domain.Entities;

/// <summary>
/// Клиент салона красоты.
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    public required string Phone { get; set; }
    
    /// <summary>
    /// Записи клиента на услуги.
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];
}