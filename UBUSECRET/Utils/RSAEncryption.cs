using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public class RSAEncryption
    {

        private readonly UnicodeEncoding ByteConverter = new UnicodeEncoding();
        private readonly RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

        public RSAEncryption()
        {

        }

        private byte[] Encrypt(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private byte[] Decrypt(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public byte[] EncryptText(string text)
        {
            byte[] plaintext = ByteConverter.GetBytes(text);
            byte[] encryptedtext = this.Encrypt(plaintext, RSA.ExportParameters(false), false);
            return encryptedtext;
        }

        public string DecryptText(byte[] encryptedtext)
        {

            byte[] decryptedtext = this.Decrypt(encryptedtext, RSA.ExportParameters(true), false);
            string plaintext = ByteConverter.GetString(decryptedtext);
            return plaintext;
        }
    }
}
