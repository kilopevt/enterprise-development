namespace BeautySalon.Domain.Entities;

public class Client : Person
{
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    
    // у одного клиента может быть много записей
    public List<Appointment> Appointments { get; set; } = new();
}