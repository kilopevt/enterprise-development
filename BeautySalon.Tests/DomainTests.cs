using BeautySalon.Domain.Entities;
using Xunit;

namespace BeautySalon.Tests;

/// <summary>
/// Юнит-тесты аналитических запросов.
/// </summary>
public class DomainTests
{
    // 1. Мастера со стажем не менее 5 лет
    [Fact]
    public void ShouldReturnOnlyMastersWithAtLeast5YearsExperience()
    {
        // Act
        var result = TestData.Masters
            .Where(m => m.ExperienceYears >= 5)
            .ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(new[] { TestData.Master10Years.Id, TestData.Master7Years.Id }, result.Select(m => m.Id).ToArray());
        Assert.DoesNotContain(result, m => m.Id == TestData.Master2Years.Id);
    }

    // 2. Окошки выбранного мастера
    [Fact]
    public void ShouldReturnPositiveTimeGapsBetweenMasterAppointments()
    {
        // Arrange
        var master = TestData.Master10Years;
        var appointments = TestData.Appointments
            .Where(a => a.MasterId == master.Id)
            .OrderBy(a => a.StartTime)
            .ToList();

        // Act
        var gaps = new List<TimeSpan>();
        for (var i = 0; i < appointments.Count - 1; i++)
        {
            var current = appointments[i];
            var next = appointments[i + 1];
            var currentEnd = current.StartTime + current.Service.Duration;

            if (next.StartTime > currentEnd)
            {
                gaps.Add(next.StartTime - currentEnd);
            }
        }

        // Assert
        Assert.Equal(2, gaps.Count);
        Assert.Equal(TimeSpan.FromHours(1), gaps[0]);
        Assert.Equal(TimeSpan.FromHours(1), gaps[1]);
    }

    // 3. Топ 5 популярных услуг
    [Fact]
    public void ShouldReturnTop5ServicesOrderedByAppointmentCount()
    {
        // Act
        var top5 = TestData.Appointments
            .GroupBy(a => a.ServiceId)
            .Select(g => new
            {
                Service = TestData.Services.First(s => s.Id == g.Key),
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        // Assert
        Assert.Equal(5, top5.Count);

        // Топ: Стрижка(5), Маникюр(4), Массаж(3).
        Assert.Equal(TestData.ServicePopular.Id, top5[0].Service.Id);
        Assert.Equal(TestData.ServiceSecond.Id,  top5[1].Service.Id);
        Assert.Equal(TestData.ServiceThird.Id,   top5[2].Service.Id);
        
        // Педикюр(2) и Ботокс(2) делят 4-5 места, порядок может быть разным.
        var top4And5 = top5.Skip(3).Select(x => x.Service.Id).ToList();
        Assert.Contains(TestData.ServiceFourth.Id, top4And5);
        Assert.Contains(TestData.ServiceUnpopular.Id, top4And5);

        Assert.Equal(5, top5[0].Count);
        Assert.Equal(4, top5[1].Count);
        Assert.Equal(3, top5[2].Count);
        Assert.Equal(2, top5[3].Count);
        Assert.Equal(2, top5[4].Count);

        // Услуга с 1 записью не должна попасть в топ
        Assert.DoesNotContain(top5, x => x.Service.Id == TestData.ServiceFifth.Id);
    }

    // 4. Повторные записи клиентов за последний месяц
    [Fact]
    public void ShouldReturnRepeatAppointmentCountsForLastMonth()
    {
        // Arrange
        var lastMonth = DateTime.Now.AddDays(-30);

        // Act
        var repeatClients = TestData.Appointments
            .Where(a => a.StartTime >= lastMonth)
            .GroupBy(a => a.ClientId)
            .Where(g => g.Count() > 1)
            .Select(g => new { ClientId = g.Key, Count = g.Count() })
            .ToList();

        // Assert
        Assert.Equal(3, repeatClients.Count);
        Assert.DoesNotContain(repeatClients, rc => rc.ClientId == TestData.Clients[3].Id);
    }

    // 5. Клиенты у нескольких мастеров, сортировка по дате рождения
    [Fact]
    public void ShouldReturnClientsWithMultipleMastersOrderedByBirthDate()
    {
        // Act
        var clients = TestData.Appointments
            .GroupBy(a => a.ClientId)
            .Where(g => g.Select(a => a.MasterId).Distinct().Count() > 1)
            .Select(g => TestData.Clients.First(c => c.Id == g.Key))
            .OrderBy(c => c.BirthDate)
            .ToList();

        // Assert
        Assert.Equal(3, clients.Count);
        Assert.Equal(TestData.ClientOld.Id,    clients[0].Id);
        Assert.Equal(TestData.ClientMiddle.Id, clients[1].Id);
        Assert.Equal(TestData.ClientYoung.Id,  clients[2].Id);
    }
}