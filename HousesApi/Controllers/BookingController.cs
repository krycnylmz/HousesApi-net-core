using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HousesApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace HousesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return Ok(bookings);
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(long id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // POST: api/Booking
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bookingDto.check_in >= bookingDto.check_out)
            {
                return Conflict("Dates conflicts theirselfs!");
            }

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Gelen tarih aralığında rezervasyon kontrolü
                bool isBookingAvailable = CheckBookingAvailability(bookingDto.house_id, bookingDto.check_in, bookingDto.check_out);

                if (!isBookingAvailable)
                {
                    return Conflict("This house is not available for that time.");
                }

                // Rezervasyonu eklemek için devam edin
                Booking booking = new Booking()
                {
                    house_id = bookingDto.house_id,
                    user_id = bookingDto.user_id,
                    number_of_people = bookingDto.number_of_people,
                    check_in = bookingDto.check_in,
                    check_out = bookingDto.check_out,
                    created_at = DateTime.Now
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
            }
            else
            {
                // Kullanıcı giriş yapmamış, hata yanıtı döndür
                return Unauthorized("Giriş yapmadan rezervasyon yapamazsınız.");
            }
        }

        private bool CheckBookingAvailability(long houseId, DateTime checkIn, DateTime checkOut)
        {
            // Belirtilen ev ve tarih aralığında rezervasyon yapılıp yapılmadığını kontrol et
            bool isAvailable = !_context.Bookings.Any(b =>
                b.house_id == houseId &&
                ((checkIn >= b.check_in && checkIn <= b.check_out) || (checkOut >= b.check_in && checkOut <= b.check_out)));

            return isAvailable;
        }


    }
}
