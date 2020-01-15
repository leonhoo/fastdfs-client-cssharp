using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace org.csource.fastdfs.encapsulation
{
    /// <summary>
    ///
    /// </summary>
    public static class Strings
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="charToPad"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Get(int[] charToPad, int index, int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = index; i < length; i++)
            {
                builder.Append((char)charToPad[i]);
            }
            return builder.ToString();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="charToPad"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Get(byte[] charToPad, int index, int length)
        {
            StringBuilder builder = new StringBuilder();
            var len = index + length;
            for (int i = index; i < len; i++)
            {
                builder.Append((char)charToPad[i]);
            }
            return builder.ToString();
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="str">被分割的字符串</param>
        /// <param name="regex"></param>
        /// <param name="limit">
        /// <para>当 limit > 0的时候, str将被分割 limit-1次</para>
        /// <para>当 limit < 0 的时候, str将被尽可能多的分割</para>
        /// <para>当 limit = 0 的时候, str被尽可能多的分割，但是尾部的空字符串会被抛弃</para>
        /// <para> limit 默认为0 </para>
        /// </param>
        /// <returns></returns>
        public static string[] Split(this string str, string regex, int limit)
        {
            var temp = str;
            //去除尾部空字符串
            if (limit == 0)
            {
                temp = str.TrimEnd();
            }
            var result = new List<string>();
            Match match = Regex.Match(temp, regex);
            var index = 0;
            var nextIndex = 0;
            if (match.Success)
            {
                nextIndex = match.Index;
            }
            if (limit > 0)
            {
                var tag = 1;
                while (nextIndex <= temp.Length && nextIndex > index && tag < limit)
                {
                    result.Add(temp.Substring(index, nextIndex - index));
                    index = nextIndex + 1;
                    match = match.NextMatch();
                    nextIndex = match.Index;
                    tag++;
                }
            }
            else
            {
                while (nextIndex <= temp.Length && nextIndex > index)
                {
                    result.Add(temp.Substring(index, nextIndex - index));
                    index = nextIndex + 1;
                    match = match.NextMatch();
                    nextIndex = match.Index;
                }
            }
            //加上最后的字符
            if (index < temp.Length || (index == temp.Length && limit != 0))
            {
                result.Add(temp.Substring(index));
            }
            return result.ToArray();
        }

        public static string Get(byte[] body, Encoding charset)
        {
            return charset.GetString(body);
        }
    }
}
