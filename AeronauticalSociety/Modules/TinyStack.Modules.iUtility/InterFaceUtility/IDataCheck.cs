using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 数据验证接口
    /// </summary>
    interface IDataCheck
    {
        /// <summary>
        /// 验证是否为手机号码(13、14、15、18开头的11位手机号码)
        /// </summary>
        /// <param name="telNumber">手机号码(13412345678)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsMobile(string telNumber);

        /// <summary>
        /// 验证是否为身份证号码(15或18位数字，或者最后一位是字母x（不区分大小写）的15或18位数字和x的组合)
        /// </summary>
        /// <param name="strIDNumber">身份证号码</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsIDCard(string strIDNumber);

        /// <summary>
        /// 验证是否为电子邮箱地址(xxx@xxx.xx)
        /// </summary>
        /// <param name="strEmailAddress">电子邮箱地址(xxx@xx.xx)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsEmail(string strEmailAddress);

        /// <summary>
        /// 验证是否为邮政编码（6位0-9的数字的组合）
        /// </summary>
        /// <param name="strPostCode">邮政编码(100094)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsPostCode(string strPostCode);

        /// <summary>
        /// 验证是否为日期(格式为yyyy-MM-dd HH-mm-ss)
        /// </summary>
        /// <param name="strDateTime">日期(2012-12-07 13:51:24)</param>      
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsDateTime(string strDateTime);

        /// <summary>
        /// 验证是否为指定格式的日期
        /// </summary>
        /// <param name="strDateTime">日期(2012-01-01 09:23:40)</param>
        /// <param name="format">指定的格式(yyyy-MM-dd HH:mm:ss)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsDateTime(string strDateTime, string format);

        /// <summary>
        /// 验证是否为银行卡号（16-19位0-9的数字组合）
        /// </summary>
        /// <param name="strBankCardNum">银行卡号</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsBankCard(string strBankCardNum);

        /// <summary>
        /// 验证是否为金额（非负带两位小数的实数，整数部分最多支持1000位）
        /// </summary>
        /// <param name="strMoney">金额(100.00)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsMoney(string strMoney);

        /// <summary>
        /// 验证是否为IP地址（支持IPv4和IPv6地址格式的验证，IPv6地址支持缩写格式）
        /// </summary>
        /// <param name="strIp">IP地址(192.168.3.120)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsIP(string strIp);

        /// <summary>
        /// 验证是否为MAC地址（12个16进制数，每2个16进制数之间用‘：’、‘.’或者‘-’隔开）
        /// </summary>
        /// <param name="strMAC">MAC地址(以'-'分割)</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsMAC(string strMAC);

        /// <summary>
        /// 验证是否为MAC地址（12个16进制数，每2个16进制数之间用“：”、“.”或者“-”隔开）
        /// </summary>
        /// <param name="strMAC">MAC地址(B0-58-7A-11-22-33)</param>
        /// <param name="mark">分隔符字符('-')</param>
        /// <returns>验证结果(true验证通过，false验证不通过)</returns>
        bool IsMAC(string strMAC, char mark);
    }
}
