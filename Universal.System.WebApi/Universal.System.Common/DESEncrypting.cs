using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Universal.System.Common
{
    public static class DESEncrypting
    {
        /// <summary>
        /// DEC 加密过程
        /// </summary>
        /// <param name="pToEncrypt">被加密的字符串</param>
        /// <param name="sKey">密钥（只支持8个字节的密钥）</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string encrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider(); //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象

            des.Key = ASCIIEncoding.ASCII.GetBytes(key);　//建立加密对象的密钥和偏移量

            des.IV = ASCIIEncoding.ASCII.GetBytes(key);　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法

            byte[] inputByteArray = Encoding.Default.GetBytes(encrypt);//把字符串放到byte数组中

            using(MemoryStream memoryStream = new MemoryStream())//创建其支持存储区为内存的流　
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write)) //定义将数据流链接到加密转换的流
            {
                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock(); //上面已经完成了把加密后的结果放到内存中去
                StringBuilder ret = new StringBuilder();

                foreach (byte b in memoryStream.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }

        }

        /// <summary>
        ///  DEC 解密过程
        /// </summary>
        /// <param name="pToDecrypt">被解密的字符串</param>
        /// <param name="sKey">密钥（只支持8个字符的密钥，同前面的加密密钥相同）</param>
        /// <returns>返回被解密的字符串</returns>
        public static string Decrypt(string decrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[decrypt.Length / 2];

            for (int x = 0; x < decrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(decrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(key);　//建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);

            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write))
            {

                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);

                cryptoStream.FlushFinalBlock();

                return Encoding.Default.GetString(memoryStream.ToArray());
            }
        }
    }
}
