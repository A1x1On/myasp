using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace MedCardWeb.Areas._Public.Models
{

    public class Crypt
    {


        public static string GetString(byte[] data)
        {
            var results = new StringBuilder();
            foreach (byte b in data)
            {
                results.Append(b.ToString("X2"));
            }

            return results.ToString();
        }

        public static byte[] GetBytes(string data)
        {
            // GetString encodes the hex-numbers with two digits
            var results = new byte[data.Length / 2];

            for (int i = 0; i < data.Length; i += 2)
            {
                results[i / 2] = Convert.ToByte(data.Substring(i, 2), 16);
            }

            return results;
        }




        public static byte[] Encrypt(byte[] data, string password)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateEncryptor(
                (new PasswordDeriveBytes(password, null)).GetBytes(16),
                new byte[16]);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        public static string Encrypt(string data, string password)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data), password));
        }

        static public byte[] Decrypt(byte[] data, string password)
        {
            BinaryReader br = new BinaryReader(InternalDecrypt(data, password));
            return br.ReadBytes((int)br.BaseStream.Length);
        }

        static public string Decrypt(string data, string password)
        {
            CryptoStream cs = InternalDecrypt(Convert.FromBase64String(data), password);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        static CryptoStream InternalDecrypt(byte[] data, string password)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateDecryptor(
                (new PasswordDeriveBytes(password, null)).GetBytes(16),
                new byte[16]);

            MemoryStream ms = new MemoryStream(data);
            return new CryptoStream(ms, ct, CryptoStreamMode.Read);
        }

      
    }
    






}