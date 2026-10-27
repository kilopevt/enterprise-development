using BeautySalon.Domain.Entities;

namespace BeautySalon.Server.Services;

/// <summary>
/// Интерфейс хранилища данных салона.
/// </summary>
public interface ISalonDataStore
{
    public List<Client> Clients { get; }
    public List<Master> Masters { get; }
    public List<Service> Services { get; }
    public List<Appointment> Appointments { get; }

    public void SaveChanges(); 
}