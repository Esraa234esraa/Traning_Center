using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.Models.BaseEntitys
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // ✅ Soft Delete
        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
