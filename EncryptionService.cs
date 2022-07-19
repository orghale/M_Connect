using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace M_Connect.Encryption
{

    public static class EncryptionService
    {

        public static string EncryptString(this string plainText)
        {
            try
            {
                return Base64Encoding(rsaEncryptionPkcs1(GetKey(), Encoding.UTF8.GetBytes(plainText)));
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static string DecryptString(this string plainText)
        {
            try
            {
                return Base64Encoding(rsaDecryptionPkcs1(GetPrivateKey(), Encoding.UTF8.GetBytes(plainText)));
                //return Base64Encoding(rsaDecryptionOaepSha1(GetPrivateKey(), Encoding.UTF8.GetBytes(plainText)));
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static byte[] rsaEncryptionPkcs1(string publicKeyPem, byte[] plaintext)
        {
            RSA rsaAlg = RSA.Create();
            rsaAlg.ImportFromPem(publicKeyPem);
            var encryptedData = rsaAlg.Encrypt(plaintext, RSAEncryptionPadding.Pkcs1);
            return encryptedData;
        }

        public static byte[] rsaDecryptionPkcs1(string privateKeyPem, byte[] ciphertext)
        {
            RSA rsaAlgd = RSA.Create();
            byte[] privateKeyByte = Base64Decoding(privateKeyPem);
            int _out;
            rsaAlgd.ImportPkcs8PrivateKey(privateKeyByte, out _out);
            var decryptedData = rsaAlgd.Decrypt(ciphertext, RSAEncryptionPadding.Pkcs1);
            return decryptedData;
        }

        public static byte[] rsaEncryptionOaepSha1(string publicKeyXml, byte[] plaintext)
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048);
            RSAalg.PersistKeyInCsp = false;
            RSAalg.FromXmlString(publicKeyXml);
            return RSAalg.Encrypt(plaintext, true);
        }

        public static byte[] rsaDecryptionOaepSha1(string privateKeyXml, byte[] ciphertext)
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048);
            RSAalg.PersistKeyInCsp = false;
            RSAalg.FromXmlString(privateKeyXml);
            return RSAalg.Decrypt(ciphertext, true);
        }

        static string Base64Encoding(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        static byte[] Base64Decoding(String input)
        {
            return Convert.FromBase64String(input);
        }

        public static string GetPrivateKey()
        {
            string sDefaultValue = "";

            try
            {
                var root = AppDomain.CurrentDomain.BaseDirectory; using (var reader = new System.IO.StreamReader(root + @"/keys/private.key"))
                {
                    string Line = reader.ReadToEnd();

                    sDefaultValue = Line.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", ""); ;
                }
            }
            catch { throw; }
            return sDefaultValue;
        }

        public static string GetKey()
        {
            string sDefaultValue = "";

            try
            {
                var root = AppDomain.CurrentDomain.BaseDirectory; using (var reader = new System.IO.StreamReader(root + @"/keys/public.key"))
                {
                    string Line = reader.ReadToEnd();

                    sDefaultValue = Line;//.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "").Trim();
                }
            }
            catch { throw; }
            return sDefaultValue;
        }

        //public static string GetKey()
        //{
        //    string sDefaultValue = "";

        //    try
        //    {
        //        var sIniFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"/keys/public.key");

        //        if (File.Exists(sIniFile))
        //        {
        //            string Line = null;
        //            StreamReader sr = new StreamReader(sIniFile, Encoding.Default);
        //            while (sr.Peek() >= 0)
        //            {
        //                Line = sr.ReadToEnd().Trim();

        //                //Line = Line.Replace("-----BEGIN PUBLIC KEY-----", "");
        //                //Line = Line.Replace("-----END PUBLIC KEY-----", "").Replace("\n", "");

        //                sDefaultValue = Line;
        //            }
        //            sr.Close();
        //        }
        //    }
        //    catch { throw; }
        //    return sDefaultValue;
        //}



    }
}
