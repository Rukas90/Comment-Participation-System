namespace App.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using UnityEngine;

    public static partial class Security
    {
        public const int MinimumPasswordSize = 8;
        public const int MaximumPasswordSize = 32;

        const string PasswordHash = "P@!Hy~50-ax@U$";
        const int pepper = 145447154;

        public const int HashSize = 20;
        public const int SaltSize = 16;

        public static byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];
            RandomNumberGenerator.Create().GetBytes(salt);

            return salt;
        }
        public static byte[] GetByteArray(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public static string HashToBase64(string data, byte[] salt)
        {
            return Convert.ToBase64String(Security.Hash(data, salt));
        }
        public static byte[] Hash(string data, byte[] salt)
        {
            var peppered = Pepper(data);

            var pbkdf2 = new Rfc2898DeriveBytes(peppered, salt, 10000);
            var hash = pbkdf2.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return hashBytes;
        }
        private static string Pepper(string data)
        {
            return Shuffle(data, pepper);
        }
        private static string Shuffle(string data, int pepper)
        {
            char[] chars = data.ToArray();

            System.Random rand = new System.Random(pepper);

            char swap;
            for (int charIdx = chars.Length - 1; charIdx > 0; charIdx--)
            {
                int n = rand.Next(charIdx);

                swap = chars[n];
                chars[n] = chars[charIdx];
                chars[charIdx] = swap;
            }
            return new string(chars);
        }

        public static bool CompareHashes(string hash, string input)
        {
            try
            {
                byte[] h1_bytes = Convert.FromBase64String(hash);

                byte[] salt = new byte[SaltSize];
                Array.Copy(h1_bytes, 0, salt, 0, SaltSize);

                var peppered = Pepper(input);

                var pbkdf2 = new Rfc2898DeriveBytes(peppered, salt, 10000);
                byte[] inputHash = pbkdf2.GetBytes(HashSize);

                for (int i = 0; i < HashSize; i++)
                {
                    if (h1_bytes[i + SaltSize] != inputHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return false;
            }
        }

        public static string Encrypt(string data, byte[] salt)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data + pepper), salt));
        }
        public static string Decrypt(string data, byte[] salt)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(data), salt)).Replace(pepper.ToString(), string.Empty);
        }

        public static byte[] Encrypt(byte[] data, byte[] salt)
        {
            Rfc2898DeriveBytes passbytes =
            new Rfc2898DeriveBytes(PasswordHash, salt);

            MemoryStream stream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128
            };

            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            cryptostream.Write(data, 0, data.Length);
            cryptostream.Close();

            return stream.ToArray();
        }
        public static byte[] Decrypt(byte[] data, byte[] salt)
        {
            Rfc2898DeriveBytes passbytes =
            new Rfc2898DeriveBytes(PasswordHash, salt);

            MemoryStream stream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128
            };

            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write);

            cryptostream.Write(data, 0, data.Length);
            cryptostream.Close();

            return stream.ToArray();
        }
    }
}