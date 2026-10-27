using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Server.Services;

/// <summary>
/// Реализация хранилища в памяти на основе данных из SeedData.
/// </summary>
public class InMemorySalonDataStore : ISalonDataStore
{
    public List<Client> Clients { get; }
    public List<Master> Masters { get; }
    public List<Service> Services { get; }
    public List<Appointment> Appointments { get; }

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