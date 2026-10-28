using BeautySalon.Domain.Entities;
using BeautySalon.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

// DTO для создания/обновления записи. 
// Содержит только ID, а не целые объекты.
public class AppointmentRequest
{
    public DateTime StartTime { get; set; }
    public bool IsRegularClient { get; set; }
    public int ClientId { get; set; }
    public int MasterId { get; set; }
    public int ServiceId { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly ISalonDataStore _store;

    public AppointmentsController(ISalonDataStore store)
    {
        _store = store;
    }

    // GET: api/appointments
    [HttpGet]
    public ActionResult<IEnumerable<Appointment>> GetAll()
    {
        return Ok(_store.Appointments);
    }

    // GET: api/appointments/{id}
    [HttpGet("{id}")]
    public ActionResult<Appointment> GetById(int id)
    {
        var appointment = _store.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null) return NotFound();
        return Ok(appointment);
    }

    // POST: api/appointments
    [HttpPost]
    public ActionResult<Appointment> Create([FromBody] AppointmentRequest request)
    {
        // 1. Находим связанные сущности по ID
        var client = _store.Clients.FirstOrDefault(c => c.Id == request.ClientId);
        var master = _store.Masters.FirstOrDefault(m => m.Id == request.MasterId);
        var service = _store.Services.FirstOrDefault(s => s.Id == request.ServiceId);

        // Если хотя бы один ID передан неверно, возвращаем ошибку 400
        if (client == null || master == null || service == null)
        {
            return BadRequest("Указан неверный ID клиента, мастера или услуги.");
        }

        // 2. Создаем запись, заполняя required-свойства
        var appointment = new Appointment
        {
            Id = _store.Appointments.Any() ? _store.Appointments.Max(a => a.Id) + 1 : 1,
            StartTime = request.StartTime,
            IsRegularClient = request.IsRegularClient,
            ClientId = client.Id,
            Client = client,
            MasterId = master.Id,
            Master = master,
            ServiceId = service.Id,
            Service = service
        };

        _store.Appointments.Add(appointment);
        _store.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
    }

    // PUT: api/appointments/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AppointmentRequest request)
    {
        var appointment = _store.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null) return NotFound();

        var client = _store.Clients.FirstOrDefault(c => c.Id == request.ClientId);
        var master = _store.Masters.FirstOrDefault(m => m.Id == request.MasterId);
        var service = _store.Services.FirstOrDefault(s => s.Id == request.ServiceId);

        if (client == null || master == null || service == null)
        {
            return BadRequest("Указан неверный ID клиента, мастера или услуги.");
        }

        // Обновляем поля
        appointment.StartTime = request.StartTime;
        appointment.IsRegularClient = request.IsRegularClient;
        appointment.ClientId = client.Id;
        appointment.Client = client;
        appointment.MasterId = master.Id;
        appointment.Master = master;
        appointment.ServiceId = service.Id;
        appointment.Service = service;

        _store.SaveChanges();
        return NoContent();
    }

    // DELETE: api/appointments/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var appointment = _store.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null) return NotFound();

        _store.Appointments.Remove(appointment);
        _store.SaveChanges();
        return NoContent();
    }
}