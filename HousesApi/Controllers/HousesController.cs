//HousesController
using HousesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class HousesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public HousesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/Houses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<House>>> GetHouses()
    {
        return await _dbContext.Houses.ToListAsync();
    }

    // GET: api/Houses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<House>> GetHouse(long id)
    {
        if (_dbContext.Houses == null)
        {
            return NotFound();
        }
        var House = await _dbContext.Houses.FindAsync(id);

        if (House == null)
        {
            return NotFound();
        }

        return House;
    }

    // POST: api/Houses
    [HttpPost]
    public async Task<ActionResult<House>> PostHouse(House house)
    {
        _dbContext.Houses.Add(house);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHouse), new { id = house.Id }, house);
    }
}
