using System;
using System.Text;
using System.Security.Cryptography;

namespace ProjectMann.Infrastructure.Crypto.Extensions
{
    public static class StringExtensions
    {
        public static string Encrypt(this string source)
        {
            var key = Encoding.UTF8.GetBytes("#$3n4$4$@pr0j3ctm4nn#");
            var strToEncrypt = Encoding.UTF8.GetBytes(source);

            using var sha1 = SHA1.Create();
            var hashKey = sha1.ComputeHash(key);

            var secretKey = new byte[16];
            Array.Copy(hashKey, secretKey, Math.Min(secretKey.Length, hashKey.Length));

            using var aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Key = secretKey;
            aes.IV = secretKey;

            using var encryptor = aes.CreateEncryptor();
            var result = encryptor.TransformFinalBlock(strToEncrypt, 0, strToEncrypt.Length);

            return Convert.ToBase64String(result);
        }
    }
}