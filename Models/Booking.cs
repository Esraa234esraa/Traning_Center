using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.Models
{
   public enum BookingStatus
{
    Pending,    // حجز جديد لم يتم تأكيده
    Confirmed,  // حجز تم تأكيده وإضافته للحصص
    Expired     // الحجز الذي مر عليه اليوم وتم تحويله لقائمة الانتظار
}

public class Booking
{
    [Key]
    public Guid Id { get; set; }

    public string StudentName { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime BookingDate { get; set; }

        public DateTime BookingTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; } 

    public Guid? TeacherId { get; set; } // في حالة التخصيص للحصص

    // علاقة اختيارية مع المعلم (يمكن تفعيلها لاحقًا)
    // public TeacherDetails Teacher { get; set; }
}

}
