using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.InterFaceUtility;

namespace TinyStack.Modules.iUtility.iTools
{
    /// <summary>
    /// 字符串处理
    /// </summary>
    public class StringOperate : IStringOperate
    {
        #region 将字符串中的所有字符转换为大写字符
        /// <summary>
        /// 将字符串中的所有字符转换为大写字符
        /// </summary>
        /// <param name="source">源字符串("hello")</param>
        /// <returns>大写字符串("HELLO")</returns>
        public string ToUpper(string source)
        {
            if (source == null)
            {
                return null;
            }

            return source.ToUpper();
        }
        #endregion

        #region 将字符串中的所有字符转换为小写字符
        /// <summary>
        /// 将字符串中的所有字符转换为小写字符
        /// </summary>
        /// <param name="source">源字符串("HelLo")</param>
        /// <returns>小写字符串("hello")</returns>
        public string ToLower(string source)
        {
            if (source == null)
            {
                return null;
            }

            return source.ToLower();
        }
        #endregion

        #region 同时移除字符串开始位置和结束位置的指定字符
        /// <summary>
        /// 同时移除字符串开始位置和结束位置的指定字符
        /// </summary>
        /// <param name="source">源字符串("abca")</param>
        /// <param name="chDelete">指定要移除的字符('a')</param>
        /// <returns>开始位置和结束位置都移除指定字符后的字符串("bc")</returns>
        public string Trim(string source, char chDelete)
        {
            if (source == null) //判断源字符串是否为null
            {
                return null;
            }
            else
            {
                source = source.TrimStart(chDelete); //调用string类的TrimStart()方法移除字符串开始位置的指定字符并赋值给source
                source = source.TrimEnd(chDelete); //调用string类的TrimEnd()方法移除字符串结束位置的指定字符并赋值给source
                return source; //返回source
            }
        }
        #endregion

        #region 移除字符串开始位置的指定字符
        /// <summary>
        /// 移除字符串开始位置的指定字符
        /// </summary>
        /// <param name="source">源字符串("abca")</param>
        /// <param name="chDelete">指定要移除的字符('a')</param>
        /// <returns>开始位置移除了指定字符的字符串("bca")</returns>
        public string TrimStart(string source, char chDelete)
        {
            if (source == null) //判断源字符串是否为null
            {
                return null;
            }

            return source.TrimStart(chDelete); //调用string类的TrimStart()函数移除字符串开始位置的指定字符并返回移除字符后的字符串
        }
        #endregion

        #region 移除字符串结束位置的指定字符
        /// <summary>
        /// 移除字符串结束位置的指定字符
        /// </summary>
        /// <param name="source">源字符串("abca")</param>
        /// <param name="chDelete">指定要移除的字符('a')</param>
        /// <returns>结束位置移除了指定字符的字符串("abc")</returns>
        public string TrimEnd(string source, char chDelete)
        {
            if (source == null) //判断源字符串是否为null
            {
                return null;
            }

            return source.TrimEnd(chDelete); //调用string类的TrimEnd()函数移除字符串结束位置的指定字符并返回移除字符后的字符串
        }
        #endregion

        #region 将字符串中指定字符替换为其他字符
        /// <summary>
        /// 将字符串中指定字符替换为其他字符
        /// </summary>
        /// <param name="source">源字符串("12355")</param>
        /// <param name="chTarget">要替换掉的字符('5')</param>
        /// <param name="chReplace">用来替换的字符('4')</param>
        /// <returns>替换后的字符串("12344")</returns>
        public string Replace(string source, char chTarget, char chReplace)
        {
            if (source == null || source == "")
            {
                return null;
            }

            return source.Replace(chTarget, chReplace);
        }
        #endregion

        #region 从字符串开始位置截取指定长度的字符串
        /// <summary>
        /// 从字符串开始位置截取指定长度的字符串
        /// </summary>
        /// <param name="source">源字符串("1234abcd")</param>
        /// <param name="length">截取长度(6)</param>
        /// <returns>截取的指定长度的字符串("1234ab")</returns>
        public string Substring(string source, int length)
        {
            if (source == null || source == "" || length < 0)
            {
                return null;
            }
            if (length > source.Length)
            {
                return null;
            }
            return source.Substring(0, length); //防止越界            
        } 
        #endregion

