namespace BeautySalon.Domain.Entities;

public class Master : Person
{
    public string PassportNumber { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    
    // у одного мастера может быть много записей
    public List<Appointment> Appointments { get; set; } = new();
}