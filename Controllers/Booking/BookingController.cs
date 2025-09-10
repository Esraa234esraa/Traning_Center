using Microsoft.AspNetCore.Mvc;
using TrainingCenterAPI.DTOs.Booking;
using TrainingCenterAPI.Services.Booking;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
    {
        var result = await _bookingService.CreateBookingAsync(dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingBookings()
    {
        var result = await _bookingService.GetPendingBookingsAsync();

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpGet("expired")]
    public async Task<IActionResult> GetExpiredBookings()
    {
        var result = await _bookingService.GetExpiredBookingsAsync();

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPut("move-expired")]
    public async Task<IActionResult> MoveExpiredBookings()
    {
        var result = await _bookingService.MoveExpiredBookingsToExpiredStatusAsync();

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }
}
