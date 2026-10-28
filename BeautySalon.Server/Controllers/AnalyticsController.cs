using BeautySalon.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly ISalonDataStore _store;

    public AnalyticsController(ISalonDataStore store)
    {
        _store = store;
    }

    [HttpGet("masters-with-experience")]
    public IActionResult GetMastersWithExperience([FromQuery] int minYears = 5)
    {
        var result = _store.Masters
            .Where(m => m.ExperienceYears >= minYears)
            .Select(m => new { m.Id, m.FullName, m.Specialization, m.ExperienceYears })
            .ToList();

        return Ok(new { MinYears = minYears, Count = result.Count, Masters = result });
    }

    [HttpGet("master-gaps/{masterId}")] 
    public IActionResult GetMasterTimeGaps(int masterId)
    {
        var master = _store.Masters.FirstOrDefault(m => m.Id == masterId);
        if (master == null)
        {
            return NotFound($"Мастер с id '{masterId}' не найден.");
        }

        var appointments = _store.Appointments
            .Where(a => a.MasterId == masterId)
            .OrderBy(a => a.StartTime)
            .ToList();

        if (appointments.Count < 2)
        {
            return Ok(new
            {
                MasterId = masterId,
                MasterName = master.FullName,
                GapsCount = 0,
                Message = "У мастера меньше двух записей — окошек нет."
            });
        }

        var gaps = new List<object>();
        for (var i = 0; i < appointments.Count - 1; i++)
        {
            var current = appointments[i];
            var next = appointments[i + 1];
            var currentEnd = current.StartTime + current.Service.Duration;

            if (next.StartTime > currentEnd)
            {
                gaps.Add(new
                {
                    From = currentEnd,
                    To = next.StartTime,
                    DurationMinutes = (int)(next.StartTime - currentEnd).TotalMinutes
                });
            }
        }

        return Ok(new { MasterId = masterId, MasterName = master.FullName, GapsCount = gaps.Count, Gaps = gaps });
    }

    [HttpGet("top-services")]
    public IActionResult GetTopServices([FromQuery] int take = 5)
    {
        var top = _store.Appointments
            .GroupBy(a => a.ServiceId)
            .Select(g => new
            {
                ServiceId = g.Key,
                ServiceName = _store.Services.First(s => s.Id == g.Key).Name,
                Category = _store.Services.First(s => s.Id == g.Key).Category,
                AppointmentCount = g.Count()
            })
            .OrderByDescending(x => x.AppointmentCount)
            .Take(take)
            .ToList();

        return Ok(top);
    }

    [HttpGet("repeat-clients")]
    public IActionResult GetRepeatClients([FromQuery] int days = 30)
    {
        var threshold = DateTime.Now.AddDays(-days);

        var repeats = _store.Appointments
            .Where(a => a.StartTime >= threshold)
            .GroupBy(a => a.ClientId)
            .Where(g => g.Count() > 1)
            .Select(g => new
            {
                ClientId = g.Key,
                ClientName = _store.Clients.First(c => c.Id == g.Key).FullName,
                RepeatCount = g.Count()
            })
            .OrderByDescending(x => x.RepeatCount)
            .ToList();

        return Ok(new { PeriodDays = days, ClientsWithRepeats = repeats.Count, Details = repeats });
    }

    [HttpGet("clients-multiple-masters")]
    public IActionResult GetClientsWithMultipleMasters()
    {
        var result = _store.Appointments
            .GroupBy(a => a.ClientId)
            .Where(g => g.Select(a => a.MasterId).Distinct().Count() > 1)
            .Select(g => new
            {
                ClientId = g.Key,
                FullName = _store.Clients.First(c => c.Id == g.Key).FullName,
                BirthDate = _store.Clients.First(c => c.Id == g.Key).BirthDate,
                UniqueMastersCount = g.Select(a => a.MasterId).Distinct().Count()
            })
            .OrderBy(x => x.BirthDate)
            .ToList();

        return Ok(new { Count = result.Count, Clients = result });
    }
}