        #region 从字符串指定位置开始截取指定长度的字符串
        /// <summary>
        /// 从字符串指定位置开始截取指定长度的字符串
        /// </summary>
        /// <param name="source">源字符串("12345678")</param>
        /// <param name="index">截取位置索引(2)</param>
        /// <param name="length">截取长度(5)</param>
        /// <returns>截取的字符串("34567")</returns>
        public string Substring(string source, int index, int length)
        {
            if (source == null || source == "")
            {
                return null;
            }
            try
            {
                return source.Substring(index, length); //防止越界
            }
            catch (Exception)
            {
                return null;
            }
        } 
        #endregion

        #region 从字符串开始位置开始检索，返回查找字符的第一次出现的索引
        /// <summary>
        /// 从字符串开始位置开始检索，返回查找字符的第一次出现的索引
        /// </summary>
        /// <param name="source">源字符串("12345635678")</param>
        /// <param name="chTarget">要查找的字符("3")</param>
        /// <returns>返回位置索引(-1表示不存在要查找的字符)</returns>
        public int IndexOf(string source, char chTarget)
        {
            if (source == null || source == "")
            {
                return -1;
            }

            return source.IndexOf(chTarget);
        } 
        #endregion

        #region 从字符串开始位置开始检索，返回查找字符串的第一次出现的索引
        /// <summary>
        /// 从字符串开始位置开始检索，返回查找字符串的第一次出现的索引
        /// </summary>
        /// <param name="source">源字符串("123456")</param>
        /// <param name="strTarget">要查找的字符串("2")</param>
        /// <returns>返回位置索引(-1表示不存在要查找的字符串)</returns>
        public int IndexOf(string source, string strTarget)
        {
            if (source == null || source == "" || strTarget == null)
            {
                return -1;
            }

            return source.IndexOf(strTarget);
        } 
        #endregion

        #region 从字符串指定位置开始检索，返回查找字符的第一次出现的索引
        /// <summary>
        /// 从字符串指定位置开始检索，返回查找字符的第一次出现的索引
        /// </summary>
        /// <param name="source">源字符串("12345635678")</param>
        /// <param name="index">查找位置索引(3)</param>
        /// <param name="chTarget">要查找的字符串("3")</param>
        /// <returns>返回位置索引(-1表示不存在要查找的字符)</returns>
        public int IndexOf(string source, int index, char chTarget)
        {
            if (source == null || source == "")
            {
                return -1;
            }

            return source.IndexOf(chTarget, index);
        } 
        #endregion

        #region 从字符串指定位置开始检索，返回查找字符串的第一次出现的索引
        /// <summary>
        /// 从字符串指定位置开始检索，返回查找字符串的第一次出现的索引
        /// </summary>
        /// <param name="source">原字符串("12345635678")</param>
        /// <param name="index">查找位置索引(5)</param>
        /// <param name="strTarget">要查找的字符串("56")</param>
        /// <returns>返回位置索引(-1表示不存在要查找的字符串)</returns>
        public int IndexOf(string source, int index, string strTarget)
        {
            if (source == null || source == "" || strTarget == null)
            {
                return -1;
            }

            return source.IndexOf(strTarget, index);
        } 
        #endregion

        #region 判断字符串中是否包含指定字符串
        /// <summary>
        /// 判断字符串中是否包含指定字符串
        /// </summary>
        /// <param name="source">源字符串("abc234")</param>
        /// <param name="strTarget">指定字符串("c2")</param>
        /// <returns>判断结果(true(包含)，false(不包含))</returns>
        public bool Contains(string source, string strTarget)
        {
            if (source == null || source == "")
            {
                return false;
            }

            return source.Contains(strTarget);
        } 
        #endregion

