using TrainingCenterAPI.DTOs.Booking;
using TrainingCenterAPI.Models;
using TrainingCenterAPI.Services.Booking;
using TrainingCenterAPI.Data;
using Microsoft.EntityFrameworkCore;
using TrainingCenterAPI.Responses;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _context;

    public BookingService(ApplicationDbContext context)
    {
        _context = context;
    }

    // إنشاء الحجز: يدخل BookingCreateDto، يرجع BookingDto مع الاستجابة
    public async Task<ResponseModel<BookingDto>> CreateBookingAsync(BookingCreateDto dto)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            StudentName = dto.StudentName,
            PhoneNumber = dto.PhoneNumber,
            BookingDate = dto.BookingDate,
            BookingTime = dto.BookingTime,
            CreatedAt = DateTime.UtcNow,
            Status = "Pending"
        };

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();

        var bookingDto = new BookingDto
        {
            Id = booking.Id,
            StudentName = booking.StudentName,
            PhoneNumber = booking.PhoneNumber,
            BookingDate = booking.BookingDate,
            BookingTime = booking.BookingTime,
            BookingStatus = booking.Status
        };

        return ResponseModel<BookingDto>.SuccessResponse(bookingDto, "تم إنشاء الحجز بنجاح");
    }

    // جلب الحجوزات المعلقة (Pending)
    public async Task<ResponseModel<List<BookingDto>>> GetPendingBookingsAsync()
    {
        var bookings = await _context.Bookings
            .Where(b => b.Status == "Pending")
            .OrderBy(b => b.BookingDate)
            .ThenBy(b => b.BookingTime)
            .ToListAsync();

        var dtos = bookings.Select(b => new BookingDto
        {
            Id = b.Id,
            StudentName = b.StudentName,
            PhoneNumber = b.PhoneNumber,
            BookingDate = b.BookingDate,
            BookingTime = b.BookingTime,
            BookingStatus = b.Status
        }).ToList();

        return ResponseModel<List<BookingDto>>.SuccessResponse(dtos);
    }

    // جلب الحجوزات منتهية الصلاحية (تاريخها مضى)
    public async Task<ResponseModel<List<BookingDto>>> GetExpiredBookingsAsync()
    {
        var today = DateTime.UtcNow.Date;

        var expiredBookings = await _context.Bookings
            .Where(b => b.Status == "Pending" && b.BookingDate < today)
            .OrderBy(b => b.BookingDate)
            .ToListAsync();

        var dtos = expiredBookings.Select(b => new BookingDto
        {
            Id = b.Id,
            StudentName = b.StudentName,
            PhoneNumber = b.PhoneNumber,
            BookingDate = b.BookingDate,
            BookingTime = b.BookingTime,
            BookingStatus = b.Status
        }).ToList();

        return ResponseModel<List<BookingDto>>.SuccessResponse(dtos);
    }

    // تحديث الحجوزات المنتهية وتحويل حالتها لـ "Expired"
    public async Task<ResponseModel<bool>> MoveExpiredBookingsToExpiredStatusAsync()
    {
        var today = DateTime.UtcNow.Date;

        var expiredBookings = await _context.Bookings
            .Where(b => b.Status == "Pending" && b.BookingDate < today)
            .ToListAsync();

        if (!expiredBookings.Any())
            return ResponseModel<bool>.SuccessResponse(true, "لا توجد حجوزات منتهية");

        foreach (var booking in expiredBookings)
        {
            booking.Status = "Expired";
        }

        await _context.SaveChangesAsync();

        return ResponseModel<bool>.SuccessResponse(true, "تم تحديث حالة الحجوزات المنتهية");
    }
}
