using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;
using Bogus;

namespace BeautySalon.Domain.Data;

public static class SeedData
{
    public static List<Client> Clients { get; private set; } = new();
    public static List<Master> Masters { get; private set; } = new();
    public static List<Service> Services { get; private set; } = new();
    public static List<Appointment> Appointments { get; private set; } = new();

    public static void Initialize()
    {
        if (Clients.Any()) return;

        // Фиксируем зерно, чтобы данные были одинаковыми при каждом запуске
        Randomizer.Seed = new Random(42);

        // 1. Генерация клиентов
        var clientFaker = new Faker<Client>("ru")
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.BirthDate, f => f.Date.Past(60))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("+7 (###) ###-##-##"));

        Clients = clientFaker.Generate(15); // Количество генраций

        // 2. Генерация мастеров
        var masterFaker = new Faker<Master>("ru")
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.FullName, f => f.Name.FullName())
            .RuleFor(m => m.Gender, f => f.PickRandom<Gender>())
            .RuleFor(m => m.PassportNumber, f => f.Random.Replace("## ## ######"))
            .RuleFor(m => m.Specialization, f => f.PickRandom("Парикмахер", "Мастер маникюра", "Косметолог", "Массажист", "Визажист"))
            .RuleFor(m => m.ExperienceYears, f => f.Random.Int(1, 20));

        Masters = masterFaker.Generate(12);

        // 3. Генерация услуг
        var serviceFaker = new Faker<Service>("ru")
            .RuleFor(s => s.Id, f => f.Random.Guid())
            .RuleFor(s => s.Name, f => f.PickRandom("Стрижка", "Окрашивание", "Маникюр", "Педикюр", "Чистка лица", "Массаж спины", "Укладка", "Ботокс", "Наращивание ресниц", "Коррекция бровей"))
            .RuleFor(s => s.Category, f => f.PickRandom("Волосы", "Ногти", "Лицо", "Тело"))
            .RuleFor(s => s.Cost, f => f.Finance.Amount(500, 5000))
            .RuleFor(s => s.Duration, f => TimeSpan.FromMinutes(f.PickRandom(30, 60, 90, 120)));

        Services = serviceFaker.Generate(10);

        // 4. Генерация записей
        var appointmentFaker = new Faker<Appointment>("ru")
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.StartTime, f => f.Date.Between(DateTime.Now.AddMonths(-2), DateTime.Now.AddMonths(1)))
            .RuleFor(a => a.ClientId, f => f.PickRandom(Clients).Id)
            .RuleFor(a => a.MasterId, f => f.PickRandom(Masters).Id)
            .RuleFor(a => a.ServiceId, f => f.PickRandom(Services).Id)
            .RuleFor(a => a.IsRegularClient, f => f.Random.Bool(0.3f)); // 30% постоянных клиентов

        Appointments = appointmentFaker.Generate(50);

        // 5. Восстановление навигационных свойств (чтобы в тестах можно было обращаться к a.Client.FullName)
        foreach (var appointment in Appointments)
        {
            appointment.Client = Clients.First(c => c.Id == appointment.ClientId);
            appointment.Master = Masters.First(m => m.Id == appointment.MasterId);
            appointment.Service = Services.First(s => s.Id == appointment.ServiceId);
        }
    }
}