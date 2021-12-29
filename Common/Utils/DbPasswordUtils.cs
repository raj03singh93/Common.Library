using System;
using System.Security.Cryptography;
using System.Text;

namespace Campaigner.Core.CommonLib.Utils
{
    /// <summary>
    /// Class to decrypt password and generate connectionstring.
    /// </summary>
    public static class DbPasswordUtils
    {

        /// <summary>
        /// Decrypt db user name password
        /// </summary>
        /// <param name="textToDecrypt">Text to decryptTe</param>
        private static string DecryptDbUsernamePassword(string textToDecrypt)
        {
            try
            {
                string dbUsernamePwdKey = "PatanahiHai";

                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;

                rijndaelCipher.KeySize = 0x80;
                rijndaelCipher.BlockSize = 0x80;
                byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
                byte[] pwdBytes = Encoding.UTF8.GetBytes(dbUsernamePwdKey);
                byte[] keyBytes = new byte[0x10];
                int len = pwdBytes.Length;
                if (len > keyBytes.Length)
                {
                    len = keyBytes.Length;
                }

                Array.Copy(pwdBytes, keyBytes, len);
                rijndaelCipher.Key = keyBytes;
                rijndaelCipher.IV = keyBytes;
                byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                return Encoding.UTF8.GetString(plainText);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generate connection string from password.
        /// </summary>
        /// <param name="connectionString">Db connectionstring without password</param>
        /// <param name="password">Db password aes encrypted</param>
        /// <returns>Returns proper connectionstring with decrypted password.</returns>
        public static string GenerateConnectionString(string connectionString, string password)
        {
            return DecryptConnectionString(connectionString, password);
        }

        /// <summary>
        /// Clean connectionstring from user password
        /// </summary>
        private static string CleanConnectionStringFromUserPassword(string connStr)
        {
            string newConnectionString = string.Empty;
            string[] split = connStr.Split(new char[] { ';' });

            foreach (string tmpstr in split)
            {
                string UpperStr = tmpstr.ToUpper();

                if (UpperStr.StartsWith("USER ID") || UpperStr.StartsWith("PASSWORD"))
                {
                    continue;
                }

                newConnectionString += ";" + tmpstr;
            }

            return newConnectionString;
        }

        /// <summary>
        /// Decrypt connection string
        /// </summary>
        private static string DecryptConnectionString(string connStr, string encryptedPassword)
        {
            try
            {
                string retStr = connStr;

                if (!string.IsNullOrEmpty(encryptedPassword))
                {
                    retStr = DecryptDbUsernamePassword(encryptedPassword);
                    retStr += CleanConnectionStringFromUserPassword(connStr);
                }

                return retStr;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
