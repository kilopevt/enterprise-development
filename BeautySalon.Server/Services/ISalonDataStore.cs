using BeautySalon.Domain.Entities;

namespace BeautySalon.Server.Services;

/// <summary>
/// Интерфейс хранилища данных салона.
/// </summary>
public interface ISalonDataStore
{
    /// <summary>
    /// Коллекция клиентов салона.
    /// </summary>
    public List<Client> Clients { get; }
    /// <summary>
    /// Коллекция мастеров салона.
    /// </summary>
    public List<Master> Masters { get; }
    /// <summary>
    /// Справочник услуг.
    /// </summary>
    public List<Service> Services { get; }
    /// <summary>
    /// Записи клиентов на услуги.
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// Сохраняет изменения в источнике данных.
    /// </summary>
    public void SaveChanges(); 
}