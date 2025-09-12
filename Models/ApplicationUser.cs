using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingCenterAPI.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        public string FullName { get; set; } = string.Empty;


        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد غير صحيحة")]
        public override string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صالح")]
        public override string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? VerifyCode { get; set; }

        public bool IsVerified { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = "Student";

        public Guid? LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }

        // Navigation property (nullable لتجنب المشاكل إن لم يكن معلّق)
        public TeacherDetails? TeacherDetails { get; set; }
        public ICollection<StudentDetails> StudentDetails = new HashSet<StudentDetails>();
        public ICollection<WaitingList> WaitingList { get; set; }
        public ICollection<StudentClass> Classes { get; set; } = new List<StudentClass>();

    }
}
