using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.Students
{
    public class CurrentStudent : BaseEntity
    {
        public required string StudentName { get; set; }

        public Guid? ClassesId { get; set; }

        [ForeignKey("ClassesId")]
        public Classes Classes { get; set; }


        public string? Gender { get; set; }
        public string? City { get; set; }
        public required string PhoneNumber { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public decimal? Money { get; set; }
        public virtual ICollection<CurrentStudentClass> GetCurrentStudentClasses { get; set; } = new HashSet<CurrentStudentClass>();





    }
}
