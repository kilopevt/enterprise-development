namespace BeautySalon.Tests;

/// <summary>
/// Юнит-тесты аналитических запросов.
/// </summary>
public class DomainTests
{
    /// <summary>
    /// 1. Мастера со стажем не менее 5 лет
    /// </summary>
    [Fact]
    public void ShouldReturnOnlyMastersWithAtLeast5YearsExperience()
    {
        // Act
        var result = TestData.Masters
            .Where(m => m.ExperienceYears >= 5)
            .ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(new[] { TestData.Master10YearsId, TestData.Master7YearsId }, result.Select(m => m.Id).ToArray());
        Assert.DoesNotContain(result, m => m.Id == TestData.Master2YearsId);
    }

    /// </summary>
    /// 2. Окошки выбранного мастера
    /// </summary>
    [Fact]
    public void ShouldReturnPositiveTimeGapsBetweenMasterAppointments()
    {
        // Arrange
        var appointments = TestData.Appointments
            .Where(a => a.MasterId == TestData.Master10YearsId)
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

    /// <summary>
    /// 3. Топ 5 популярных услуг
    /// </summary>
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
        Assert.Equal(TestData.ServicePopularId, top5[0].Service.Id);
        Assert.Equal(TestData.ServiceSecondId,  top5[1].Service.Id);
        Assert.Equal(TestData.ServiceThirdId,   top5[2].Service.Id);
        
        // Педикюр(2) и Ботокс(2) делят 4-5 места, порядок может быть разным.
        var top4And5 = top5.Skip(3).Select(x => x.Service.Id).ToList();
        Assert.Contains(TestData.ServiceFourthId, top4And5);
        Assert.Contains(TestData.ServiceUnpopularId, top4And5);

        Assert.Equal(5, top5[0].Count);
        Assert.Equal(4, top5[1].Count);
        Assert.Equal(3, top5[2].Count);
        Assert.Equal(2, top5[3].Count);
        Assert.Equal(2, top5[4].Count);

        // Услуга с 1 записью не должна попасть в топ
        Assert.DoesNotContain(top5, x => x.Service.Id == TestData.ServiceFifthId);
    }

    /// <summary>
    /// 4. Повторные записи клиентов за последний месяц
    /// </summary>
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

    /// <summary>
    /// 5. Клиенты у нескольких мастеров, сортировка по дате рождения
    /// </summary>
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
        Assert.Equal(TestData.ClientOldId,    clients[0].Id);
        Assert.Equal(TestData.ClientMiddleId, clients[1].Id);
        Assert.Equal(TestData.ClientYoungId,  clients[2].Id);
    }
}