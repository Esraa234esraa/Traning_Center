namespace TrainingCenterAPI.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

}
