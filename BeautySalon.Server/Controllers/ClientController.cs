using BeautySalon.Domain.Entities;
using BeautySalon.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ISalonDataStore _store;

    public ClientsController(ISalonDataStore store)
    {
        _store = store;
    }

    // GET: api/clients
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll()
    {
        return Ok(_store.Clients);
    }

    // GET: api/clients/{id}
    [HttpGet("{id}")]
    public ActionResult<Client> GetById(string id)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return NotFound();
        return Ok(client);
    }

    // POST: api/clients
    [HttpPost]
    public ActionResult<Client> Create([FromBody] Client client)
    {
        if (string.IsNullOrEmpty(client.Id))
        {
            client.Id = Guid.NewGuid().ToString("N")[..8]; 
        }
        
        _store.Clients.Add(client);
        _store.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    // PUT: api/clients/{id}
    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Client updatedClient)
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

    // DELETE: api/clients/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return NotFound();

        _store.Clients.Remove(client);
        _store.SaveChanges();
        return NoContent();
    }
}