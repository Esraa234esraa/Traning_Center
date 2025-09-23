using TrainingCenterAPI.Models.BaseEntitys;
using static TrainingCenterAPI.Enums.Enums;

namespace TrainingCenterAPI.Models.Students
{
    public class NewStudent : BaseEntity
    {

        public required string StudentName { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public NewStudentStatus status { get; set; }
    }
}
