using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingCenterAPI.Models
{
    public class StudentClass
    {
        [Key]
        public Guid Id { get; set; }

        // 🔗 علاقة مع Class
        [Required]
        public Guid ClassId { get; set; }
        public Classes Class { get; set; }

        // 🔗 علاقة مع Student (ApplicationUser)
        [Required]
        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        // ✅ Soft Delete
        public DateTime? DeletedAt { get; set; }
        public Guid LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }
        // ✅ حالة الدفع
        public bool IsPaid { get; set; } = false;
    }
}
