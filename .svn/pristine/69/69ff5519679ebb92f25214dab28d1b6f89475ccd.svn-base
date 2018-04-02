using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AU.Common.Utility
{
    public class CryptoHelp
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        public static string GetMD5(string myString)
        {
            string byte2String = null;
            byte2String = ToGetMD5(myString) + "f";
            byte2String = ToGetMD5(byte2String);
            return byte2String;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        public static string ToGetMD5(string myString)
        {

            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = null;
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                targetData = md5.ComputeHash(fromData);
            }
            catch
            {
                targetData = new byte[fromData.Length];
                fromData.CopyTo(targetData, 0);
            }
            string byte2String = string.Empty;
            for (int i = 0; i < targetData.Length && i < 32; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        }
        /// <summary>
        /// 解密方法  
        /// </summary>
        /// <param name="pToDecrypt">密文</param>
        /// <param name="sKey">密钥</param>
        /// <returns>明文</returns>
        public static string DesDecrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Put  the  input  string  into  the  byte  array  
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改  
            des.Key = ASCIIEncoding.ASCII.GetBytes(GetMD5(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(GetMD5(sKey).Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream  
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
