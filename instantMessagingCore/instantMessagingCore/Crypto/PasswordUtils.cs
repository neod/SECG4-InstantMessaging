using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace instantMessagingCore.Crypto
{
    public class PasswordUtils
    {
        private static UTF8Encoding encoding = new UTF8Encoding();

        public static string hashAndSalt(string password, string salt) => sha256(password + salt);

        public static string sha256(string value)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(encoding.GetBytes(value));
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }

        public static string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }

        private static readonly char[] alphabetLower = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly char[] alphabetUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] numbers = "0123456789".ToCharArray();
        private static readonly char[] symboles = "&é\"'(-è_çà)=^$ù*<,;:!~#{[|`\\^@]}¤¨£%µ>?./§".ToCharArray();

        public static bool CheckPolicy(String username, String password)
        {
            bool result = password.Length >= 8 &&
                password.Length <= 255 &&
                !password.Contains(username) &&
                password.Any(c => alphabetLower.Contains(c)) &&
                password.Any(c => alphabetUpper.Contains(c)) &&
                password.Any(c => numbers.Contains(c)) &&
                password.Any(c => symboles.Contains(c));

            username = null;
            password = null;

            return result;
        }

    }
}
