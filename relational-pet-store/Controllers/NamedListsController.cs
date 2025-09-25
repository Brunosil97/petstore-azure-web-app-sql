using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using relational_pet_store.Data;
using relational_pet_store.Models;

namespace relational_pet_store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NamedListsController : ControllerBase
{
    private readonly PetStoreDbContext _context;

    public NamedListsController(PetStoreDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all named lists
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NamedList>>> GetNamedLists()
    {
        return await _context.NamedLists.ToListAsync();
    }

    /// <summary>
    /// Get a specific named list by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<NamedList>> GetNamedList(int id)
    {
        var namedList = await _context.NamedLists.FindAsync(id);

        if (namedList == null)
        {
            return NotFound();
        }

        return namedList;
    }

    /// <summary>
    /// Get all dogs in a specific named list
    /// </summary>
    [HttpGet("{id}/dogs")]
    public async Task<ActionResult<IEnumerable<Dog>>> GetDogsInList(int id)
    {
        var namedList = await _context.NamedLists.FindAsync(id);
        if (namedList == null)
        {
            return NotFound();
        }

        var dogs = await _context.DogLists
            .Where(dl => dl.NamedListId == id)
            .Include(dl => dl.Dog)
            .Select(dl => dl.Dog)
            .ToListAsync();

        return dogs;
    }

    /// <summary>
    /// Get all cats in a specific named list
    /// </summary>
    [HttpGet("{id}/cats")]
    public async Task<ActionResult<IEnumerable<Cat>>> GetCatsInList(int id)
    {
        var namedList = await _context.NamedLists.FindAsync(id);
        if (namedList == null)
        {
            return NotFound();
        }

        var cats = await _context.CatLists
            .Where(cl => cl.NamedListId == id)
            .Include(cl => cl.Cat)
            .Select(cl => cl.Cat)
            .ToListAsync();

        return cats;
    }

    /// <summary>
    /// Create a new named list
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<NamedList>> PostNamedList(NamedList namedList)
    {
        _context.NamedLists.Add(namedList);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetNamedList", new { id = namedList.Id }, namedList);
    }

    /// <summary>
    /// Update an existing named list
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNamedList(int id, NamedList namedList)
    {
        if (id != namedList.Id)
        {
            return BadRequest();
        }

        namedList.UpdatedAt = DateTime.UtcNow;
        _context.Entry(namedList).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NamedListExists(id))
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
    /// Add a dog to a named list
    /// </summary>
    [HttpPost("{listId}/dogs/{dogId}")]
    public async Task<IActionResult> AddDogToList(int listId, int dogId)
    {
        var namedList = await _context.NamedLists.FindAsync(listId);
        var dog = await _context.Dogs.FindAsync(dogId);

        if (namedList == null || dog == null)
        {
            return NotFound();
        }

        var existingEntry = await _context.DogLists
            .FirstOrDefaultAsync(dl => dl.NamedListId == listId && dl.DogId == dogId);

        if (existingEntry != null)
        {
            return Conflict("Dog is already in this list");
        }

        var dogList = new DogList
        {
            DogId = dogId,
            NamedListId = listId
        };

        _context.DogLists.Add(dogList);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Add a cat to a named list
    /// </summary>
    [HttpPost("{listId}/cats/{catId}")]
    public async Task<IActionResult> AddCatToList(int listId, int catId)
    {
        var namedList = await _context.NamedLists.FindAsync(listId);
        var cat = await _context.Cats.FindAsync(catId);

        if (namedList == null || cat == null)
        {
            return NotFound();
        }

        var existingEntry = await _context.CatLists
            .FirstOrDefaultAsync(cl => cl.NamedListId == listId && cl.CatId == catId);

        if (existingEntry != null)
        {
            return Conflict("Cat is already in this list");
        }

        var catList = new CatList
        {
            CatId = catId,
            NamedListId = listId
        };

        _context.CatLists.Add(catList);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove a dog from a named list
    /// </summary>
    [HttpDelete("{listId}/dogs/{dogId}")]
    public async Task<IActionResult> RemoveDogFromList(int listId, int dogId)
    {
        var dogList = await _context.DogLists
            .FirstOrDefaultAsync(dl => dl.NamedListId == listId && dl.DogId == dogId);

        if (dogList == null)
        {
            return NotFound();
        }

        _context.DogLists.Remove(dogList);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove a cat from a named list
    /// </summary>
    [HttpDelete("{listId}/cats/{catId}")]
    public async Task<IActionResult> RemoveCatFromList(int listId, int catId)
    {
        var catList = await _context.CatLists
            .FirstOrDefaultAsync(cl => cl.NamedListId == listId && cl.CatId == catId);

        if (catList == null)
        {
            return NotFound();
        }

        _context.CatLists.Remove(catList);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Delete a named list
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNamedList(int id)
    {
        var namedList = await _context.NamedLists.FindAsync(id);
        if (namedList == null)
        {
            return NotFound();
        }

        _context.NamedLists.Remove(namedList);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NamedListExists(int id)
    {
        return _context.NamedLists.Any(e => e.Id == id);
    }
}