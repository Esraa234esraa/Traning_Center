using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace TrainingCenterAPI.Models
{
    public class WaitingList
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Class")]
        public Guid ClassId { get; set; }
        public Classes Class { get; set; }

        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }  // 👈 هنا ApplicationUser مش StudentClass
        public DateTime? DeletedAt { get; set; }  // 👈 Soft Delete

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
