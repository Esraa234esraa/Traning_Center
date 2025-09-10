using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingCenterAPI.Models
{
    public class TeacherDetails
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; } // FK إلى ApplicationUser

        public string Gender { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }  // 👈 Soft Delete
        public ApplicationUser User { get; set; }

        public ICollection<Classes> Classes { get; set; } = new List<Classes>();
    }
}
