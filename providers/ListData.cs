
using System;
using System.Collections.Generic;

namespace blazor3.providers
{
    public class ListData : IListData
    {
        public List<string> lista { get; set; } = new List<string>();

        private byte[] base64tosArrayByte(string strbase64)
        {

            byte[] decodedBytes = Convert.FromBase64String(strbase64);
            return decodedBytes;
        }
        public string Decrypt(string plaintext, string strkey)
        {
            try
            {
                string strResult = "";
                if (strkey.Length > 16)
                    strkey = strkey.Substring(0, 16);
                string straux = "ADOTRMDWOD1QWELK";

                byte[] key = System.Text.Encoding.UTF8.GetBytes(strkey);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(straux);
                byte[] encryptedText = base64tosArrayByte(plaintext);

                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.IV = IV;
                    aes.Key = key;

                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var ms = new System.IO.MemoryStream(encryptedText))
                    {
                        using (var csDecrypt = new System.Security.Cryptography.CryptoStream(ms, decryptor,
                        System.Security.Cryptography.CryptoStreamMode.Read))
                        {
                            using (var swDecrypt = new System.IO.StreamReader(csDecrypt))
                            {
                                strResult = swDecrypt.ReadToEnd();
                                Console.WriteLine("decrypt : " + strResult);
                            }
                        }
                    }
                }
                return strResult;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }

        public string Encrypt(string plaintext, string strkey)
        {
            try
            {
                string strResult = "";
                if (strkey.Length > 16)
                    strkey = strkey.Substring(0, 16);
                string straux = "ADOTRMDWOD1QWELK";

                byte[] key = System.Text.Encoding.UTF8.GetBytes(strkey);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(straux);
                byte[] encrypted;
                
                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.IV = IV;
                    aes.Key = key;
                    var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (var ms = new System.IO.MemoryStream())
                    {
                        using (var csEncrypt = new System.Security.Cryptography.CryptoStream(ms, encryptor,
                        System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plaintext);
                            }
                            encrypted = ms.ToArray();
                            this.lista = new List<string>();
                            strResult = Convert.ToBase64String(encrypted);
                        }
                    }
                }
                return strResult;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
