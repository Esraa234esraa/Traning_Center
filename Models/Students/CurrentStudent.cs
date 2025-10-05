using TrainingCenterAPI.Models.BaseEntitys;
using TrainingCenterAPI.Models.Notes;

namespace TrainingCenterAPI.Models.Students
{
    public class CurrentStudent : BaseEntity
    {
        public required string StudentName { get; set; }

        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public required string PhoneNumber { get; set; }



        public bool IsPaid { get; set; } = false;
        public virtual ICollection<CurrentStudentClass> GetCurrentStudentClasses { get; set; } = new HashSet<CurrentStudentClass>();
        public virtual ICollection<Note> notes { get; set; } = new HashSet<Note>();





    }
}
