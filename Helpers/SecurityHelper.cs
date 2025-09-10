// Services/SecurityHelper.cs
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class SecurityHelper
{
    public static (string Salt, string Hash) HashOtp(string otp)
    {
        // salt random
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        string salt = Convert.ToBase64String(saltBytes);

        // PBKDF2
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: otp,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        return (salt, hash);
    }

    public static bool VerifyOtp(string otp, string saltBase64, string expectedHashBase64)
    {
        var saltBytes = Convert.FromBase64String(saltBase64);
        string computed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: otp,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(computed),
            Convert.FromBase64String(expectedHashBase64));
    }
}
