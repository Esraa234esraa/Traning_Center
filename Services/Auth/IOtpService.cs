namespace TrainingCenterAPI.Services.Auth
{
    public interface IOtpService
    {

        public Task<string> GenerateOtp();
        public Task SendVerificationEmailAsync(string toEmail, string otp);
        public bool VerifyEmail(string email, string inputOtp, EmailVerification storedOtp);

    }
}
