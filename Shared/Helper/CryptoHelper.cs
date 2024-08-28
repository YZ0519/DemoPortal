using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helper
{
    public class CryptoHelper
    {
        public static string DecryptTextWithSecretKey(string input, string secretKey)
        {
            byte[] numArray = Convert.FromBase64String(input);
            byte[] bytes = Encoding.UTF8.GetBytes(secretKey);
            bytes = SHA256.Create().ComputeHash(bytes);
            byte[] numArray1 = AESDecrypt(numArray, bytes);
            return Encoding.UTF8.GetString(numArray1);
        }

        public static string EncryptTextWithSecretKey(string input, string secretKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] numArray = Encoding.UTF8.GetBytes(secretKey);
            numArray = SHA256.Create().ComputeHash(numArray);
            return Convert.ToBase64String(AESEncrypt(bytes, numArray));
        }
        private static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] array = null;
            byte[] numArray = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                try
                {
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;
                    Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(passwordBytes, numArray, 1000);
                    rijndaelManaged.Key = rfc2898DeriveByte.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = rfc2898DeriveByte.GetBytes(rijndaelManaged.BlockSize / 8);
                    rijndaelManaged.Mode = CipherMode.CBC;
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write);
                    try
                    {
                        cryptoStream.Write(bytesToBeDecrypted, 0, (int)bytesToBeDecrypted.Length);
                        cryptoStream.Close();
                    }
                    finally
                    {
                        if (cryptoStream != null)
                        {
                            ((IDisposable)cryptoStream).Dispose();
                        }
                    }
                    array = memoryStream.ToArray();
                }
                finally
                {
                    if (rijndaelManaged != null)
                    {
                        ((IDisposable)rijndaelManaged).Dispose();
                    }
                }
            }
            finally
            {
                if (memoryStream != null)
                {
                    ((IDisposable)memoryStream).Dispose();
                }
            }
            return array;
        }

        private static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] array = null;
            byte[] numArray = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                try
                {
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;
                    Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(passwordBytes, numArray, 1000);
                    rijndaelManaged.Key = rfc2898DeriveByte.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = rfc2898DeriveByte.GetBytes(rijndaelManaged.BlockSize / 8);
                    rijndaelManaged.Mode = CipherMode.CBC;
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write);
                    try
                    {
                        cryptoStream.Write(bytesToBeEncrypted, 0, (int)bytesToBeEncrypted.Length);
                        cryptoStream.Close();
                    }
                    finally
                    {
                        if (cryptoStream != null)
                        {
                            ((IDisposable)cryptoStream).Dispose();
                        }
                    }
                    array = memoryStream.ToArray();
                }
                finally
                {
                    if (rijndaelManaged != null)
                    {
                        ((IDisposable)rijndaelManaged).Dispose();
                    }
                }
            }
            finally
            {
                if (memoryStream != null)
                {
                    ((IDisposable)memoryStream).Dispose();
                }
            }
            return array;
        }
    }
}
