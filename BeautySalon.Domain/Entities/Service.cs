namespace BeautySalon.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Cost { get; set; } 
    public TimeSpan Duration { get; set; } 
    
    // одна услуга может быть оказана много раз
    public List<Appointment> Appointments { get; set; } = new();
}