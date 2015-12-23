using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EVERGRANDE
{
    public class RegexUtil
    {
        /// <summary>
        /// 判断是否为整数的正则表达式
        /// </summary>
        public static bool IsInteger(string value)
        {
            string regexString = "^[0-9]+$";
            Regex reg = new Regex(regexString);

            bool result = reg.IsMatch(value);

            return result;
        }

        /// <summary>
        /// 判断是否为正整数的正则表达式
        /// </summary>
        public static bool IsPositiveInteger(string value)
        {
            string regexString = "^[1-9][0-9]*$";
            Regex reg = new Regex(regexString);

            bool result = reg.IsMatch(value);

            return result;
        }

        ///// <summary>
        ///// 判断是否是数字
        ///// </summary>
        //public static bool IsNumber(string value)
        //{
        //    string regexString = "^[0-9.]+$";
        //    Regex reg = new Regex(regexString);

        //    bool result = reg.IsMatch(value);

        //    return result;
        //}

        /// <summary>
        /// 校验桁架号正则表达式
        /// </summary>
        public static string ProductSerialNoRegularExpression = @"^\w{1,}\d{0,}-\d{1,}-\d{1,}$";

        /// <summary>
        /// 判断是否符合桁架号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsProductSerialNo(string value)
        {
            Regex reg = new Regex(RegexUtil.ProductSerialNoRegularExpression);

            bool result = reg.IsMatch(value);

            return result;
        }

        /// <summary>
        /// 判断是否符合时间 （格式为 HH:mm）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTimeFormat1(string value)
        {
            string regexString = @"^([0-1][0-9]|[2][0-3]):([0-5][0-9])$";
            Regex reg = new Regex(regexString);

            bool result = reg.IsMatch(value);

            return result;
        }

        /// <summary>
        /// 判断是否符合时间 （格式为 HHmm）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTimeFormat2(string value)
        {
            string regexString = @"^([0-1][0-9]|[2][0-3])([0-5][0-9])$";
            Regex reg = new Regex(regexString);

            bool result = reg.IsMatch(value);

            return result;
        }

    }
}
