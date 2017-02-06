using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.Text.RegularExpressions;

namespace TinyStack.Modules.iUtility.iTools
{
    /// <summary>
    /// 信息验证
    /// </summary>
    public class DataCheck : IDataCheck
    {
        #region 验证是否为手机号码
        /// <summary>
        /// 验证是否为手机号码(13、14、15、18开头的11位手机号码)
        /// </summary>
        /// <param name="telNumber">手机号码(13412345678)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsMobile(string telNumber)
        {
            if (telNumber == null || telNumber == "")
            {
                return false;
            }

            return Regex.IsMatch(telNumber, @"^1[3|4|5|8]\d{9}$", RegexOptions.IgnoreCase); //匹配13、14、15、18开头的11位数字则返回true，否则返回false
        }
        #endregion

        #region 验证是否为身份证号码
        /// <summary>
        /// 验证是否为身份证号码(15或18位数字，或者最后一位是字母x（不区分大小写）的15或18位数字和x的组合)
        /// </summary>
        /// <param name="strIDNumber">身份证号码</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsIDCard(string strIDNumber)
        {
            if (strIDNumber == null || strIDNumber == "")
            {
                return false;
            }
            if (strIDNumber.Length == 18) //若输入的身份证号码为18位
            {
                bool check = IsIDCard18(strIDNumber); //调用验证18位身份证号码的方法并将返回值赋值给check
                return check;
            }
            else if (strIDNumber.Length == 15) //若输入的身份证号码为15位
            {
                bool check = IsIDCard15(strIDNumber); //调用验证15位身份证号码的方法并将返回值赋值给check
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证18位身份证号码
        /// </summary>
        /// <param name="Id">身份证号码</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        private bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-"); //在身份证号码中截取出生日期的字符串
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证15位身份证号码
        /// </summary>
        /// <param name="Id">身份证号码</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        private bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-"); //在身份证号码中截取出生日期的字符串
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        #endregion

        #region 验证是否为电子邮件地址
        /// <summary>
        /// 验证是否为电子邮箱地址(xxx@xxx.xx)
        /// </summary>
        /// <param name="strEmailAddress">电子邮箱地址(xxx@xx.xx)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsEmail(string strEmailAddress)
        {
            if (strEmailAddress == null || strEmailAddress == "")
            {
                return false;
            }

            return Regex.IsMatch(strEmailAddress, @"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", RegexOptions.IgnoreCase); //验证若符合xxx@xx.xx的电子邮箱格式则返回true，否则返回false
        }
        #endregion

        #region 验证是否为邮政编码
        /// <summary>
        /// 验证是否为邮政编码（6位0-9的数字的组合）
        /// </summary>
        /// <param name="strPostCode">邮政编码(100094)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsPostCode(string strPostCode)
        {
            if (strPostCode == null || strPostCode == "")
            {
                return false;
            }
            return Regex.IsMatch(strPostCode, @"^\d{6}$", RegexOptions.IgnoreCase); //验证若符合6位0-9的数字的组合则返回true，否则返回false
        }
        #endregion

        #region 验证是否为日期
        /// <summary>
        /// 验证是否为日期(格式为yyyy-MM-dd HH-mm-ss)
        /// </summary>
        /// <param name="strDateTime">日期(2012-12-07 13:51:24)</param>      
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsDateTime(string strDateTime)
        {
            try
            {
                DateTime time = Convert.ToDateTime(strDateTime); //将传入的日期转换为DateTime格式
                if (time.ToString() == strDateTime) //比较转换后的日期与原日期是否相同
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 验证是否为指定格式的日期
        /// <summary>
        /// 验证是否为指定格式的日期
        /// </summary>
        /// <param name="strDateTime">日期(2012-01-01 09:23:40)</param>
        /// <param name="format">指定的格式(yyyy-MM-dd HH:mm:ss)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsDateTime(string strDateTime, string format)
        {
            string strFormat;
            string strEnd;
            DateTime time;
            string formatDateTime;

            try
            {
                if (format.Contains("pm") || format.Contains("PM") || format.Contains("am") || format.Contains("AM")) //判断格式字符串中是否包含pm、PM、am、AM
                {
                    strFormat = format.Substring(0, format.TrimEnd().Length - 2); //截取格式字符串表示日期和时间的值的部分
                    strEnd = format.Substring(format.TrimEnd().Length - 2); //将日期中表示上下午的英文字符赋值给strEnd
                    time = Convert.ToDateTime(strDateTime); //将日期转换为系统yyyy-MM-dd HH-mm-ss形式
                    formatDateTime = time.ToString(strFormat); //按照格式字符串表示日期和时间的值的部分格式化time
                    formatDateTime = formatDateTime + strEnd; //格式化后的字符串加上表示上午或者下午的英文字符
                }
                else //格式字符串中不包含pm、PM、am、AM
                {
                    time = Convert.ToDateTime(strDateTime); //将日期转换为系统yyyy-MM-dd HH-mm-ss形式
                    formatDateTime = time.ToString(format); //按照格式字符串格式化日期
                }
                if (format.Contains('/')) //判断格式字符串中是否包含'/'
                {
                    formatDateTime = formatDateTime.Replace('-', '/'); //'-'替换为'/'
                }
                if (formatDateTime.Equals(strDateTime)) //比较格式化后的日期是否与源日期相等
                {
                    return true;
                }

                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 验证是否为银行卡号
        /// <summary>
        /// 验证是否为银行卡号（16-19位0-9的数字组合）
        /// </summary>
        /// <param name="strBankCardNum">银行卡号</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsBankCard(string strBankCardNum)
        {
            if (strBankCardNum == null || strBankCardNum == "" || strBankCardNum.Length < 16 || strBankCardNum.Length > 19)
            {
                return false;
            }

            return Regex.IsMatch(strBankCardNum, @"^\d{16,19}$", RegexOptions.IgnoreCase); //验证16到19位的银行卡号格式
        }
        #endregion

        #region 验证是否为金额
        /// <summary>
        /// 验证是否为金额（带两位小数的非负实数，整数部分最多支持1000位，若整数部分大于1位则不能以0开头）
        /// </summary>
        /// <param name="strMoney">金额(100.00)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsMoney(string strMoney)
        {
            if (strMoney == null || strMoney == "")
            {
                return false;
            }

            return Regex.IsMatch(strMoney, @"^([1-9][\d]{0,1000}|0)(\.[\d]{1,2})?$", RegexOptions.IgnoreCase); //验证是否符合金额格式，符合返回true，否则返回false
        }
        #endregion

        #region 验证是否为IP地址
        /// <summary>
        /// 验证是否为IP地址（支持IPv4和IPv6地址格式的验证，IPv6地址支持缩写格式）
        /// </summary>
        /// <param name="strIp">IP地址(192.168.3.120)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsIP(string strIp)
        {
            if (strIp == null || strIp == "")
            {
                return false;
            }

            bool bIPv4 = Regex.IsMatch(strIp.Trim(), @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase); //验证是否为IPv4的IP地址

            bool bIPv6 = Regex.IsMatch(strIp.Trim(), @"：^([\da-fA-F]{1,4}:){6}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^::([\da-fA-F]{1,4}:){0,4}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:):([\da-fA-F]{1,4}:){0,3}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){2}:([\da-fA-F]{1,4}:){0,2}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){3}:([\da-fA-F]{1,4}:){0,1}((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){4}:((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$|^([\da-fA-F]{1,4}:){7}[\da-fA-F]{1,4}$|^:((:[\da-fA-F]{1,4}){1,6}|:)$|^[\da-fA-F]{1,4}:((:[\da-fA-F]{1,4}){1,5}|:)$|^([\da-fA-F]{1,4}:){2}((:[\da-fA-F]{1,4}){1,4}|:)$|^([\da-fA-F]{1,4}:){3}((:[\da-fA-F]{1,4}){1,3}|:)$|^([\da-fA-F]{1,4}:){4}((:[\da-fA-F]{1,4}){1,2}|:)$|^([\da-fA-F]{1,4}:){5}:([\da-fA-F]{1,4})?$|^([\da-fA-F]{1,4}:){6}:$", RegexOptions.IgnoreCase); //验证是否为IPv6的IP地址

            return (bIPv4 || bIPv6);
        }
        #endregion

        #region 验证是否为MAC地址
        /// <summary>
        /// 验证是否为MAC地址（12个16进制数，每2个16进制数之间用‘：’、‘.’或者‘-’隔开）
        /// </summary>
        /// <param name="strMAC">MAC地址(以'-'分割)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsMAC(string strMAC)
        {
            if (strMAC == null || strMAC == "")
            {
                return false;
            }

            return IsMAC(strMAC, '-'); //以默认连接符'-'为参数调用IsMAC(string strMAC, char mark)函数
        }
        #endregion

        #region 验证是否为MAC地址(两个参数)
        /// <summary>
        /// 验证是否为MAC地址（12个16进制数，每2个16进制数之间用“：”、“.”或者“-”隔开）
        /// </summary>
        /// <param name="strMAC">MAC地址(B0-58-7A-11-22-33)</param>
        /// <param name="mark">分隔符字符('-')</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        public bool IsMAC(string strMAC, char mark)
        {
            if (mark != '-' && mark != ':' && mark != '.')
            {
                return false;
            }

            switch (mark) //根据分隔符的不同判断不同的格式
            {
                case '-':
                    {
                        return Regex.IsMatch(strMAC, @"^([0-9a-fA-F]{2})(([/\s:-][0-9a-fA-F]{2}){5})$", RegexOptions.IgnoreCase); //验证以‘-’分隔的格式
                    }
                case '.':
                    {
                        return Regex.IsMatch(strMAC, @"^([0-9a-fA-F]{2})(([/\s:.][0-9a-fA-F]{2}){5})$", RegexOptions.IgnoreCase); //验证以‘.’分隔的格式
                    }
                case ':':
                    {
                        return Regex.IsMatch(strMAC, @"^([0-9a-fA-F]{2})(([/\s::][0-9a-fA-F]{2}){5})$", RegexOptions.IgnoreCase); //验证以‘：’分隔的格式
                    }
                default: //若不是‘-’、‘.’、‘：’中的任一种分隔符则返回false
                    {
                        return false;
                    }
            }
        } 
        #endregion
       
    }
}
