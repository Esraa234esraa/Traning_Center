﻿namespace TrainingCenterAPI.Models
{
    public class EmailVerification
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsVerified { get; set; }
    }
}
