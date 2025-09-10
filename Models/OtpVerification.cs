// Models/OtpVerification.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models;

public class OtpVerification
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    // HASH and SALT for the OTP
    [Required]
    public string OtpHash { get; set; }

    [Required]
    public string OtpSalt { get; set; }

    // صلاحية الكود
    public DateTime ExpiresAt { get; set; }

    // عدد محاولات التحقق
    public int AttemptCount { get; set; } = 0;

    // متى ارسل آخر مرة (لمنع resend متكرر)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastSentAt { get; set; }

    // علاقة اختيارية بـ ApplicationUser
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}
