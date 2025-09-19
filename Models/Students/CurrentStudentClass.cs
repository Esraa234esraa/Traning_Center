using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.Students
{
    public class CurrentStudentClass : BaseEntity
    {



        // 🔗 علاقة مع Class
        [Required]
        public Guid ClassId { get; set; }
        public Classes Class { get; set; }

        // 🔗 علاقة مع Student (ApplicationUser)
        [Required]


        public Guid StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]


        public CurrentStudent Student { get; set; }



        // ✅ حالة الدفع
        public bool IsPaid { get; set; } = false;
    }
}
