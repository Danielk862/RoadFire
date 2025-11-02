using System.Security.Cryptography;
using System.Text;

namespace MS.RoadFire.Common.Helpers
{
    public static class CryptoManager
    {
        private static readonly string Key = "E245B02B42AFCAEB";

        /// <summary>
        /// Cifrar en base 64.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeBase64(string input)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(input);
            string base64Encoded = Convert.ToBase64String(byteArray);
            return base64Encoded;
        }

        /// <summary>
        /// Descifrar en base64.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeBase64(string input)
        {
            byte[] byteArray = Convert.FromBase64String(input);
            string decodedString = System.Text.Encoding.UTF8.GetString(byteArray);
            return decodedString;
        }

        /// <summary>
        /// Encripta el texto en AES 16.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptAES(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = new byte[16];

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Desencripta el texto en AES 16.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptAES(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
