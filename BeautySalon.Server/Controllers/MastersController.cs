using BeautySalon.Domain.Entities;
using BeautySalon.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MastersController : ControllerBase
{
    private readonly ISalonDataStore _store;

    public MastersController(ISalonDataStore store)
    {
        _store = store;
    }

    // GET: api/masters
    [HttpGet]
    public ActionResult<IEnumerable<Master>> GetAll()
    {
        return Ok(_store.Masters);
    }

    // GET: api/masters/{id}
    [HttpGet("{id}")]
    public ActionResult<Master> GetById(string id)
    {
        var master = _store.Masters.FirstOrDefault(m => m.Id == id);
        if (master == null) return NotFound();
        return Ok(master);
    }

    // POST: api/masters
    [HttpPost]
    public ActionResult<Master> Create([FromBody] Master master)
    {
        if (string.IsNullOrEmpty(master.Id))
        {
            master.Id = Guid.NewGuid().ToString("N")[..8];
        }
        
        _store.Masters.Add(master);
        _store.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = master.Id }, master);
    }

    // PUT: api/masters/{id}
    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Master updatedMaster)
    {
        var master = _store.Masters.FirstOrDefault(m => m.Id == id);
        if (master == null) return NotFound();

        master.FullName = updatedMaster.FullName;
        master.Gender = updatedMaster.Gender;
        master.PassportNumber = updatedMaster.PassportNumber;
        master.Specialization = updatedMaster.Specialization;
        master.ExperienceYears = updatedMaster.ExperienceYears;

        _store.SaveChanges();
        return NoContent();
    }

    // DELETE: api/masters/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var master = _store.Masters.FirstOrDefault(m => m.Id == id);
        if (master == null) return NotFound();

        _store.Masters.Remove(master);
        _store.SaveChanges();
        return NoContent();
    }
}