        #region 判断字符串为空或者为null
        /// <summary>
        /// 判断字符串为空或者为null
        /// </summary>
        /// <param name="source">源字符串(null)</param>
        /// <returns>判断结果(true(为空或者为null)，false(不为空或者null))</returns>
        public bool IsNullOrEmpty(string source)
        {
            if (source != null)
            {
                return (source.Length == 0); //返回字符串的长度是否等于0的结果
            }

            return true; //若字符串等于null直接返回true
        }
        #endregion

        #region 比较两个字符串是否相同
        /// <summary>
        /// 比较两个字符串是否相同
        /// </summary>
        /// <param name="strA">字符串("abc")</param>
        /// <param name="strB">字符串("abc")</param>
        /// <returns>true(相同)，false(不相同)</returns>
        public bool Compare(string strA, string strB)
        {
            if (string.Compare(strA, strB) == 0) //判断string.Compare(strA, strB)的值是否等于0
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 将指定的字符串列表合并为一个字符串
        /// <summary>
        /// 将指定的字符串列表合并为一个字符串
        /// </summary>
        /// <param name="stringList">字符串列表({"我","和","你"})</param>
        /// <returns>合并后的字符串("我和你")</returns>
        public string Join(IList<string> stringList)
        {

            if (stringList == null)
            {
                return null;
            }

            return string.Concat(stringList);

        } 
        #endregion

        #region 将指定数组中的内容合并成一个字符串
        /// <summary>
        /// 将指定数组中的内容合并成一个字符串
        /// </summary>
        /// <param name="args">数组({"hello","world"})</param>
        /// <returns>合并后的字符串("helloworld")</returns>
        public string Concat(string[] args)
        {
            if (args == null)
            {
                return null;
            }

            return string.Concat(args);
        } 
        #endregion

        #region 将指定数组中的内容合并成一个字符串，并且将指定的字符插入到字符串中
        /// <summary>
        /// 将指定数组中的内容合并成一个字符串，并且将指定的字符插入到字符串中
        /// </summary>
        /// <param name="args">数组{"hello","world"}</param>
        /// <param name="index">位置索引(5)</param>
        /// <param name="chInser">要插入的字符(',')</param>
        /// <returns>字符串("hello,world")</returns>
        public string Concat(string[] args, int index, char chInser)
        {
            if (args == null)
            {
                return null;
            }
            if (index < 0 || index > args.ToString().Length) //判断下标是否越界
            {
                return null;
            }
            string strConcat = string.Concat(args); //调用string类的Concat()函数合并字符串数组的内容

            return strConcat.Insert(index, chInser.ToString()); //调用Insert()函数将指定字符插入指定位置
        } 
        #endregion

        #region 按照指定模板格式化字符串
        /// <summary>
        /// 按照指定模板格式化字符串
        /// </summary>
        /// <param name="format">格式化模板("{0} world")</param>
        /// <param name="args">要格式化的对象({"hello"})</param>
        /// <returns>格式化后的字符串("hello world")</returns>
        public string Format(string format, string[] args)
        {
            if (args == null)
            {
                return null;
            }
            if (format == null || format == "")
            {
                return null;
            }
            try
            {
                return string.Format(format, args);
            }
            catch(Exception e)
            {
                return null;
            }
        } 
        #endregion

        #region 将指定字符串插入到字符串指定位置
        /// <summary>
        /// 将指定字符串插入到字符串指定位置
        /// </summary>
        /// <param name="source">源字符串("helo")</param>
        /// <param name="index">插入位置索引(2)</param>
        /// <param name="strInser">要插入的字符串("l")</param>
        /// <returns>字符串("hello")</returns>
        public string Insert(string source, int index, string strInser)
        {
            if (source == null || strInser == null || index < 0 || index > source.Length)
            {
                return null;
            }

             return source.Insert(index, strInser);            
        } 
        #endregion

        #region 将指定字符串插入到字符串结束位置
        /// <summary>
        /// 将指定字符串插入到字符串结束位置
        /// </summary>
        /// <param name="source">源字符串("hell")</param>
        /// <param name="strInser">要插入的字符串("o")</param>
        /// <returns>字符串("hello")</returns>
        public string Insert(string source, string strInser)
        {
            if (source == null || strInser == null)
            {
                return null;
            }

            return source.Insert(source.Length, strInser);
        } 
        #endregion

        #region 移除字符串中从指定位置开始到指定位置结束的所有字符
        /// <summary>
        /// 移除字符串中从指定位置开始到指定位置结束的所有字符
        /// </summary>
        /// <param name="source">源字符串("helabclo")</param>
        /// <param name="tindexStar">移除开始位置索引(3)</param>
        /// <param name="indexEnd">移除结束位置索引(5)</param>
        /// <returns>字符串("hello")</returns>
        public string Remove(string source, int tindexStar, int indexEnd)
        {
            if (source == null || source == "")
            {
                return null;
            }
            if (tindexStar > indexEnd) //限制开始位置不能大于结束位置
            {
                return null;
            }

            try
            {
                return source.Remove(tindexStar, indexEnd - tindexStar + 1); //调用Remove()方法移除指定字符
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 根据指定的字符将字符串拆分为数组
        /// <summary>
        /// 根据指定的字符将字符串拆分为数组
        /// </summary>
        /// <param name="source">源字符串("hello,world")</param>
        /// <param name="ch">拆分的标志字符(',')</param>
        /// <returns>拆分后的字符串数组{"hello","world"}</returns>
        public string[] Split(string source, char ch)
        {
            if (source == null || source == "")
            {
                return null;
            }
            return source.Split(ch);
        } 
        #endregion

        #region 以指定字符，在原有字符串左边，将字符串补满到指定长度
        /// <summary>
        /// 以指定字符，在原有字符串左边，将字符串补满到指定长度
        /// </summary>
        /// <param name="source">源字符串("hello")</param>
        /// <param name="ch">指定字符('#')</param>
        /// <param name="length">字符串最终长度(7)</param>
        /// <returns>补齐后的字符串("##hello")</returns>
        public string PadLeft(string source, char ch, int length)
        {
            if (source == null)
            {
                return null;
            }

            if (length < 0)
            {
                return source; //若指定的长度小于0则返回源字符串
            }

            try
            {
                return source.PadLeft(length, ch); //防止越界
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 以指定字符，在原有字符串右边，将字符串补满到指定长度
        /// <summary>
        /// 以指定字符，在原有字符串右边，将字符串补满到指定长度
        /// </summary>
        /// <param name="source">源字符串("hello")</param>
        /// <param name="ch">指定字符('#')</param>
        /// <param name="length">字符串最终长度(7)</param>
        /// <returns>补齐后的字符串("hello##")</returns>
        public string PadRight(string source, char ch, int length)
        {
            if (source == null)
            {
                return null;
            }

            if (length < 0)
            {
                return source; //若指定的长度小于0则返回源字符串
            }

            try
            {
                return source.PadRight(length, ch); //防止越界
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion

        #region 判断字符串是否以指定的字符串开始
        /// <summary>
        /// 判断字符串是否以指定的字符串开始
        /// </summary>
        /// <param name="source">源字符串("hello")</param>
        /// <param name="strTarget">要判断的开始字符串("he")</param>
        /// <returns>判断结果(true(是)，false(否))</returns>
        public bool StartWith(string source, string strTarget)
        {
            if (source == null || strTarget == null)
            {
                return false;
            }
            return source.StartsWith(strTarget); //调用string类的StartsWith()方法
        }
        #endregion

        #region 判断字符串是否以指定的字符串结束
        /// <summary>
        /// 判断字符串是否以指定的字符串结束
        /// </summary>
        /// <param name="source">源字符串("hello")</param>
        /// <param name="strTarget">要判断的结束字符串("o")</param>
        /// <returns>判断结果(true(是)，false(否))</returns>
        public bool EndsWith(string source, string strTarget)
        {
            if (source == null || strTarget == null)
            {
                return false;
            }
            return source.EndsWith(strTarget); //调用string类的EndsWith()方法
        }
        #endregion
    }
}
