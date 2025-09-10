//using TrainingCenterAPI.Data;
//using TrainingCenterAPI.Models;

//namespace TrainingCenterAPI.Services.Auth
//{
//    public class OtpService:IOtpService
//    {
//        private readonly ApplicationDbContext _db;
//        private readonly IEmailService _emailService;
//        // optional ISmsService _smsService
//        private readonly ILogger<OtpService> _logger;

//        // config constants (tweak as needed)
//        private const int OtpLength = 6;
//        private const int OtpExpiryMinutes = 15;
//        private const int MaxAttempts = 5;
//        private static readonly TimeSpan ResendCooldown = TimeSpan.FromSeconds(30); // minimum time between resends

//        public OtpService(ApplicationDbContext db, IEmailService emailService, ILogger<OtpService> logger)
//        {
//            _db = db;
//            _emailService = emailService;
//            _logger = logger;
//        }

//        public async Task GenerateAndSendOtpAsync(Guid userId, string destination, bool useSms = false)
//        {
//            // remove old OTPs for this user (optional)
//            var existing = await _db.OtpVerifications.Where(o => o.UserId == userId).ToListAsync();
//            if (existing.Any())
//            {
//                _db.OtpVerifications.RemoveRange(existing);
//                await _db.SaveChangesAsync();
//            }

//            var code = new Random().Next((int)Math.Pow(10, OtpLength - 1), (int)Math.Pow(10, OtpLength) - 1).ToString();
//            var (salt, hash) = SecurityHelper.HashOtp(code);

//            var otp = new OtpVerification
//            {
//                UserId = userId,
//                OtpHash = hash,
//                OtpSalt = salt,
//                ExpiresAt = DateTime.UtcNow.AddMinutes(OtpExpiryMinutes),
//                AttemptCount = 0,
//                CreatedAt = DateTime.UtcNow,
//                LastSentAt = DateTime.UtcNow
//            };

//            _db.OtpVerifications.Add(otp);
//            await _db.SaveChangesAsync();

//            // send via email or sms
//            if (useSms)
//            {
//                // TODO: call SMS provider
//                _logger.LogInformation("Send OTP {code} to {dest} by SMS (simulate)", code, destination);
//            }
//            else
//            {
//                await _emailService.SendVerificationCodeAsync(destination, code);
//            }
//        }

//        public async Task ResendOtpAsync(Guid userId, string destination, bool useSms = false)
//        {
//            var otp = await _db.OtpVerifications
//                .Where(o => o.UserId == userId)
//                .OrderByDescending(o => o.CreatedAt)
//                .FirstOrDefaultAsync();

//            if (otp == null)
//            {
//                await GenerateAndSendOtpAsync(userId, destination, useSms);
//                return;
//            }

//            if (otp.LastSentAt.HasValue && DateTime.UtcNow - otp.LastSentAt.Value < ResendCooldown)
//                throw new InvalidOperationException($"Please wait {ResendCooldown.TotalSeconds} seconds before resending.");

//            // generate a new code (recommended) and update hash & expiry
//            var code = new Random().Next((int)Math.Pow(10, OtpLength - 1), (int)Math.Pow(10, OtpLength) - 1).ToString();
//            var (salt, hash) = SecurityHelper.HashOtp(code);

//            otp.OtpHash = hash;
//            otp.OtpSalt = salt;
//            otp.ExpiresAt = DateTime.UtcNow.AddMinutes(OtpExpiryMinutes);
//            otp.AttemptCount = 0;
//            otp.LastSentAt = DateTime.UtcNow;

//            _db.OtpVerifications.Update(otp);
//            await _db.SaveChangesAsync();

//            if (useSms)
//            {
//                _logger.LogInformation("Resend OTP {code} to {dest} by SMS (simulate)", code, destination);
//            }
//            else
//            {
//                await _emailService.SendVerificationCodeAsync(destination, code);
//            }
//        }

//        public async Task<VerifyResult> VerifyOtpAsync(Guid userId, string code)
//        {
//            var otp = await _db.OtpVerifications
//                .Where(o => o.UserId == userId)
//                .OrderByDescending(o => o.CreatedAt)
//                .FirstOrDefaultAsync();

//            if (otp == null)
//                return new VerifyResult { Success = false, Message = "No verification code found. Request a new code." };

//            if (otp.ExpiresAt < DateTime.UtcNow)
//                return new VerifyResult { Success = false, Message = "Verification code expired. Please request a new code." };

//            if (otp.AttemptCount >= MaxAttempts)
//                return new VerifyResult { Success = false, Message = "Maximum verification attempts exceeded. Request a new code." };

//            // increment attempt count (save even on success to reduce brute force chance)
//            otp.AttemptCount++;
//            _db.OtpVerifications.Update(otp);
//            await _db.SaveChangesAsync();

//            bool isValid = SecurityHelper.VerifyOtp(code, otp.OtpSalt, otp.OtpHash);

//            if (!isValid)
//                return new VerifyResult { Success = false, Message = "Invalid verification code." };

//            // valid → mark user verified
//            var user = await _db.Users.FindAsync(userId);
//            if (user == null)
//                return new VerifyResult { Success = false, Message = "User not found." };

//            user.IsVerified = true;
//            _db.Users.Update(user);

//            // remove otp record (cleanup)
//            _db.OtpVerifications.Remove(otp);

//            await _db.SaveChangesAsync();

//            return new VerifyResult { Success = true, Message = "Account verified successfully." };
//        }
//    }
//}
