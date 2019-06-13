using System;
using System.Security.Cryptography;
using System.Text;

namespace Universal.System.Common
{
    public static class MD5Encrypting
    {
        /// <summary>
        /// 16位MD5加密 使用ComputeHash方法,适合用于计算简单的字符串的md5值时
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string value)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

            return BitConverter.ToString(MD5.ComputeHash(Encoding.Default.GetBytes(value)), 4, 8);
        }
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            MD5 md5 = MD5.Create(); //实例化一个md5对像    

            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));  // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　

            for (int i = 0; i < bytes.Length; i++)   // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            {
                stringBuilder.Append(bytes[i].ToString("X")); // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string value)
        {
            MD5 md5 = MD5.Create(); //实例化一个md5对像      

            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));// 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　

            return Convert.ToBase64String(bytes);
        }
    }
}
