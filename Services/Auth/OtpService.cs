
//using MimeKit;
//using System.Net.Mail;

//namespace TrainingCenterAPI.Services.Auth
//{
//    public class OtpService : IOtpService
//    {
//        public OtpService()
//        {

//        }

//        public async Task<string> GenerateOtp()
//        {
//            var rng = new Random();
//            return rng.Next(100000, 999999).ToString(); // كود من 6 أرقام
//        }

//        public async Task SendVerificationEmailAsync(string toEmail, string otp)
//        {
//            var message = new MimeMessage();
//            message.From.Add(new MailboxAddress("Training Center", "yourEmail@gmail.com"));
//            message.To.Add(new MailboxAddress("", toEmail));
//            message.Subject = "تأكيد البريد الإلكتروني";

//            message.Body = new TextPart("plain")
//            {
//                Text = $"رمز التحقق الخاص بك هو: {otp}\nصالح لمدة 5 دقائق فقط."
//            };

//            using (var client = new SmtpClient())
//            {
//                await client.ConnectAsync("smtp.gmail.com", 587, false);
//                await client.AuthenticateAsync("yourEmail@gmail.com", "AppPasswordHere");
//                await client.SendAsync(message);
//                await client.DisconnectAsync(true);
//            }
//        }

//        public bool VerifyEmail(string email, string inputOtp, EmailVerification storedOtp)
//        {
//            if (storedOtp == null || storedOtp.Email != email) return false;
//            if (storedOtp.Expiry < DateTime.UtcNow) return false;
//            if (storedOtp.Code != inputOtp) return false;

//            storedOtp.IsVerified = true;
//            return true;
//        }
//    }
//}
