using BeautySalon.Domain.Entities;
using BeautySalon.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над клиентами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(ISalonDataStore _store) : ControllerBase
{
    /// <summary>
    /// Возвращает всех клиентов.
    /// </summary>
    // GET: api/clients
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll()
    {
        return Ok(_store.Clients);
    }

    /// <summary>
    /// Возвращает клиента по идентификатору.
    /// </summary>
    // GET: api/clients/{id}
    [HttpGet("{id}")]
    public ActionResult<Client> GetById(int id)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return NotFound();
        return Ok(client);
    }

    /// <summary>
    /// Создает нового клиента.
    /// </summary>
    // POST: api/clients
    [HttpPost]
    public ActionResult<Client> Create([FromBody] Client client)
    {
        client.Id = _store.Clients.Any() ? _store.Clients.Max(c => c.Id) + 1 : 1;
        _store.Clients.Add(client);
        
        _store.Clients.Add(client);
        _store.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    // PUT: api/clients/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Client updatedClient)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return NotFound();

        // Обновляем поля
        client.FullName = updatedClient.FullName;
        client.Gender = updatedClient.Gender;
        client.BirthDate = updatedClient.BirthDate;
        client.Phone = updatedClient.Phone;

        _store.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    // DELETE: api/clients/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return NotFound();

        _store.Clients.Remove(client);
        _store.SaveChanges();
        return NoContent();
    }
}