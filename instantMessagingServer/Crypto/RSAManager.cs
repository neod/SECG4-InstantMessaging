using System;
using System.Security.Cryptography;
using System.Text;

namespace Crypto
{
    public class RSAManager
    {
        private readonly RSAEncryptionPadding EncryptionPadding = RSAEncryptionPadding.Pkcs1;
        private readonly RSASignaturePadding SignaturPadding = RSASignaturePadding.Pkcs1;
        private readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA512;

        private RSA Rsa { get; set; }

        public string GetKey(bool withPrivateKey)
        {
            return Rsa.ToXmlString(withPrivateKey);
        }

        public RSAManager(string key)
        {
            Rsa = RSA.Create();
            Rsa.FromXmlString(key);
        }

        #region Encrypt

        public byte[] Encrypt(byte[] data)
        {
            return Rsa.Encrypt(data, EncryptionPadding);
        }

        public byte[] Encrypt(string data)
        {
            return Encrypt(Encoding.UTF8.GetBytes(data));
        }

        public string EncryptToBase64(byte[] data)
        {
            return Convert.ToBase64String(Encrypt(data));
        }

        public string EncryptToBase64(string data)
        {
            return Convert.ToBase64String(Encrypt(data));
        }

        #endregion Encrypt

        #region Decrypt

        public byte[] Decrypt(byte[] data)
        {
            return Rsa.Decrypt(data, EncryptionPadding);
        }
        public byte[] DecryptFromBase64(string data)
        {
            return Decrypt(Convert.FromBase64String(data));
        }

        public string DecryptToString(byte[] data)
        {
            return Encoding.UTF8.GetString(Decrypt(data));
        }
        public string DecryptFromBase64ToString(string data)
        {
            return Encoding.UTF8.GetString(DecryptFromBase64(data));
        }

        #endregion Decrypt

        #region Sign

        public byte[] Sign(byte[] data)
        {
            return Rsa.SignData(data, hashAlgorithmName, SignaturPadding);
        }

        public byte[] Sign(string data)
        {
            return Sign(Encoding.UTF8.GetBytes(data));
        }

        public string SignToBase64(byte[] data)
        {
            return Convert.ToBase64String(Sign(data));
        }

        public string SignToBase64(string data)
        {
            return Convert.ToBase64String(Sign(Encoding.UTF8.GetBytes(data)));
        }

        #endregion Sign

    }
}
