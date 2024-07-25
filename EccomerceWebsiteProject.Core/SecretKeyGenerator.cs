using System;
using System.Security.Cryptography;

namespace EccomerceWebsiteProject.Core
{
    public class SecretKeyGenerator
    {
        public string GenerateSecretKey()
        {
            // Generate a random byte array
            byte[] randomNumber = new byte[32]; // 32 bytes for a 256-bit key
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
            }

            // Convert the byte array to a base64 string
            string secretKey = Convert.ToBase64String(randomNumber);

            return secretKey;
        }
    }
}
