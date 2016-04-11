using System;
using System.Text.RegularExpressions;

namespace robotjob.Common
{
    public static class TextHandler
    {
        /// <summary>
        /// 密码专用加密方式（MD5）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string PasswordMD5(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return "";
            source = string.Format("%$#&{0}!&", source);
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5").ToLower();
        }

        /// <summary>
        /// .NET的MD5码生成
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GeneralMD5(string source)
        {
            byte[] data = System.Text.Encoding.Unicode.GetBytes(source);
            //创建一个Md5对象
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //加密Byte[]数组
            byte[] result = md5.ComputeHash(data);
            //将加密后的数组转化为字段
            return System.Text.Encoding.Unicode.GetString(result);
        }

        /// <summary>
        /// 检查是否是数字
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool CheckNumber(string Str)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return false;
            else
                return Regex.IsMatch(Str, "^[0-9]{1,8}$");
        }

        /// <summary>
        /// 检查是否是手机号
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool CheckMobile(string Str)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return false;
            return Regex.IsMatch(Str, @"^[1]+[3,5,7,8]+\d{9}");
        }

        /// <summary>
        /// 检查是否是正确的邮箱地址
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool CheckEmail(string Str)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return false;
            return Regex.IsMatch(Str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool CheckIsValid(string input, EnumText type)
        {
            switch (type)
            {
                case EnumText.SpecialCharacter:
                    return checkIsValid(input, @"^[^'&\""]*$");

                case EnumText.PasswordFormat:
                    return checkIsValid(input, @"^[A-Za-z0-9_\-]*$");

                case EnumText.PasswordTooLong:
                    if (input.Length > 50) return false;
                    return true;

                default:
                    break;
            }
            return true;
        }
        private static bool checkIsValid(string Str, string valid)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return false;
            return Regex.IsMatch(Str, valid);
        }
    }
}
