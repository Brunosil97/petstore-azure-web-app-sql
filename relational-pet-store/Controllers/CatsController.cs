using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using relational_pet_store.Data;
using relational_pet_store.Models;

namespace relational_pet_store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatsController : ControllerBase
{
    private readonly PetStoreDbContext _context;

    public CatsController(PetStoreDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all cats with their traits
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cat>>> GetCats()
    {
        return await _context.Cats.ToListAsync();
    }

    /// <summary>
    /// Get a specific cat by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Cat>> GetCat(int id)
    {
        var cat = await _context.Cats.FindAsync(id);

        if (cat == null)
        {
            return NotFound();
        }

        return cat;
    }

    /// <summary>
    /// Create a new cat
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Cat>> PostCat(Cat cat)
    {
        _context.Cats.Add(cat);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCat", new { id = cat.Id }, cat);
    }

    /// <summary>
    /// Update an existing cat
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCat(int id, Cat cat)
    {
        if (id != cat.Id)
        {
            return BadRequest();
        }

        _context.Entry(cat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CatExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a cat
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCat(int id)
    {
        var cat = await _context.Cats.FindAsync(id);
        if (cat == null)
        {
            return NotFound();
        }

        _context.Cats.Remove(cat);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CatExists(int id)
    {
        return _context.Cats.Any(e => e.Id == id);
    }
}