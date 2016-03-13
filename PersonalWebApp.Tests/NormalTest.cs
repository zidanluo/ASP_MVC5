using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalWebApp;
using PersonalWebApp.Controllers;
using System.Security.Cryptography;
using System.IO;
using log4net;
using PersonalWebApp.Data;

namespace PersonalWebApp.Tests.Controllers
{
    [TestClass]
    public class NormalTest
    {
        public static ILog log = LogManager.GetLogger(typeof(NormalTest));

        [TestMethod]
        public void GeneralTest()
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                string start = "160305_100000";
                string end = "160305_230000";
                DateTime starttime = DateTime.ParseExact(start, "yyMMdd_HHmmss", System.Globalization.CultureInfo.CurrentCulture);
                DateTime endtime = DateTime.ParseExact(end, "yyMMdd_HHmmss", System.Globalization.CultureInfo.CurrentCulture);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                for(int i=0;i<200;i++)
                {
                    char c = (char)i;
                    log.Info(string.Format("{0} -- {1}",i,c));
                }
                string original = "201512";

                // Create a new instance of the AesCryptoServiceProvider
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (AesCryptoServiceProvider myAes = new AesCryptoServiceProvider())
                {

                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);


                    long ss = BitConverter.ToInt64(encrypted,0);

                    string s = System.Convert.ToBase64String(encrypted);

                    byte[] b = Convert.FromBase64String(s);

                    bool f = b == encrypted;

                    string target = DecryptStringFromBytes_Aes(b, myAes.Key, myAes.IV);

                    // Decrypt the bytes to a string.
                    string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                    //Display the original data and the decrypted data.
                    Console.WriteLine("Original:   {0}", original);
                    Console.WriteLine("Round Trip: {0}", roundtrip);
                    ;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}
