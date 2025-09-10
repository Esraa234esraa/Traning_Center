using TrainingCenterAPI.DTOs.Booking;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingCenterAPI.Responses;

namespace TrainingCenterAPI.Services.Booking
{
    public interface IBookingService
    {
        Task<ResponseModel<BookingDto>> CreateBookingAsync(BookingCreateDto dto);

        Task<ResponseModel<List<BookingDto>>> GetPendingBookingsAsync();

        Task<ResponseModel<List<BookingDto>>> GetExpiredBookingsAsync();

        Task<ResponseModel<bool>> MoveExpiredBookingsToExpiredStatusAsync();
    }
}
