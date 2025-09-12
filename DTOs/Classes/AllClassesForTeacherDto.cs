using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.DTOs.Classes
{
    public class AllClassesForTeacherDto
    {
        public Guid Id { get; set; }
        public int? PackageSize { get; set; } // 1 طالب - 2 طالب...

        public int CurrentStudentsCount { get; set; } = 0;

        [Required]
        public ClassStatus Status { get; set; } // ✅ حالة واحدة

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
