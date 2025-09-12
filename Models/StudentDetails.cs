using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TrainingCenterAPI.Enums.Enums;

namespace TrainingCenterAPI.Models
{
    public class StudentDetails
    {
        [Key]
        public Guid Id { get; set; }



        public string? Gender { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }  // 👈 Soft Delete

        [Required]
        public Guid UserId { get; set; }   // FK to ApplicationUser

        [ForeignKey(nameof(UserId))]

        public ApplicationUser User { get; set; }
        public Guid TeacherId { get; set; }

        public StudentStatus studentStatus { get; set; }


    }
}
