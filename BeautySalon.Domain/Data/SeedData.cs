using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;
using Bogus;

namespace BeautySalon.Domain.Data;

/// <summary>
/// Генератор тестовых данных.
/// Данные детерминированы.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Клиенты салона.
    /// </summary>
    public static List<Client> Clients { get; private set; } = [];

    /// <summary>
    /// Мастера салона.
    /// </summary>
    public static List<Master> Masters { get; private set; } = [];

    /// <summary>
    /// Справочник услуг.
    /// </summary>
    public static List<Service> Services { get; private set; } = [];

    /// <summary>
    /// Записи клиентов на услуги.
    /// </summary>
    public static List<Appointment> Appointments { get; private set; } = [];

    /// <summary>
    /// Инициализирует датасид. Повторный вызов не перегенерирует данные.
    /// </summary>
    public static void Initialize()
    {
        if (Clients.Count > 0) return;

        Randomizer.Seed = new Random(42);

        // Клиенты
        var clientFaker = new Faker<Client>("ru")
            .RuleFor(c => c.Id, (_, _) => string.Empty)
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.BirthDate, f => f.Date.Past(60))
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("+7 (###) ###-##-##"));

        Clients = clientFaker.Generate(15);
        for (var i = 0; i < Clients.Count; i++)
        {
            Clients[i].Id = (i + 1).ToString("D3");
        }

        // Мастера
        var masterFaker = new Faker<Master>("ru")
            .RuleFor(m => m.Id, (_, _) => string.Empty)
            .RuleFor(m => m.FullName, f => f.Name.FullName())
            .RuleFor(m => m.Gender, f => f.PickRandom<Gender>())
            .RuleFor(m => m.PassportNumber, f => f.Random.Replace("## ## ######"))
            .RuleFor(m => m.Specialization, f => f.PickRandom("Парикмахер", "Мастер маникюра", "Косметолог", "Массажист", "Визажист"))
            .RuleFor(m => m.ExperienceYears, f => f.Random.Int(1, 20));

        Masters = masterFaker.Generate(12);
        for (var i = 0; i < Masters.Count; i++)
        {
            Masters[i].Id = (101 + i).ToString("D3");
        }

        // Услуги
        var serviceFaker = new Faker<Service>("ru")
            .RuleFor(s => s.Id, (_, _) => string.Empty)
            .RuleFor(s => s.Name, f => f.PickRandom("Стрижка", "Окрашивание", "Маникюр", "Педикюр", "Чистка лица", "Массаж спины", "Укладка", "Ботокс", "Наращивание ресниц", "Коррекция бровей"))
            .RuleFor(s => s.Category, f => f.PickRandom("Волосы", "Ногти", "Лицо", "Тело"))
            .RuleFor(s => s.Cost, f => f.Finance.Amount(500, 5000))
            .RuleFor(s => s.Duration, f => TimeSpan.FromMinutes(f.PickRandom(30, 60, 90, 120)));

        Services = serviceFaker.Generate(10);
        for (var i = 0; i < Services.Count; i++)
        {
            Services[i].Id = (201 + i).ToString("D3");
        }

        // Записи
        var random = new Random(24);
        Appointments = new List<Appointment>(50);

        for (var i = 0; i < 50; i++)
        {
            var client = Clients[random.Next(Clients.Count)];
            var master = Masters[random.Next(Masters.Count)];
            var service = Services[random.Next(Services.Count)];

            Appointments.Add(new Appointment
            {
                Id = (301 + i).ToString("D3"),
                StartTime = DateTime.Now.AddDays(random.Next(-60, 30)).AddHours(random.Next(9, 19)),
                IsRegularClient = random.NextDouble() < 0.3,
                ClientId = client.Id,
                Client = client,
                MasterId = master.Id,
                Master = master,
                ServiceId = service.Id,
                Service = service
            });
        }
    }
}