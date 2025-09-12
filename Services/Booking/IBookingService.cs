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
