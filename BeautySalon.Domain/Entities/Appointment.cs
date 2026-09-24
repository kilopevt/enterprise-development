namespace BeautySalon.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public bool IsRegularClient { get; set; } // Индикатор постоянного клиента
    
    // Внешние ключи для связи с другими таблицами
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
    
    public Guid MasterId { get; set; }
    public Master Master { get; set; } = null!;
    
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;
}