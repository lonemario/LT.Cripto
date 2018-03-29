using LT.Cripto.Helper;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LT.Cripto
{
    public class CriptoHelper
    {
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        // private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm
        private const int keysize = 256;
        //private string _InitVector;
        private byte[] initVectorBytes;
        private RijndaelManaged symmetricKey;

        /// <summary>
        /// This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        /// 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        /// </summary>
        /// <param name="InitVector"></param>
        public CriptoHelper(string InitVector)
        {
            if (InitVector.Trim().Length != 16)
                throw new Exception("The Init Vector must be 16 characters long");

            if (!TextHelper.IsUnicode(InitVector))
                throw new Exception("The Init Vector contains not valid character");

            //_InitVector = InitVector;

            initVectorBytes = Encoding.UTF8.GetBytes(InitVector);

            symmetricKey = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public string EncryptString(string plainText, string passPhrase)
        {
            if (passPhrase.Trim().Length != 16)
                throw new Exception("The passPhrase must be 16 characters long");

            //if (!TextHelper.IsUnicode(passPhrase))
            //    throw new Exception("The passPhrase contains not valid character");

            //passPhrase = NormalizePassPhrase(passPhrase);
            //byte[] initVectorBytes = Encoding.UTF8.GetBytes(_InitVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public string DecryptString(string cipherText, string passPhrase)
        {
            if (passPhrase.Trim().Length != 16)
                throw new Exception("The passPhrase must be 16 characters long");

            //if (!TextHelper.IsUnicode(passPhrase))
            //    throw new Exception("The passPhrase contains not valid character");

            //passPhrase = NormalizePassPhrase(passPhrase);
            //byte[] initVectorBytes = Encoding.ASCII.GetBytes(_InitVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            //RijndaelManaged symmetricKey = new RijndaelManaged();
            //symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        //private static string NormalizePassPhrase(string passPhrase, string fillerPhrase)
        //{
        //    if (passPhrase.Length > 16)
        //    {
        //        return passPhrase.Substring(0, 16);
        //    }
        //    else
        //    {
        //        if (passPhrase.Length == 16)
        //        {
        //            return passPhrase;
        //        }
        //        else
        //        {
        //            return (passPhrase + "****************").Substring(0, 16);
        //        }
        //    }
        //}
    }
}
