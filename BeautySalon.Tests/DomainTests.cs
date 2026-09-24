using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using Xunit;

namespace BeautySalon.Tests;

public class DomainTests
{
    public DomainTests()
    {
        SeedData.Initialize();
    }

    // НАчало тестов
    
// Стаж мастера> 5 лет
[Fact]
public void GetMastersWithExperienceAtLeast5Years_ReturnsCorrectMasters()
{
    // Act
    var result = SeedData.Masters
        .Where(m => m.ExperienceYears >= 5)
        .ToList();

    // Assert
    Assert.NotEmpty(result);
    Assert.All(result, m => Assert.True(m.ExperienceYears >= 5));
}

// "Окошко" в записях
[Fact]
public void GetMasterTimeGaps_ReturnsCorrectGaps()
{
    // Arrange
    var master = SeedData.Masters
        .First(m => SeedData.Appointments.Count(a => a.MasterId == m.Id) >= 2);

    var appointments = SeedData.Appointments
        .Where(a => a.MasterId == master.Id)
        .OrderBy(a => a.StartTime)
        .ToList();

    // Act
    var gaps = new List<TimeSpan>();
    for (int i = 0; i < appointments.Count - 1; i++)
    {
        var currentAppointment = appointments[i];
        var nextAppointment = appointments[i + 1];

        // Конец текущей записи = начало + длительность услуги
        var currentEndTime = currentAppointment.StartTime + currentAppointment.Service.Duration;
        
        // Если следующая запись начинается позже, чем закончилась текущая — это "окошко"
        if (nextAppointment.StartTime > currentEndTime)
        {
            gaps.Add(nextAppointment.StartTime - currentEndTime);
        }
    }

    // Assert
    Assert.All(gaps, gap => Assert.True(gap.TotalMinutes > 0));
}

// Топ 5 услуг
[Fact]
public void GetTop5PopularServices_ReturnsOrderedList()
{
    // Act
    var topServices = SeedData.Appointments
        .GroupBy(a => a.ServiceId)
        .Select(g => new 
        { 
            Service = SeedData.Services.First(s => s.Id == g.Key), 
            Count = g.Count() 
        })
        .OrderByDescending(x => x.Count)
        .Take(5)
        .ToList();

    // Assert
    Assert.True(topServices.Count <= 5);
    // Список отсортирован по убыванию популярности
    for (int i = 0; i < topServices.Count - 1; i++)
    {
        Assert.True(topServices[i].Count >= topServices[i + 1].Count);
    }
}

// Количество повторных записей клиентов за месяц
[Fact]
public void GetRepeatAppointmentsLastMonth_ReturnsCorrectData()
{
    // Arrange
    var lastMonth = DateTime.Now.AddMonths(-1);

    // Act
    var repeatClients = SeedData.Appointments
        .Where(a => a.StartTime >= lastMonth)
        .GroupBy(a => a.ClientId)
        .Where(g => g.Count() > 1)
        .Select(g => new 
        { 
            ClientId = g.Key, 
            RepeatCount = g.Count() 
        })
        .ToList();

    // Assert
    Assert.All(repeatClients, rc => Assert.True(rc.RepeatCount > 1));
}

// Клиенты у разных мастеров
[Fact]
public void GetClientsWithMultipleMasters_OrderedByBirthDate()
{
    // Act
    var clients = SeedData.Appointments
        .GroupBy(a => a.ClientId)
        .Where(g => g.Select(a => a.MasterId).Distinct().Count() > 1) // Клиент ходил к >1 мастеру
        .Select(g => SeedData.Clients.First(c => c.Id == g.Key))
        .OrderBy(c => c.BirthDate)
        .ToList();

    // Assert
    // Сортировка по дате рождения 
    for (int i = 0; i < clients.Count - 1; i++)
    {
        Assert.True(clients[i].BirthDate <= clients[i + 1].BirthDate);
    }
}
}