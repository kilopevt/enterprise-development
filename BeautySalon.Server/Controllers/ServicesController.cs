using BeautySalon.Domain.Entities;
using BeautySalon.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly ISalonDataStore _store;

    public ServicesController(ISalonDataStore store)
    {
        _store = store;
    }

    // GET: api/services
    [HttpGet]
    public ActionResult<IEnumerable<Service>> GetAll()
    {
        return Ok(_store.Services);
    }

    // GET: api/services/{id}
    [HttpGet("{id}")]
    public ActionResult<Service> GetById(int id)
    {
        var service = _store.Services.FirstOrDefault(s => s.Id == id);
        if (service == null) return NotFound();
        return Ok(service);
    }

    // POST: api/services
    [HttpPost]
    public ActionResult<Service> Create([FromBody] Service service)
    {
        service.Id = _store.Services.Any() ? _store.Services.Max(s => s.Id) + 1 : 1;
        
        _store.Services.Add(service);
        _store.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    // PUT: api/services/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Service updatedService)
    {
        var service = _store.Services.FirstOrDefault(s => s.Id == id);
        if (service == null) return NotFound();

        service.Name = updatedService.Name;
        service.Category = updatedService.Category;
        service.Cost = updatedService.Cost;
        service.Duration = updatedService.Duration;

        _store.SaveChanges();
        return NoContent();
    }

    // DELETE: api/services/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var service = _store.Services.FirstOrDefault(s => s.Id == id);
        if (service == null) return NotFound();

        _store.Services.Remove(service);
        _store.SaveChanges();
        return NoContent();
    }
}