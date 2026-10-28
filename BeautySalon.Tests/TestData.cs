using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;

namespace BeautySalon.Tests;

/// <summary>
/// Контролируемые тестовые данные с известным ожидаемым результатом.
/// </summary>
internal static class TestData
{
    // ID-константы для ссылки на конкретные объекты из тестов
    public const int Master10YearsId = 1;
    public const int Master7YearsId = 2;
    public const int Master2YearsId = 3;

    public const int ServicePopularId = 101;
    public const int ServiceSecondId = 102;
    public const int ServiceThirdId = 103;
    public const int ServiceFourthId = 104;
    public const int ServiceFifthId = 105;
    public const int ServiceUnpopularId = 106;

    public const int ClientOldId = 201;
    public const int ClientMiddleId = 202;
    public const int ClientYoungId = 203;
    public const int ClientSingleId = 204;

    // Мастера: 2 со стажем >= 5, 1 со стажем < 5
    public static readonly List<Master> Masters =
    [
        new() { 
            Id = Master10YearsId, 
            FullName = "Иванов Иван Иванович", 
            Gender = Gender.Male, 
            PassportNumber = "11 11 111111", 
            Specialization = "Парикмахер", 
            ExperienceYears = 10 
        },
        new() { 
            Id = Master7YearsId, 
            FullName = "Петрова Мария Сергеевна", 
            Gender = Gender.Female, 
            PassportNumber = "22 22 222222", 
            Specialization = "Косметолог", 
            ExperienceYears = 7 
        },
        new() { 
            Id = Master2YearsId, 
            FullName = "Сидоров Алексей Петрович", 
            Gender = Gender.Male, 
            PassportNumber = "33 33 333333", 
            Specialization = "Массажист",
            ExperienceYears = 2
        }
    ];

    // Услуги
    public static readonly List<Service> Services =
    [
        new() { 
            Id = ServicePopularId, 
            Name = "Стрижка", 
            Category = "Волосы", 
            Cost = 1000, 
            Duration = TimeSpan.FromMinutes(60) 
        },
        new() { 
            Id = ServiceSecondId, 
            Name = "Маникюр", 
            Category = "Ногти", 
            Cost = 1500, 
            Duration = TimeSpan.FromMinutes(60) },
        new() { 
            Id = ServiceThirdId, 
            Name = "Массаж", 
            Category = "Тело", 
            Cost = 2000, 
            Duration = TimeSpan.FromMinutes(60) 
        },
        new() { 
            Id = ServiceFourthId, 
            Name = "Педикюр", 
            Category = "Ногти", 
            Cost = 1200, 
            Duration = TimeSpan.FromMinutes(60) },
        new() { 
            Id = ServiceFifthId, 
            Name = "Чистка лица", 
            Category = "Лицо", 
            Cost = 2500, 
            Duration = TimeSpan.FromMinutes(60) 
        },
        new() { 
            Id = ServiceUnpopularId, 
            Name = "Ботокс", 
            Category = "Лицо", 
            Cost = 5000, 
            Duration = TimeSpan.FromMinutes(60) 
        }
    ];

    // Клиенты: 3 основных + 1 одиночный
    public static readonly List<Client> Clients =
    [
        new() { 
            Id = ClientOldId, 
            FullName = "Старый Клиент", 
            Gender = Gender.Male, 
            BirthDate = new DateTime(1980, 1, 1), 
            Phone = "+7 (900) 000-00-01" 
        },
        new() { 
            Id = ClientMiddleId, 
            FullName = "Средний Клиент", 
            Gender = Gender.Female, 
            BirthDate = new DateTime(1990, 6, 15), 
            Phone = "+7 (900) 000-00-02" 
        },
        new() { 
            Id = ClientYoungId, 
            FullName = "Молодой Клиент", 
            Gender = Gender.Male, 
            BirthDate = new DateTime(2000, 12, 31), 
            Phone = "+7 (900) 000-00-03" 
        },
        new() { 
            Id = ClientSingleId, 
            FullName = "Одиночный Клиент", 
            Gender = Gender.Male, 
            BirthDate = new DateTime(1985, 5, 5), 
            Phone = "+7 (900) 000-00-04" 
        }
    ];

    // Записи
    public static readonly List<Appointment> Appointments = CreateAppointments();

    private static List<Appointment> CreateAppointments()
    {
        // Локальные переменные только для читаемости — ссылаемся на объекты из списков
        var master10 = Masters.First(m => m.Id == Master10YearsId);
        var master7 = Masters.First(m => m.Id == Master7YearsId);
        var master2 = Masters.First(m => m.Id == Master2YearsId);

        var servicePopular = Services.First(s => s.Id == ServicePopularId);
        var serviceSecond = Services.First(s => s.Id == ServiceSecondId);
        var serviceThird = Services.First(s => s.Id == ServiceThirdId);
        var serviceFourth = Services.First(s => s.Id == ServiceFourthId);
        var serviceFifth = Services.First(s => s.Id == ServiceFifthId);
        var serviceUnpopular = Services.First(s => s.Id == ServiceUnpopularId);

        var clientOld = Clients.First(c => c.Id == ClientOldId);
        var clientMiddle = Clients.First(c => c.Id == ClientMiddleId);
        var clientYoung = Clients.First(c => c.Id == ClientYoungId);
        var clientSingle = Clients.First(c => c.Id == ClientSingleId);

        var now = DateTime.Now;
        var id = 401;
        var list = new List<Appointment>
        {
            // 1. Окошки для master10 (ровно 3 записи, 2 окошка по часу)
            Make(id++, now.Date.AddDays(-10).AddHours(10), master10, clientOld, servicePopular),
            Make(id++, now.Date.AddDays(-10).AddHours(12), master10, clientMiddle, serviceSecond),
            Make(id++, now.Date.AddDays(-10).AddHours(14), master10, clientYoung, serviceThird),

            // 2. Популярность услуг: Стрижка=5, Маникюр=4, Массаж=3, Педикюр=2, Ботокс=2, Чистка=1 
            Make(id++, now.AddDays(-5), master7, clientOld, servicePopular),
            Make(id++, now.AddDays(-4), master7, clientOld, servicePopular),
            Make(id++, now.AddDays(-3), master7, clientMiddle, servicePopular),
            Make(id++, now.AddDays(-2), master7, clientYoung, servicePopular),
            Make(id++, now.AddDays(-5), master2, clientMiddle, serviceSecond),
            Make(id++, now.AddDays(-4), master2, clientYoung, serviceSecond),
            Make(id++, now.AddDays(-3), master2, clientOld, serviceSecond),
            Make(id++, now.AddDays(-2), master2, clientOld, serviceThird),
            Make(id++, now.AddDays(-1), master2, clientMiddle, serviceThird),
            Make(id++, now.AddDays(-1), master7, clientYoung, serviceFourth),
            Make(id++, now.AddDays(-1), master2, clientYoung, serviceFourth),
            Make(id++, now.AddDays(-1), master7, clientOld, serviceFifth),

            // 3. Одиночный клиент: 1 запись в последний месяц, 1 старше 30 дней
            Make(id++, now.AddDays(-7), master7, clientSingle, serviceUnpopular),
            Make(id++, now.AddDays(-45), master7, clientSingle, serviceUnpopular)
        };

        return list;
    }

    private static Appointment Make(int id, DateTime start, Master master, Client client, Service service) => new()
    {
        Id = id,
        StartTime = start,
        IsRegularClient = false,
        ClientId = client.Id,
        Client = client,
        MasterId = master.Id,
        Master = master,
        ServiceId = service.Id,
        Service = service
    };
}