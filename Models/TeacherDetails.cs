using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models.Courses;

namespace TrainingCenterAPI.Models
{
    public class TeacherDetails
    {
        [Key]
        public Guid Id { get; set; }

        // FK إلى ApplicationUser

        public string Gender { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }  // 👈 Soft Delete

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public ICollection<Classes> Classes { get; set; } = new List<Classes>();
    }
}
