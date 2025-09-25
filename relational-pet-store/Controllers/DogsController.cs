using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using relational_pet_store.Data;
using relational_pet_store.Models;

namespace relational_pet_store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DogsController : ControllerBase
{
    private readonly PetStoreDbContext _context;

    public DogsController(PetStoreDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all dogs with their traits
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dog>>> GetDogs()
    {
        return await _context.Dogs.ToListAsync();
    }

    /// <summary>
    /// Get a specific dog by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Dog>> GetDog(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);

        if (dog == null)
        {
            return NotFound();
        }

        return dog;
    }

    /// <summary>
    /// Create a new dog
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Dog>> PostDog(Dog dog)
    {
        _context.Dogs.Add(dog);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDog", new { id = dog.Id }, dog);
    }

    /// <summary>
    /// Update an existing dog
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDog(int id, Dog dog)
    {
        if (id != dog.Id)
        {
            return BadRequest();
        }

        _context.Entry(dog).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DogExists(id))
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
    /// Delete a dog
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDog(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog == null)
        {
            return NotFound();
        }

        _context.Dogs.Remove(dog);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DogExists(int id)
    {
        return _context.Dogs.Any(e => e.Id == id);
    }
}