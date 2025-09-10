using System;

namespace TrainingCenterAPI.DTOs.Booking
{
    public class BookingDto
    {
        public Guid Id { get; set; }

        public string StudentName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime BookingTime { get; set; }

        public string BookingStatus { get; set; }
    }
}
