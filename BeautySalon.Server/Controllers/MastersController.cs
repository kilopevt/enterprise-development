using BeautySalon.Domain.Entities;
using BeautySalon.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над мастерами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MastersController(ISalonDataStore _store) : ControllerBase
{
    /// <summary>
    /// Возвращает всех мастеров.
    /// </summary>
    // GET: api/masters
    [HttpGet]
    public ActionResult<IEnumerable<Master>> GetAll()
    {
        return Ok(_store.Masters);
    }

    /// <summary>
    /// Возвращает мастера по идентификатору.
    /// </summary>
    // GET: api/masters/{id}
    [HttpGet("{id}")]
    public ActionResult<Master> GetById(int id)
    {
        var master = _store.Masters.FirstOrDefault(m => m.Id == id);
        if (master == null) return NotFound();
        return Ok(master);
    }

    /// <summary>
    /// Создает нового мастера.
    /// </summary>
    // POST: api/masters
    [HttpPost]
    public ActionResult<Master> Create([FromBody] Master master)
    {
        master.Id = _store.Masters.Any() ? _store.Masters.Max(m => m.Id) + 1 : 1;
        
        _store.Masters.Add(master);
        _store.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = master.Id }, master);
    }

    /// <summary>
    /// Обновляет данные мастера по идентификатору.
    /// </summary>
    // PUT: api/masters/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Master updatedMaster)
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

    /// <summary>
    /// Удаляет мастера по идентификатору.
    /// </summary>
    // DELETE: api/masters/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var master = _store.Masters.FirstOrDefault(m => m.Id == id);
        if (master == null) return NotFound();

        _store.Masters.Remove(master);
        _store.SaveChanges();
        return NoContent();
    }
}