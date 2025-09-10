using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingCenterAPI.Models
{
    public class Classes
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public TeacherDetails Teacher { get; set; }

        public int? PackageSize { get; set; } // 1 طالب - 2 طالب...

        public int CurrentStudentsCount { get; set; } = 0;

        [Required]
        public ClassStatus Status { get; set; } // ✅ حالة واحدة

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? ClassTime { get; set; }
        public DateTime? DeletedAt { get; set; }  // 👈 Soft Delete
        public Guid LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
        public ICollection<WaitingList> WaitingList { get; set; }

    }
}
