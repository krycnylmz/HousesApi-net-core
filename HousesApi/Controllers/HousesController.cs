//HousesController
using HousesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


[Route("api/[controller]")]
[ApiController]
public class HousesController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public HousesController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Other using statements...

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HouseDto>>> GetHouses()
    {
        var houses = await _dbContext.Houses.ToListAsync();
        var houseDtos = new List<HouseDto>();

        foreach (var house in houses)
        {
            var houseDto = new HouseDto
            {
                house_name = house.house_name,
                location = house.location,
                capacity = house.capacity,
                garden_view = house.garden_view,
                wifi = house.wifi,
                lake_view = house.lake_view,
                pool = house.pool,
                lake_access = house.lake_access,
                hot_tub = house.hot_tub,
            };

            houseDtos.Add(houseDto);
        }

        return Ok(houseDtos);
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

    // GET: api/Houses/Available
    [HttpGet("Available")]
    public async Task<ActionResult<IEnumerable<House>>> GetAvailableHouses([FromQuery] int capacity, [FromQuery] string location, [FromQuery] DateTime check_in, [FromQuery] DateTime check_out)
    {
        // Uygun evleri getir
        var availableHouses = await _dbContext.Houses
            .Where(h => h.capacity >= capacity && h.location == location &&
                        !_dbContext.Bookings.Any(b => b.house_id == h.Id &&
                                                       (check_in >= b.check_in && check_in <= b.check_out) ||
                                                       (check_out >= b.check_in && check_out <= b.check_out)))
            .ToListAsync();

        return availableHouses;
    }


    // POST: api/Houses
    //[HttpPost]
    //public async Task<ActionResult<House>> PostHouse(HouseDto houseDto)
    //{
    //    // HouseDto'yu House sınıfına dönüştürme
    //    // Burada AutoMapper gibi bir paket kullanıp hepisni tek seferde yapmak isterdim ancak zamanım yetmedi AutoMapper mekanizmasını çözmeye
    //    House house = new House()
    //    {
    //        // House sınıfının özelliklerine, HouseDto'nun özelliklerini atayalım
    //        user_id = 1,
    //        house_name = houseDto.house_name,
    //        location = houseDto.location,
    //        capacity = houseDto.capacity,
    //        wifi = houseDto.wifi,
    //        lake_view = houseDto.lake_view,
    //        pool = houseDto.pool,
    //        lake_access = houseDto.lake_access,
    //        hot_tub = houseDto.hot_tub,
    //        created_at = DateTime.Now
    //    };

    //    _dbContext.Houses.Add(house);

    //    await _dbContext.SaveChangesAsync();

    //    return CreatedAtAction(nameof(GetHouse), new { id = house.Id }, house);
    //}


}
