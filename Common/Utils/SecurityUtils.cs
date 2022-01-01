using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Library.Utils
{
    /// <summary>
    /// Security utils class for Campaigner
    /// </summary>
    public static class SecurityUtils
    {
        /// <summary>
        /// Method to encrypt the text.
        /// </summary>
        /// <param name="textToEncrypt">Text to encrypt</param>
        /// <param name="encryptionPwd">Encryption password to use.</param>
        /// <returns>Encrypted text</returns>
        public static string EncryptText(string textToEncrypt, string encryptionPwd)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(encryptionPwd);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        /// <summary>
        /// Method to decrypt the text.
        /// </summary>
        /// <param name="textToDecrypt">Text to decrypt</param>
        /// <param name="decryptionPwd">Decryption password to use. Has to be same as encryption password.</param>
        /// <returns>Decrypted text</returns>
        public static string DecryptText(string textToDecrypt, string decryptionPwd)
        {

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(decryptionPwd);
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
    }
}
