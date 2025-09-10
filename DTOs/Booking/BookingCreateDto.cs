using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.DTOs.Booking
{
    public class BookingCreateDto
    {
        [Key]
        public string StudentName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BookingDate { get; set; }
        public DateTime BookingTime { get; set; }


    }
}
