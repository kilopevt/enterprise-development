using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Server.Services;

/// <summary>
/// Реализация хранилища в памяти на основе данных из SeedData.
/// </summary>
public class InMemorySalonDataStore : ISalonDataStore
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
    /// Инициализирует хранилище, связывая его с коллекциями из SeedData
    /// </summary>
    public InMemorySalonDataStore()
    {
        // Инициализируем SeedData
        SeedData.Initialize();
        
        // Связываем хранилище с коллекциями из SeedData
        Clients = SeedData.Clients;
        Masters = SeedData.Masters;
        Services = SeedData.Services;
        Appointments = SeedData.Appointments;
    }

    public void SaveChanges()
    {
        // При хранении в памяти сохранение не требуется, 
        // изменения в списках применяются сразу.
    }
}