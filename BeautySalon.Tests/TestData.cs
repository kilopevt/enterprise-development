using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;

namespace BeautySalon.Tests;

/// <summary>
/// Контролируемые тестовые данные с известным ожидаемым результатом.
/// </summary>
internal static class TestData
{
    // Мастера: 2 со стажем >= 5, 1 со стажем < 5
    public static readonly Master Master10Years = new()
    {
        Id = "001",
        FullName = "Иванов Иван Иванович",
        Gender = Gender.Male,
        PassportNumber = "11 11 111111",
        Specialization = "Парикмахер",
        ExperienceYears = 10
    };

    public static readonly Master Master7Years = new()
    {
        Id = "002",
        FullName = "Петрова Мария Сергеевна",
        Gender = Gender.Female,
        PassportNumber = "22 22 222222",
        Specialization = "Косметолог",
        ExperienceYears = 7
    };

    public static readonly Master Master2Years = new()
    {
        Id = "003",
        FullName = "Сидоров Алексей Петрович",
        Gender = Gender.Male,
        PassportNumber = "33 33 333333",
        Specialization = "Массажист",
        ExperienceYears = 2
    };

    public static readonly List<Master> Masters = [Master10Years, Master7Years, Master2Years];

    // Услуги
    public static readonly Service ServicePopular = new()
    {
        Id = "101",
        Name = "Стрижка",
        Category = "Волосы",
        Cost = 1000,
        Duration = TimeSpan.FromMinutes(60)
    };

    public static readonly Service ServiceSecond = new()
    {
        Id = "102",
        Name = "Маникюр",
        Category = "Ногти",
        Cost = 1500,
        Duration = TimeSpan.FromMinutes(60)
    };

    public static readonly Service ServiceThird = new()
    {
        Id = "103",
        Name = "Массаж",
        Category = "Тело",
        Cost = 2000,
        Duration = TimeSpan.FromMinutes(60)
    };

    public static readonly Service ServiceFourth = new()
    {
        Id = "104",
        Name = "Педикюр",
        Category = "Ногти",
        Cost = 1200,
        Duration = TimeSpan.FromMinutes(60)
    };

    public static readonly Service ServiceFifth = new()
    {
        Id = "105",
        Name = "Чистка лица",
        Category = "Лицо",
        Cost = 2500,
        Duration = TimeSpan.FromMinutes(60)
    };

    public static readonly Service ServiceUnpopular = new()
    {
        Id = "106",
        Name = "Ботокс",
        Category = "Лицо",
        Cost = 5000,
        Duration = TimeSpan.FromMinutes(60)
    };

    public static readonly List<Service> Services =
    [
        ServicePopular, ServiceSecond, ServiceThird,
        ServiceFourth, ServiceFifth, ServiceUnpopular
    ];

    // Клиенты
    public static readonly Client ClientOld = new()
    {
        Id = "201",
        FullName = "Старый Клиент",
        Gender = Gender.Male,
        BirthDate = new DateTime(1980, 1, 1),
        Phone = "+7 (900) 000-00-01"
    };

    public static readonly Client ClientMiddle = new()
    {
        Id = "202",
        FullName = "Средний Клиент",
        Gender = Gender.Female,
        BirthDate = new DateTime(1990, 6, 15),
        Phone = "+7 (900) 000-00-02"
    };

    public static readonly Client ClientYoung = new()
    {
        Id = "203",
        FullName = "Молодой Клиент",
        Gender = Gender.Male,
        BirthDate = new DateTime(2000, 12, 31),
        Phone = "+7 (900) 000-00-03"
    };

    public static readonly List<Client> Clients = [ClientOld, ClientMiddle, ClientYoung];

    // Записи
    public static readonly List<Appointment> Appointments = CreateAppointments();

    private static List<Appointment> CreateAppointments()
    {
        var now = DateTime.Now;
        var list = new List<Appointment>();
        var idCounter = 401;

        // 1. Окошки для Master10Years
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.Date.AddDays(-10).AddHours(10), Master10Years, ClientOld, ServicePopular));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.Date.AddDays(-10).AddHours(12), Master10Years, ClientMiddle, ServiceSecond));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.Date.AddDays(-10).AddHours(14), Master10Years, ClientYoung, ServiceThird));

        // 2. Популярность услуг
        // Стрижка = 5, Маникюр = 4, Массаж = 3, Педикюр = 2, Ботокс = 2, Чистка = 1
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-5), Master7Years, ClientOld, ServicePopular));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-4), Master7Years, ClientOld, ServicePopular));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-3), Master7Years, ClientMiddle, ServicePopular));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-2), Master7Years, ClientYoung, ServicePopular));

        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-5), Master2Years, ClientMiddle, ServiceSecond));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-4), Master2Years, ClientYoung, ServiceSecond));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-3), Master2Years, ClientOld, ServiceSecond));

        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-2), Master2Years, ClientOld, ServiceThird));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-1), Master2Years, ClientMiddle, ServiceThird));

        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-1), Master7Years, ClientYoung, ServiceFourth));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-1), Master2Years, ClientYoung, ServiceFourth));

        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-1), Master7Years, ClientOld, ServiceFifth));

        // 3. Одиночный клиент
        var clientSingle = new Client
        {
            Id = "204",
            FullName = "Одиночный Клиент",
            Gender = Gender.Male,
            BirthDate = new DateTime(1985, 5, 5),
            Phone = "+7 (900) 000-00-04"
        };
        Clients.Add(clientSingle);
        
        // Одна запись за последний месяц и одна старше месяца
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-7), Master7Years, clientSingle, ServiceUnpopular));
        list.Add(MakeAppointment((idCounter++).ToString("D3"), now.AddDays(-45), Master7Years, clientSingle, ServiceUnpopular));

        return list;
    }

    private static Appointment MakeAppointment(string id, DateTime start, Master master, Client client, Service service) => new()
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