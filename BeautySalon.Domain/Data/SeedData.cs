using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;

namespace BeautySalon.Domain.Data;

/// <summary>
/// Список тестовых данных.
/// </summary>
public static class SeedData
{
    public static List<Client> Clients { get; private set; } = [];
    public static List<Master> Masters { get; private set; } = [];
    public static List<Service> Services { get; private set; } = [];
    public static List<Appointment> Appointments { get; private set; } = [];

    public static void Initialize()
    {
        if (Clients.Count > 0) return;

        // Клиенты
        Clients = 
        [
            new() { Id = 1, FullName = "Иванов Иван Иванович", Gender = Gender.Male, BirthDate = new DateTime(1980, 1, 1), Phone = "+7 (900) 000-00-01" },
            new() { Id = 2, FullName = "Петрова Мария Сергеевна", Gender = Gender.Female, BirthDate = new DateTime(1990, 6, 15), Phone = "+7 (900) 000-00-02" },
            new() { Id = 3, FullName = "Сидоров Алексей Петрович", Gender = Gender.Male, BirthDate = new DateTime(2000, 12, 31), Phone = "+7 (900) 000-00-03" },
            new() { Id = 4, FullName = "Кузнецова Ольга Дмитриевна", Gender = Gender.Female, BirthDate = new DateTime(1985, 5, 5), Phone = "+7 (900) 000-00-04" },
            new() { Id = 5, FullName = "Смирнов Дмитрий Андреевич", Gender = Gender.Male, BirthDate = new DateTime(1995, 8, 20), Phone = "+7 (900) 000-00-05" },
            new() { Id = 6, FullName = "Волкова Анна Игоревна", Gender = Gender.Female, BirthDate = new DateTime(1988, 3, 12), Phone = "+7 (900) 000-00-06" },
            new() { Id = 7, FullName = "Морозов Николай Васильевич", Gender = Gender.Male, BirthDate = new DateTime(1975, 11, 30), Phone = "+7 (900) 000-00-07" },
            new() { Id = 8, FullName = "Павлова Елена Олеговна", Gender = Gender.Female, BirthDate = new DateTime(1992, 7, 7), Phone = "+7 (900) 000-00-08" },
            new() { Id = 9, FullName = "Соколов Артем Максимович", Gender = Gender.Male, BirthDate = new DateTime(1998, 2, 14), Phone = "+7 (900) 000-00-09" },
            new() { Id = 10, FullName = "Лебедева Виктория Сергеевна", Gender = Gender.Female, BirthDate = new DateTime(1983, 9, 25), Phone = "+7 (900) 000-00-10" },
            new() { Id = 11, FullName = "Козлов Михаил Юрьевич", Gender = Gender.Male, BirthDate = new DateTime(1970, 4, 10), Phone = "+7 (900) 000-00-11" },
            new() { Id = 12, FullName = "Новикова Дарья Александровна", Gender = Gender.Female, BirthDate = new DateTime(1999, 12, 5), Phone = "+7 (900) 000-00-12" },
            new() { Id = 13, FullName = "Егоров Сергей Владимирович", Gender = Gender.Male, BirthDate = new DateTime(1987, 8, 18), Phone = "+7 (900) 000-00-13" },
            new() { Id = 14, FullName = "Орлова Ксения Павловна", Gender = Gender.Female, BirthDate = new DateTime(1994, 1, 22), Phone = "+7 (900) 000-00-14" },
            new() { Id = 15, FullName = "Федоров Андрей Игоревич", Gender = Gender.Male, BirthDate = new DateTime(1981, 6, 30), Phone = "+7 (900) 000-00-15" }
        ];

        // Мастеры
        Masters = 
        [
            new() { Id = 101, FullName = "Петров Петр Петрович", Gender = Gender.Male, PassportNumber = "11 11 111111", Specialization = "Парикмахер", ExperienceYears = 10 },
            new() { Id = 102, FullName = "Смирнова Ольга Ивановна", Gender = Gender.Female, PassportNumber = "22 22 222222", Specialization = "Косметолог", ExperienceYears = 7 },
            new() { Id = 103, FullName = "Сидоров Сидор Сидорович", Gender = Gender.Male, PassportNumber = "33 33 333333", Specialization = "Массажист", ExperienceYears = 2 },
            new() { Id = 104, FullName = "Кузнецова Мария Ивановна", Gender = Gender.Female, PassportNumber = "44 44 444444", Specialization = "Мастер маникюра", ExperienceYears = 12 },
            new() { Id = 105, FullName = "Попов Алексей Николаевич", Gender = Gender.Male, PassportNumber = "55 55 555555", Specialization = "Визажист", ExperienceYears = 6 },
            new() { Id = 106, FullName = "Васильева Елена Дмитриевна", Gender = Gender.Female, PassportNumber = "66 66 666666", Specialization = "Парикмахер", ExperienceYears = 15 },
            new() { Id = 107, FullName = "Михайлов Игорь Олегович", Gender = Gender.Male, PassportNumber = "77 77 777777", Specialization = "Массажист", ExperienceYears = 4 },
            new() { Id = 108, FullName = "Федорова Анна Сергеевна", Gender = Gender.Female, PassportNumber = "88 88 888888", Specialization = "Косметолог", ExperienceYears = 8 },
            new() { Id = 109, FullName = "Николаев Дмитрий Андреевич", Gender = Gender.Male, PassportNumber = "99 99 999999", Specialization = "Мастер маникюра", ExperienceYears = 1 },
            new() { Id = 110, FullName = "Захарова Ирина Викторовна", Gender = Gender.Female, PassportNumber = "10 10 101010", Specialization = "Визажист", ExperienceYears = 9 },
            new() { Id = 111, FullName = "Морозов Павел Валерьевич", Gender = Gender.Male, PassportNumber = "12 12 121212", Specialization = "Парикмахер", ExperienceYears = 5 },
            new() { Id = 112, FullName = "Волкова Оксана Юрьевна", Gender = Gender.Female, PassportNumber = "13 13 131313", Specialization = "Косметолог", ExperienceYears = 3 }
        ];

        // Услуги
        Services = 
        [
            new() { Id = 201, Name = "Стрижка", Category = "Волосы", Cost = 1500, Duration = TimeSpan.FromMinutes(60) },
            new() { Id = 202, Name = "Окрашивание", Category = "Волосы", Cost = 3500, Duration = TimeSpan.FromMinutes(120) },
            new() { Id = 203, Name = "Маникюр", Category = "Ногти", Cost = 1000, Duration = TimeSpan.FromMinutes(60) },
            new() { Id = 204, Name = "Педикюр", Category = "Ногти", Cost = 1500, Duration = TimeSpan.FromMinutes(90) },
            new() { Id = 205, Name = "Чистка лица", Category = "Лицо", Cost = 2500, Duration = TimeSpan.FromMinutes(90) },
            new() { Id = 206, Name = "Массаж спины", Category = "Тело", Cost = 2000, Duration = TimeSpan.FromMinutes(60) },
            new() { Id = 207, Name = "Укладка", Category = "Волосы", Cost = 800, Duration = TimeSpan.FromMinutes(30) },
            new() { Id = 208, Name = "Ботокс", Category = "Лицо", Cost = 5000, Duration = TimeSpan.FromMinutes(120) },
            new() { Id = 209, Name = "Наращивание ресниц", Category = "Лицо", Cost = 3000, Duration = TimeSpan.FromMinutes(90) },
            new() { Id = 210, Name = "Коррекция бровей", Category = "Лицо", Cost = 700, Duration = TimeSpan.FromMinutes(30) }
        ];

        // Записи
        var random = new Random(42);
        Appointments = new List<Appointment>(50);

        for (var i = 0; i < 50; i++)
        {
            var client = Clients[random.Next(Clients.Count)];
            var master = Masters[random.Next(Masters.Count)];
            var service = Services[random.Next(Services.Count)];

            Appointments.Add(new Appointment
            {
                Id = 301 + i,
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