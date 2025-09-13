using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.Students
{
    public class CurrentStudent : BaseEntity
    {
        public required string StudentName { get; set; }
        public Guid? LevelId { get; set; }

        [ForeignKey("LevelId")]
        public Level Level { get; set; }

        public Guid? TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public required string PhoneNumber { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public decimal? Money { get; set; }
        public bool IsPaid { get; set; }


    }
}
