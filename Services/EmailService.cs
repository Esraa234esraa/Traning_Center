using MimeKit;


namespace TrainingCenterAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }


        public string GenerateOtp()
        {
            var rng = new Random();
            return rng.Next(100000, 999999).ToString(); // كود من 6 أرقام
        }


        public async Task SendVerificationEmailAsync(string toEmail, string otp)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("مركز اللغة المثاليه", _config["EmailSettings:From"]));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = "تأكيد البريد الإلكتروني";

            email.Body = new TextPart("plain")
            {
                Text = $"رمز التحقق الخاص بك هو: {otp}\nصالح لمدة 5 دقائق فقط."
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), true);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public bool VerifyEmail(string email, string inputOtp, EmailVerification storedOtp)
        {
            if (storedOtp == null || storedOtp.Email != email) return false;
            if (storedOtp.Expiry < DateTime.UtcNow) return false;
            if (storedOtp.Code != inputOtp) return false;

            storedOtp.IsVerified = true;
            return true;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("مركز اللغة المثاليه", _config["EmailSettings:From"]));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), true);
            await smtp.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}


